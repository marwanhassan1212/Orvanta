# Orvanta – Project Structure

## 1. Overview

This document defines the proposed solution and folder organization for Orvanta.

The structure follows a layered organization to separate application responsibilities and make the codebase easier to navigate and maintain.

## 2. Solution Structure

```text
Orvanta/
│
├── Orvanta.sln
│
├── src/
│   │
│   ├── Orvanta.Web/
│   │   ├── Controllers/
│   │   ├── Views/
│   │   ├── ViewModels/
│   │   ├── Filters/
│   │   ├── Middleware/
│   │   ├── wwwroot/
│   │   │   ├── css/
│   │   │   ├── js/
│   │   │   ├── images/
│   │   │   └── lib/
│   │   ├── Extensions/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── Orvanta.Application/
│   │   ├── Interfaces/
│   │   │   ├── Repositories/
│   │   │   └── Services/
│   │   ├── Services/
│   │   ├── DTOs/
│   │   ├── Models/
│   │   ├── Validators/
│   │   ├── Authorization/
│   │   └── Common/
│   │
│   ├── Orvanta.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Constants/
│   │   └── Exceptions/
│   │
│   └── Orvanta.Infrastructure/
│       ├── Persistence/
│       │   ├── ApplicationDbContext.cs
│       │   ├── Configurations/
│       │   ├── Migrations/
│       │   └── Seed/
│       │
│       ├── Repositories/
│       ├── Identity/
│       └── DependencyInjection.cs
│
├── tests/
│   ├── Orvanta.UnitTests/
│   └── Orvanta.IntegrationTests/
│
├── docs/
│
├── .gitignore
└── README.md
```

## 3. Project Responsibilities

### 3.1 Orvanta.Web

The Presentation Layer.

Contains:
- MVC Controllers.
- Razor Views.
- ViewModels used by the UI.
- Custom filters and middleware.
- Static assets.
- Application startup and configuration.

Controllers handle requests and delegate business operations to Application services.

ViewModels are designed for presentation needs and should not replace domain entities or application DTOs.

### 3.2 Orvanta.Application

The Application Layer.

Contains:
- Service interfaces.
- Repository interfaces.
- Service implementations.
- Application DTOs.
- Input validation.
- Application-specific authorization logic.
- Shared application results and abstractions.

Example:

```text
Services/
├── SalesInvoiceService.cs
├── PurchaseInvoiceService.cs
├── InventoryService.cs
├── CustomerService.cs
├── SupplierService.cs
├── CustomerReceiptService.cs
├── SupplierPaymentService.cs
├── ExpenseService.cs
└── ReportingService.cs
```

Service interfaces are placed under Interfaces/Services.

Repository interfaces are placed under Interfaces/Repositories.

### 3.3 Orvanta.Domain

The Domain Layer.

Contains:
- Core entities.
- Domain enums.
- Business constants.
- Domain exceptions.

Example:

```text
Entities/
├── Category.cs
├── Unit.cs
├── Product.cs
├── Customer.cs
├── Supplier.cs
├── Warehouse.cs
├── InventoryStock.cs
├── InventoryTransaction.cs
├── SalesInvoice.cs
├── SalesInvoiceItem.cs
├── PurchaseInvoice.cs
├── PurchaseInvoiceItem.cs
├── InvoiceApprovalHistory.cs
├── CustomerReceipt.cs
├── ReceiptAllocation.cs
├── SupplierPayment.cs
├── PaymentAllocation.cs
├── ExpenseCategory.cs
├── Expense.cs
├── AuditLog.cs
└── CompanySetting.cs
```

Example enums:

```text
Enums/
├── InvoiceStatus.cs
├── PaymentMethod.cs
├── PaymentStatus.cs
├── InventoryTransactionType.cs
└── InvoiceType.cs
```

### 3.4 Orvanta.Infrastructure

The Infrastructure Layer.

Contains:
- EF Core DbContext.
- Entity configurations.
- Database migrations.
- Repository implementations.
- Identity persistence.
- Database initialization and seeding.

Example:

```text
Persistence/
├── ApplicationDbContext.cs
├── Configurations/
│   ├── ProductConfiguration.cs
│   ├── InventoryStockConfiguration.cs
│   ├── SalesInvoiceConfiguration.cs
│   └── PurchaseInvoiceConfiguration.cs
├── Migrations/
└── Seed/
```

## 4. Dependency Rules

The following dependency rules must be respected:

| Project | Can depend on |
|---|---|
| Domain | No other application project |
| Application | Domain |
| Infrastructure | Application and Domain |
| Web | Application, Domain where required, Infrastructure for composition root |

The Web project is responsible for assembling the application dependencies through Dependency Injection.

Infrastructure implementations must not be referenced directly throughout controllers.

## 5. Naming Conventions

- Projects use the `Orvanta.` prefix.
- Classes use PascalCase.
- Interfaces start with `I`.
- Methods use PascalCase.
- Private fields use camelCase.
- DTO names should describe their purpose.
- Entity configurations use the `Configuration` suffix.
- Services use the `Service` suffix.
- Repositories use the `Repository` suffix.

Examples:

```text
ISalesInvoiceService
SalesInvoiceService
ISalesInvoiceRepository
SalesInvoiceRepository
CreateSalesInvoiceDto
SalesInvoiceDetailsDto
SalesInvoiceConfiguration
```

## 6. Dependency Injection

Dependencies are registered through the built-in ASP.NET Core Dependency Injection container.

Infrastructure registers database and repository implementations.

Application services are registered through an application registration extension or the Web composition root.

Controllers receive dependencies through constructor injection.

## 7. Testing Structure

### Unit Tests

`Orvanta.UnitTests`

Used to test isolated business logic and service behavior.

Examples:
- Invoice discount calculations.
- Weighted average cost calculations.
- Stock validation.
- Allocation validation.
- Invoice approval rules.

### Integration Tests

`Orvanta.IntegrationTests`

Used to test interactions between application components and the database.

Examples:
- Invoice posting transactions.
- Inventory updates.
- Receipt allocation.
- Authorization behavior.

Testing will be introduced gradually during development.

## 8. Important Structural Rules

- Do not put business logic inside MVC controllers.
- Do not access DbContext directly from controllers.
- Do not place database-specific code in Domain.
- Do not return EF Core entities directly to Razor Views.
- Use ViewModels for UI-specific data.
- Use DTOs for application operations.
- Keep services focused on clear business responsibilities.
- Avoid unnecessary abstractions and empty folders.

## 9. Summary

The solution structure keeps the presentation, application, domain, and infrastructure responsibilities separate.

This organization provides a clear foundation for developing Orvanta without introducing unnecessary architectural complexity.