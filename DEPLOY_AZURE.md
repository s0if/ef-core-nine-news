var connectionString = builder.Configuration.GetConnectionString("LocalSqlServer");var connectionString = builder.Configuration.GetConnectionString("LocalSqlServer");var connectionString = builder.Configuration.GetConnectionString("LocalSqlServer");var connectionString = builder.Configuration.GetConnectionString("LocalSqlServer");var connectionString = builder.Configuration.GetConnectionString("LocalSqlServer");# Azure Deployment Guide – EFCoreNews

![link website ](http://ef-core-eth5hvd9e0a0fefn.israelcentral-01.azurewebsites.net)
This document explains the full deployment of **EFCoreNews** to **Azure App Service**, including configuration, logging, security, deployment, and database migrations.

---

## 1️⃣ Create Azure App Service

The application was deployed on Microsoft Azure using **Azure App Service**.

**Configuration:**

| Setting  | Value          |
| -------- | -------------- |
| App Name | ef-core        |
| Runtime  | .NET           |
| OS       | Windows        |
| Region   | Israel Central |
| Plan     | Basic (B1)     |

![First configuration](https://drive.google.com/uc?export=view&id=1RwLOQ9l2r4R8NFYIqVqMRa9REHZqt3t0)
![second Configuration](https://drive.google.com/uc?export=view&id=1TeW_pxD3AFTzgO8QcxXEVKn3zlG2W6vy)

---

## 2️⃣ Configure Application Settings & Connection String

### Environment Variables

**Path:**  
Azure Portal → App Service → **Environment Variables**

| Key                    | Value      |
| ---------------------- | ---------- |
| ASPNETCORE_ENVIRONMENT | Production |

### Connection String

| Name           | Type      | Value                       |
| -------------- | --------- | --------------------------- |
| LocalSqlServer | SQLServer | Azure SQL Connection String |

Used in code:

```csharp
var connectionString = builder.Configuration.GetConnectionString("LocalSqlServer");
```
![Environment Variables](https://drive.google.com/uc?export=view&id=1gAWpeNNKkEDa6lp70wKGRYZe0HN-GD1l)
![connectionString](https://drive.google.com/uc?export=view&id=11joksLzZI2Zg0pR-02eADejlVKBuQ8cy)
---

## 3️⃣ Enable HTTPS

Path:
Configuration → General Settings

Enabled:

HTTPS Only ✔

Minimum TLS Version: 1.2
![ Enable HTTPS](https://drive.google.com/uc?export=view&id=14BzWnpmEvLLJsvBLpg4eeeejxh_ldpy4)



4️⃣ Enable Logging

Path:
Monitoring → App Service Logs

Enabled:

Application Logging (Filesystem): ON

Log Level: Information

Detailed Error Messages: ON

Failed Request Tracing: ON

![Enable Logging](https://drive.google.com/uc?export=view&id=1Nvq-QeDe8bOKYcSM8s6TazCV2Y9JfFyN)
---
5️⃣ Deploy Application (Zip Deploy)

Steps:

Publish project locally

Zip the publish folder

Upload via:
https://ef-core-eth5hvd9e0a0fefn.israelcentral-01.azurewebsites.net
Deployment completed successfully.
![Deploy Application](https://drive.google.com/uc?export=view&id=1rHfL1xVpQC6bK6_W3Rri-9yQnburC9Fq )

---
## 6️⃣ Database Migrations Strategy

Two approaches were considered for applying Entity Framework Core migrations:
### Pipeline / Deployment Step (Used in this project)
In this project, migrations were executed before application startup using the EF CLI:.
dotnet ef database update

This approach provides better control, safety, and separation of concerns.


## 7 Azure SQL Database Deployment

The application database was successfully deployed to **Azure SQL Database**.

### Database Information

| Setting | Value |
|------|------|
Server | ef-core-nine-news.database.windows.net |
Database | ef-core-nine-news |
Authentication | SQL Authentication |
Encryption | Enabled |
Connection Method | Public Endpoint |

The database is hosted on Azure and connected to the App Service using the configured **Connection String** stored securely in Azure **Environment Variables**.

The application was verified to communicate correctly with the cloud database after deployment.

![azure-sql-server](https://drive.google.com/uc?export=view&id=1HLoaBKbx30luQwIOCXsGBhCybQLToiel )
![azure-sql-database](https://drive.google.com/uc?export=view&id=1nGWRJULYQ_30kyIybMtj90TpCg_EhV3j )
![connection-string](https://drive.google.com/uc?export=view&id=1cKZEh0iGDTXS3_iZamDeCS2Q90DljcAK )


---

## 🧪 Verification

After deployment, the following checks were performed:

- API endpoints respond successfully.
- Database read/write operations work correctly.
- Logs confirm successful database connections.
- HTTPS communication is enforced.

The cloud environment is now fully operational and production-ready.


```
