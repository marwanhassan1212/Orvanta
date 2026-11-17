# Orvanta – Architecture Decisions

## 1. Purpose

This document records the main architectural and technical decisions made during Orvanta development.

Each decision includes the selected approach and its reasoning.

## 2. Architecture Decisions

### ADR-001: Layered Monolithic Architecture

**Status:** Accepted

**Decision:**

Use a Layered Monolithic Architecture.

**Reason:**
- Suitable for a medium-sized trading company.
- Easier to develop and deploy.
- Clear separation of responsibilities.
- Avoids unnecessary distributed-system complexity.

Microservices are not required for the MVP.

### ADR-002: ASP.NET Core MVC

**Status:** Accepted

**Decision:**

Use ASP.NET Core MVC with Razor Views.

**Reason:**
- Server-rendered business application.
- Suitable for dashboards, forms, tables, and management screens.
- Integrated routing, model binding, validation, and authorization.
- Avoids maintaining a separate frontend application.

### ADR-003: SQL Server and Entity Framework Core

**Status:** Accepted

**Decision:**

Use SQL Server as the relational database and EF Core as the ORM.

**Reason:**
- Strong relational data support.
- Transaction management.
- Entity relationships and constraints.
- LINQ-based queries.
- Database migrations.

### ADR-004: ASP.NET Core Identity

**Status:** Accepted

**Decision:**

Use ASP.NET Core Identity for authentication and user management.

**Reason:**
- Built-in password hashing.
- User and role management.
- Authentication integration.
- Reduced need to implement security-sensitive functionality manually.

### ADR-005: Role-Based Authorization with Policies

**Status:** Accepted

**Decision:**

Use Identity Roles and Authorization Policies.

Roles:
- Admin
- Manager
- Accountant
- SalesEmployee
- WarehouseEmployee

**Reason:**

The MVP has a defined set of employee roles and does not require a dynamic permission-management system.

Authorization must be enforced on sensitive application operations as well as MVC endpoints.

### ADR-006: Service Layer

**Status:** Accepted

**Decision:**

Business operations are implemented through application services.

**Reason:**
- Keeps controllers lightweight.
- Centralizes business rules.
- Supports reuse across application workflows.
- Makes business logic easier to test.

### ADR-007: Repository Pattern

**Status:** Accepted

**Decision:**

Use repository abstractions for data access where they provide meaningful separation.

**Reason:**
- Separates persistence details from application services.
- Centralizes common queries.
- Helps maintain a consistent data access approach.

Avoid creating unnecessary generic repositories that hide useful EF Core capabilities.

### ADR-008: Dependency Injection

**Status:** Accepted

**Decision:**

Use the built-in ASP.NET Core Dependency Injection container.

**Reason:**
- Reduces tight coupling.
- Supports interface-based dependencies.
- Simplifies testing and service registration.

### ADR-009: Database Transactions for Critical Operations

**Status:** Accepted

**Decision:**

Use database transactions for operations that update multiple related records.

Examples:
- Posting invoices.
- Updating inventory.
- Creating inventory transactions.
- Posting receipts and payments.
- Voiding financial records.

**Reason:**

These operations must be atomic to avoid inconsistent business data.

### ADR-010: Weighted Average Costing per Warehouse

**Status:** Accepted

**Decision:**

Use the Weighted Average Costing method independently for each warehouse.

**Reason:**

The company operates multiple warehouses, and each warehouse must maintain its own inventory valuation.

The system must preserve historical inventory transactions and the cost snapshots used in sales.

When stock reaches zero, the last average cost is retained. The next incoming stock recalculates the average using the new receipt cost.

### ADR-011: No Negative Inventory

**Status:** Accepted

**Decision:**

The system must prevent stock from becoming negative.

**Reason:**

Negative stock can cause inaccurate inventory valuation and incorrect sales costing.

Stock availability must be validated during invoice posting, with concurrency protection for simultaneous operations.

### ADR-012: Invoice Approval Workflow

**Status:** Accepted

**Decision:**

Sales and purchase invoices require approval before posting.

The creator cannot approve their own invoice.

**Reason:**

This provides a maker-checker control and reduces the risk of unauthorized or incorrect transactions.

Rejected invoices require a rejection reason, and approval history must be preserved.

### ADR-013: Posted Invoice Immutability

**Status:** Accepted

**Decision:**

Posted sales and purchase invoices cannot be edited or deleted in the MVP.

**Reason:**

Changing posted invoices could invalidate inventory, costing, and financial reports.

Corrections through returns and other adjustment workflows are deferred beyond the MVP.

### ADR-014: Cash-Only Operations

**Status:** Accepted

**Decision:**

The MVP supports cash-based receipts and payments only.

Online payment gateways and electronic payment integrations are excluded.

**Reason:**

The project focuses on internal business management rather than online payment processing.

### ADR-015: Single-Company Scope

**Status:** Accepted

**Decision:**

The MVP supports one company.

Multi-company management is deferred.

**Reason:**

The initial business scenario represents one trading company with multiple warehouses.

### ADR-016: No Full Accounting System

**Status:** Accepted

**Decision:**

The MVP provides operational and estimated financial reports but does not implement a full double-entry accounting system or general ledger.

**Reason:**

The project's primary scope is trading operations, inventory, invoices, collections, payments, and expenses.

### ADR-017: Audit Logging

**Status:** Accepted

**Decision:**

Use a dedicated AuditLogs table to record important user activities.

**Reason:**

Audit logs provide traceability for important business actions.

InvoiceApprovalHistory remains separate because it records the specific approval lifecycle of invoices.

### ADR-018: No Manual Inventory Adjustments

**Status:** Accepted

**Decision:**

Manual stock adjustments are excluded from the MVP.

Initial stock is introduced through opening balances.

Subsequent stock changes must originate from supported business operations.

**Reason:**

This preserves traceability and reduces uncontrolled changes to inventory quantities.

### ADR-019: Incremental Testing

**Status:** Accepted

**Decision:**

Introduce automated testing gradually during development.

**Reason:**

Critical calculations and business workflows should be tested as they are implemented rather than postponing all testing until the end.

Initial testing priorities:
- Inventory costing.
- Invoice calculations.
- Stock validation.
- Approval rules.
- Receipt and payment allocations.
- Transaction consistency.

### ADR-020: No CQRS or Microservices in the MVP

**Status:** Accepted

**Decision:**

Do not introduce CQRS, MediatR, or Microservices in the MVP.

**Reason:**

The current system does not require the additional complexity of separate read/write models or distributed services.

The architecture can evolve later if actual business or performance requirements justify it.

## 3. Deferred Architectural Decisions

The following capabilities are outside the MVP and require separate decisions if introduced later:

| Capability | Status |
|---|---|
| Multi-Company Support | Deferred |
| Multi-Currency | Deferred |
| Sales and Purchase Returns | Deferred |
| Warehouse Transfers | Deferred |
| General Ledger | Deferred |
| Dynamic Permissions | Deferred |
| Online Payment Integration | Deferred |
| CQRS | Not planned for MVP |
| Microservices | Not planned for MVP |

## 4. Decision Review

Architectural decisions may be reviewed when:
- Business requirements change.
- Performance issues are identified.
- New modules are introduced.
- Existing decisions create unnecessary development limitations.

Any major change should be documented with its reason and impact.

## 5. Summary

These decisions establish a practical architecture for Orvanta that prioritizes business correctness, maintainability, security, and controlled complexity.