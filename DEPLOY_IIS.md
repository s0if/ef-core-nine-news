# IIS Deployment Guide — EF Core Nine News

## A) Windows Server + IIS Deployment

---

## 1. Installing and Enabling IIS

IIS was enabled from Windows Features with the required components:

- Web Server (IIS)
- ASP.NET
- .NET Extensibility
- ISAPI Extensions & Filters

### 📸 Screenshot: IIS_Features_Enabled.png

![IIS Features Enabled](https://drive.google.com/file/d/19PEhkm09peIX45YpijMfB_PTA9QXC25C/view?usp=drive_link)

---

## 2. Installing .NET Hosting Bundle for IIS

The correct .NET Hosting Bundle was installed to allow IIS to host ASP.NET applications.

### 📸 Screenshot: DotNet_HostingBundle_Installed.png

![DotNet Hosting Bundle Installed](https://drive.google.com/file/d/1CvcDNzv_OoL8RYieVe56tJdvrda98m2U/view?usp=drive_link)

---

## 3. Publishing the Project

The project was published using:

```bash
dotnet publish -c Release
```

The application was deployed using **Folder Publish** and placed at:

```
C:\inetpub\wwwroot\app-news
```

### 📸 Screenshot: Publish_Folder_Output.png

![Publish Folder Output](https://drive.google.com/file/d/1JKAajM6hSVzw6-QiD7zY0FKvwlQ8yPr_/view?usp=drive_link)

---

## 4. IIS Site Configuration

A new website was created in IIS with:

- Physical Path → `C:\inetpub\wwwroot\app-news`
- Application Pool → **DefaultAppPool**
- Pipeline Mode → **Integrated**

### 📸 Screenshot: IIS_Site_Settings.png

![IIS Site Settings](https://drive.google.com/file/d/1jL95jHuHY6Mn2du9Qefk8PEajxgN4p1D/view?usp=drive_link)

---

## 5. Application Pool Configuration

The application is running under the default IIS application pool:

- Application Pool Name: **DefaultAppPool**
- Pipeline Mode: **Integrated**
- Hosting Model: **ASP.NET Core (No Managed Code required)**

### 📸 Screenshot: ApplicationPool_Settings.png

![Application Pool Settings](https://drive.google.com/file/d/1W8-Cz7Bk5br75UW1ehA4C9W_Svd4l9gd/view?usp=drive_link)

---

## 6. Environment Configuration

The environment variable was configured:

```
ASPNETCORE_ENVIRONMENT = Production
```

### 📸 Screenshot: Environment_Variable_Setup.png

![Environment Variable Setup](https://drive.google.com/file/d/1G2KsHbOCiLeiadlESXyxveDZOLY5si8e/view?usp=drive_link)

---

## 7. Database Connection Issue & Resolution

### ❌ Problem

After deployment, the application showed the error:

```
Login failed for user 'IIS APPPOOL\DefaultAppPool'
```

### 🧠 Cause

IIS runs the application under the identity:

```
IIS APPPOOL\DefaultAppPool
```

This account did not exist in SQL Server, so the database rejected the connection.

### ✅ Solution

Inside SQL Server Management Studio (SSMS):

To allow the web application to access SQL Server, a Windows-based login was created for the IIS Application Pool.

Inside SQL Server Management Studio (SSMS), the following step was performed:

- **Created a new Login:**
  `IIS APPPOOL\DefaultAppPool`
  using **Windows Authentication**.

This login allows SQL Server to recognize the identity under which the application runs in IIS, enabling proper authentication between the web server and the database engine.

### 📸 Screenshot: SQL_Login_Config.png

![SQL Login Configuration](https://drive.google.com/file/d/17UaEHix6ysBw4WgsFC1ucL7QPxqXNGKe/view?usp=drive_link)

---

## 8. Running Migrations on the Server

Database migrations were executed on the server using:

```bash
dotnet ef database update
```

---

## 8.1 Automated Deployment using GitHub Actions (CI/CD)

In addition to manual deployment, the project also supports automated deployment using **GitHub Actions** and a self-hosted Windows runner on the IIS server.

### Workflow Location

The deployment pipeline is defined in the following file inside the repository:

```
.github/workflows/deploy-iis.yml
```

### Pipeline Responsibilities

The pipeline performs the following steps automatically whenever changes are pushed to the `main` branch:

1. Checkout the repository
2. Restore NuGet packages
3. Build the application in Release mode
4. Publish the project
5. Install EF Core CLI
6. Apply database migrations
7. Restart IIS to apply the new version

### Deployment Workflow

```yaml
name: Deploy to IIS

on:
  push:
    branches:
      - main

jobs:
  deploy:
    runs-on: [self-hosted]

    steps:
      - name: Checkout code
        uses: actions/checkout@v4

      - name: Restore
        run: dotnet restore

      - name: Build
        run: dotnet build -c Release

      - name: Publish
        run: dotnet publish -c Release

      - name: Create tools manifest
        run: dotnet new tool-manifest --force

      - name: Install EF Core CLI
        run: dotnet tool install dotnet-ef --version 9.0.1

      - name: Run migrations
        run: dotnet ef database update

      - name: Restart IIS
        run: iisreset
```

This pipeline ensures consistent, repeatable, and safe deployments with database migrations handled automatically during release.

---

## 9. Endpoint Verification

The deployed application was verified by opening:

```
http://localhost/app-news
```

The application responded successfully.

### 📸 Screenshot: Live_Endpoint_Test.png

![Live Endpoint Test](https://drive.google.com/file/d/18uLh4c1yNnG7rdWo_JyIEaIKJ83ONcWU/view?usp=drive_link)

---

## ✅ Final Result

The application was successfully:

- Published and deployed on IIS
- Connected to SQL Server
- Migrated correctly
- Configured for Production
- Verified and operational
