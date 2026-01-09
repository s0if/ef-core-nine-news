using EFCoreNews.Data;
using EFCoreNews.Interface;
using EFCoreNews.Middleware;
using EFCoreNews.Models;
using EFCoreNews.Services;
using EFCoreNews.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
//var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

var connectionString = builder.Configuration.GetConnectionString("LocalSqlServer");
builder.Services.AddDbContext<NewsDbContext>(options => options.UseSqlServer(connectionString, x => x.UseHierarchyId()));

builder.Services.AddScoped<EFNewsService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISeedData, SeedData>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPartyEventService, PartyEventService>();
builder.Services.AddScoped<IPersonService, PersonService>();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("=== Application started successfully ===");

app.UseMiddleware<CorrelationIdMiddleware>();

// Seed Data Endpoint

app.MapPost("/customer/seed", async (ISeedData service) =>
{
    await service.SeedCustomerData();
    return Results.NoContent();
});


app.MapPut("/customer/update-address", async (Address address, ICustomerService service) =>
{
    await service.ExecuteUpdateAddress(address);
    return Results.Ok();
});

app.MapGet("/customer-console-writeline", async (ICustomerService service) =>
{
   await service.ComplexTypesGroupByAsync();
    return Results.NoContent();
});

app.MapGet("/customer/us-customers", async (ICustomerService service) =>
{
    var usCustomers = await service.GetUSCustomers();
    return Results.Ok(usCustomers);
});

app.MapGet("/post/with-author", async (IPostService service) =>
{
    var postWithAuthors = await service.GetPostsWithAuthors();
    return Results.Ok(postWithAuthors);
});

app.MapGet("/post/recent-posts-quantity", async (IPostService service) =>
{
    var recentPostQuantity = await service.GetRecentPostQuantity();
    return Results.Ok(recentPostQuantity);
});

app.MapGet("/post/with-author-count", async (IPostService service) =>
{
    var postWithAuthors = await service.GetPostsWithAuthorsUsingCount();
    return Results.Ok(postWithAuthors);
});

app.MapGet("/post/new-products", async (IProductService service) =>
{
    var newProducts = await service.GetNewProducts();
    return Results.Ok(newProducts);
});

app.MapGet("/post/ordered-posts-with-authors", async (IPostService service) =>
{
    var orderedPostsWithAuthors = await service.NewOrderOperators();
    return Results.Ok(orderedPostsWithAuthors);
});
app.MapGet("/health", () =>
{
    return Results.Ok(new { status = "ok" });
});


app.Run();
