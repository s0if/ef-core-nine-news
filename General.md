# 🧾 GENERAL.md

## Configuration & Deployment Notes — EF Core Nine News

This document summarizes the main configuration issues discovered in the project and the actions taken to stabilize the application for development, migration, and runtime.

---

## 🛠 Initial Issues & Fixes

### 1. Missing EF Core Tools

The package `Microsoft.EntityFrameworkCore.Tools` was not installed, which caused migration and design-time commands to fail.

**Fix:**

The package was added to enable proper migration and tooling support.

---

### 2. Person Model Constructor Issue

The `Person` entity contained only parameterized constructors, which prevented EF Core from creating instances at runtime and during design-time operations.

**Fix:**

An empty (parameterless) constructor was added to the `Person` model to satisfy EF Core's materialization requirements.

---

### 3. HierarchyId Mapping Problems

The `HierarchyId` property in `Person`:

- Had no explicit mapping
- Was not enabled by default
- Caused migration and runtime failures

**Additionally:**

- EF Core Tools could not determine that SQL Server was the provider
- `UseSqlServer()` was not configured during Design-Time
- `HierarchyId` support was not enabled explicitly

**Fixes Applied:**

- SQL Server provider was explicitly configured
- Design-time configuration was corrected so EF Core Tools recognize the provider
- The project was stabilized to run without enabling `HierarchyId` at runtime

**Current State**

The application is currently operating without requiring SQL Server `HierarchyId` support at runtime.  
This design decision was intentionally made during development and deployment to avoid potential runtime issues while still allowing database migrations and all core application functionality to execute safely and reliably.

**Alternative Strategy**

Instead of depending on `HierarchyId` during normal application execution, the system uses standard relational identifiers such as `INT` and `UNIQUEIDENTIFIER` as primary keys for all user-facing operations.  
This approach guarantees system stability, broad environment compatibility, and uninterrupted functionality for end users without introducing unnecessary operational complexity in production.

`HierarchyId` remains part of the database schema exclusively for internal structural representation and future expansion. It can be safely activated in later versions if advanced hierarchical features become a functional requirement.

### Practical Application

Within the `Person` entity, the application includes a `HierarchyId` field (`PathFromPatriarch`) to model hierarchical relationships.  
However, all business services, API endpoints, and application workflows rely solely on the standard `Id` field for identification and processing.

This ensures that the core system remains stable, environment-agnostic, and resilient, while preserving the option for future hierarchical enhancements.

---

## 🧩 Final Outcome

After applying these corrections:

- Migrations execute correctly
- Design-time tooling works as expected
- The project builds and runs without runtime failures
- Configuration is now stable for further development and deployment
