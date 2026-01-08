using System.Diagnostics;

namespace EFCoreNews.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderName = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<CorrelationIdMiddleware> logger)
        {
            // 1) Get or create correlation id
            var correlationId = context.Request.Headers[HeaderName].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(correlationId))
                correlationId = Guid.NewGuid().ToString();

            // 2) Put it in response header
            context.Response.Headers[HeaderName] = correlationId;

            // 3) Extra useful ids
            var traceId = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;

            // 4) Scope => added to every log in this request
            using (logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId,
                ["TraceId"] = traceId,
                ["Path"] = context.Request.Path.Value ?? "",
                ["Method"] = context.Request.Method
            }))
            {
                var sw = Stopwatch.StartNew();
                logger.LogInformation("Request started");

                try
                {
                    await _next(context);
                    sw.Stop();

                    logger.LogInformation(
                      "Request finished. CorrelationId={CorrelationId} StatusCode={StatusCode} ElapsedMs={ElapsedMs}",
                      correlationId,
                      context.Response.StatusCode,
                      sw.ElapsedMilliseconds
                    );
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    logger.LogError(ex, "Request failed after {ElapsedMs}ms", sw.ElapsedMilliseconds);
                    throw;
                }
            }
        }
    }
}
