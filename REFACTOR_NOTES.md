# Refactor Task — EF Core Nine News

This document describes the refactoring work performed according to **Option 2 — Refactor Task**.

---

## 🎯 Objective

The goal was to improve code structure, separation of concerns, validation, and testability without changing the existing business behavior.

---

## 🧩 Refactor Scope

The following service was selected for refactoring:

> **EFNewsService → CustomerService**

---

## 🧱 1. Separation of Concerns

Originally, multiple responsibilities were handled inside a single service class.  
The following responsibilities were extracted into a dedicated service:

- Customer business logic
- Validation
- Database operations related to customers

### Before

- Customer logic mixed inside `EFNewsService`

### After

- Created **`ICustomerService`**
- Implemented **`CustomerService`**
- Moved customer-related logic from `EFNewsService` into `CustomerService`

This resulted in:

- Cleaner architecture
- Easier maintenance
- Improved testability

---

## 🛡 2. Validation Added

Input validation was added to the following method:

```csharp
ExecuteUpdateAddress(Address newAddress)
```

### Implemented Rules

- Street must not be empty
- Number must be greater than zero
- City must not be empty
- Country must not be empty
- PostCode must not be empty

Invalid input now throws meaningful exceptions before reaching the database layer.

---

## 🧪 3. Unit Testing

Unit tests were implemented using **NUnit** with a lightweight in-memory SQLite database.

### Key Design Decisions

To ensure reliable testing while avoiding `HierarchyId` provider issues:

- A derived test context `TestNewsDbContext` was created
- `OnConfiguring` was overridden to prevent execution of base configuration
- The `Person` entity was explicitly ignored to bypass unsupported `HierarchyId` mapping
- `Address` was configured as an owned/complex type
- SQLite in-memory database was used for realistic relational behavior

### Test Infrastructure (Excerpt)

```csharp
private sealed class TestNewsDbContext : NewsDbContext
{
    public TestNewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // intentionally empty
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<Person>();
        modelBuilder.Entity<Customer>().OwnsOne(c => c.CustomerAddress);
    }
}
```

### Implemented Tests

| Test Name                                           | Purpose                                         |
| --------------------------------------------------- | ----------------------------------------------- |
| ExecuteUpdateAddress_Throws_When_Street_Is_Empty    | Validates input validation logic                |
| GetUSCustomers_Returns_TotalCount_For_USA_Customers | Verifies business logic and pagination behavior |

### Testing Tools

- NUnit
- EF Core InMemory provider

The refactored service is now fully testable and verified by automated unit tests.

---

## 🏁 Result

After refactoring:

- Business logic is cleanly separated
- Validation is enforced
- The codebase is more maintainable

This refactor improves long-term stability without affecting existing behavior.
