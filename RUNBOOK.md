# 🧭 RUNBOOK – EFCoreNews

This runbook describes operational procedures for health checks, logging and database operations.

---

## 1️⃣ Health Check Endpoint

### Endpoint

```
GET /health
```

### Expected Response

```json
{ "status": "ok" }
```

### Usage

Used by monitoring systems to verify service availability.

---

## 2️⃣ Structured Logging with Correlation ID

Each incoming request is assigned a CorrelationId via custom middleware.

### Logged Fields

- CorrelationId
- TraceId
- HTTP Method
- Request Path
- Status Code
- Execution Time
- Exception (if any)

### Sample Log

```
Request started
Request finished. CorrelationId=648d2dad-aea6-4cf9-b690-abb4c15d4d46 StatusCode=200 ElapsedMs=11
```

This allows tracking each request across logs and debugging distributed flows.

---

## 3️⃣ Application Startup

### Start Locally

```
dotnet run
```

### Successful Startup Log

```
Application started successfully
Now listening on: https://localhost:7283
```

---

## 4️⃣ Database Operations

### List Migrations

```
dotnet ef migrations list
```

### Apply Migrations

```
dotnet ef database update
```

### Rollback Database

```
dotnet ef database update <PreviousMigrationName>
```
