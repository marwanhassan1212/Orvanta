# Orvanta – Development Plan

## 1. Introduction

This document defines the development approach, implementation phases, technical activities, and expected deliverables for the Orvanta Business Management System.

The project will be developed incrementally, with each phase building on the previous one to maintain architectural consistency and reduce implementation risks.

## 2. Development Objectives

The development process aims to:

1. Build a reliable business management application.
2. Implement the agreed business requirements.
3. Maintain consistency between the database, business logic, and user interface.
4. Apply secure authentication and authorization.
5. Ensure inventory and financial data integrity.
6. Maintain readable, maintainable, and reusable code.
7. Verify important business workflows before delivery.

## 3. Development Methodology

Orvanta will follow an incremental development approach.

The project will be divided into implementation phases. Each phase will produce a working part of the system that can be reviewed before moving to the next phase.

### Development Cycle

1. Analyze requirements.
2. Design the required functionality.
3. Implement the database and backend logic.
4. Implement the user interface.
5. Verify business rules and functionality.
6. Review and integrate the completed module.

## 4. Technology Stack

| Area | Technology |
|---|---|
| Programming Language | C# |
| Backend Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core |
| Database | Microsoft SQL Server |
| Authentication | ASP.NET Core Identity |
| Authorization | Roles and Authorization Policies |
| Frontend | Razor Views, HTML, CSS, JavaScript |
| Version Control | Git |
| Repository Hosting | GitHub |
| Database Design | dbdiagram.io |
| Documentation | Markdown |

## 5. Development Phases

### Phase 1: Project Preparation

**Objective:** Prepare the development environment and establish the project foundation.

**Tasks:**
- Create the GitHub repository.
- Create the ASP.NET Core MVC solution.
- Configure the application settings.
- Configure SQL Server connectivity.
- Install required NuGet packages.
- Establish the project folder structure.
- Configure Git and `.gitignore`.
- Prepare the development database.
- Verify that the application runs successfully.

**Deliverables:**
- Initial ASP.NET Core MVC application.
- Configured development environment.
- GitHub repository.
- Initial project structure.

### Phase 2: Architecture and Database Implementation

**Objective:** Implement the database structure and application architecture.

**Tasks:**
- Review the approved Entity Relationship Diagram.
- Create the Domain and Data Access layers.
- Define entities and relationships.
- Configure Entity Framework Core.
- Configure ASP.NET Core Identity.
- Implement entity configurations.
- Configure database constraints and indexes.
- Create and apply initial migrations.
- Seed predefined roles and required initial data.

**Deliverables:**
- Implemented database schema.
- Entity models.
- Entity configurations.
- Identity configuration.
- Initial database migrations.
- Required seed data.

### Phase 3: Authentication and Authorization

**Objective:** Implement secure user authentication and role-based access control.

**Tasks:**
- Configure ASP.NET Core Identity.
- Implement login and logout.
- Implement user management.
- Implement user activation and deactivation.
- Implement role assignment.
- Seed predefined system roles.
- Configure authorization policies.
- Protect restricted controllers and actions.
- Validate authorization in sensitive business services.

**Deliverables:**
- Working authentication system.
- User management module.
- Role assignment functionality.
- Authorization policies.

### Phase 4: Master Data Management

**Objective:** Implement the main business records required by other modules.

**Modules:**
- Categories.
- Units.
- Products.
- Customers.
- Suppliers.
- Warehouses.

**Tasks:**
- Implement repositories and services.
- Implement create, read, update, and deactivate operations.
- Add server-side validation.
- Enforce unique business identifiers.
- Implement pagination and filtering where needed.
- Preserve historical records.
- Implement the corresponding management interfaces.

**Deliverables:**
- Completed master data modules.
- Validated business records.
- Functional management interfaces.

### Phase 5: Inventory Management

**Objective:** Implement warehouse-specific inventory tracking and costing.

**Tasks:**
- Implement InventoryStocks management.
- Implement opening stock balances.
- Implement inventory transaction recording.
- Implement stock availability validation.
- Implement Weighted Average Cost calculations.
- Implement stock updates after purchase posting.
- Implement stock deduction after sales posting.
- Implement concurrency handling using RowVersion.
- Prevent negative stock.
- Preserve the last average cost when quantity reaches zero.
- Implement inventory reports.

**Deliverables:**
- Inventory management module.
- Inventory transaction history.
- Warehouse-specific stock tracking.
- Weighted Average Cost calculation.
- Inventory reports.

### Phase 6: Sales Management

**Objective:** Implement customer sales operations and invoice workflows.

**Tasks:**
- Implement sales invoice creation.
- Implement sales invoice line items.
- Implement customer and warehouse selection.
- Implement invoice-level discounts.
- Implement subtotal and total calculations.
- Implement draft management.
- Implement invoice submission.
- Implement approval and rejection.
- Implement invoice approval history.
- Implement inventory validation before posting.
- Implement transactional inventory deduction.
- Record cost snapshots at posting.
- Prevent modifications to posted invoices.
- Implement sales invoice views and reports.

**Deliverables:**
- Sales management module.
- Sales invoice lifecycle.
- Maker–Checker approval workflow.
- Inventory integration.
- Sales reports.

### Phase 7: Purchase Management

**Objective:** Implement supplier purchasing operations and inventory receiving.

**Tasks:**
- Implement purchase invoice creation.
- Implement purchase invoice line items.
- Implement supplier and warehouse selection.
- Implement invoice-level discounts.
- Implement proportional discount allocation.
- Calculate effective purchase unit costs.
- Implement draft management.
- Implement invoice submission.
- Implement approval and rejection.
- Implement invoice approval history.
- Implement transactional inventory receiving.
- Update Weighted Average Cost.
- Prevent modifications to posted invoices.
- Implement purchase invoice views and reports.

**Deliverables:**
- Purchase management module.
- Purchase invoice lifecycle.
- Purchase approval workflow.
- Inventory receiving integration.
- Purchase reports.

### Phase 8: Customer Receipts and Supplier Payments

**Objective:** Implement cash collection and supplier payment operations.

**Customer Receipts Tasks:**
- Create customer receipts.
- Select the related customer.
- Allocate receipts to posted sales invoices.
- Validate outstanding invoice balances.
- Enforce full allocation before posting.
- Implement receipt posting.
- Implement receipt voiding with authorization and reason.
- Calculate customer outstanding balances.

**Supplier Payments Tasks:**
- Create supplier payments.
- Select the related supplier.
- Allocate payments to posted purchase invoices.
- Validate outstanding invoice balances.
- Enforce full allocation before posting.
- Implement payment posting.
- Implement payment voiding with authorization and reason.
- Calculate supplier outstanding balances.

**Deliverables:**
- Customer receipt module.
- Supplier payment module.
- Allocation functionality.
- Customer and supplier outstanding balances.

### Phase 9: Expense Management

**Objective:** Implement operational expense tracking.

**Tasks:**
- Implement expense category management.
- Implement expense creation.
- Implement expense validation.
- Implement expense posting.
- Implement expense voiding with authorization and reason.
- Implement expense history.
- Include posted expenses in financial reports.

**Deliverables:**
- Expense management module.
- Expense categories.
- Expense records and status management.

### Phase 10: Reports and Dashboard

**Objective:** Provide management with useful operational and financial information.

**Tasks:**
- Implement sales reports.
- Implement purchase reports.
- Implement inventory reports.
- Implement inventory valuation.
- Implement customer outstanding reports.
- Implement supplier outstanding reports.
- Implement receipt and payment reports.
- Implement expense reports.
- Calculate gross profit.
- Calculate estimated operating profit.
- Implement date filters and relevant report filters.
- Build the main management dashboard.

**Deliverables:**
- Management dashboard.
- Operational reports.
- Financial management reports.
- Report filtering functionality.

### Phase 11: Auditing and Company Settings

**Objective:** Improve accountability and allow management of company information.

**Tasks:**
- Implement audit logging.
- Record important user activities.
- Record selected old and new values.
- Implement audit log viewing.
- Implement company information management.
- Configure the default currency.
- Verify audit coverage for important operations.

**Deliverables:**
- Audit logging module.
- Company settings module.
- Activity history.

### Phase 12: Testing and Quality Assurance

**Objective:** Verify that the application behaves correctly and protects business data.

**Tasks:**
- Learn the testing fundamentals required for the project.
- Write unit tests for important business services.
- Test inventory costing calculations.
- Test invoice approval rules.
- Test receipt and payment allocations.
- Test authorization restrictions.
- Test invalid business operations.
- Perform integration tests for critical database workflows.
- Perform manual end-to-end workflow testing.
- Fix identified defects.

**Deliverables:**
- Test cases.
- Automated tests for critical business logic.
- Integration test coverage for important workflows.
- Defect and verification records.

### Phase 13: Finalization and Delivery

**Objective:** Prepare the application for demonstration and portfolio delivery.

**Tasks:**
- Review the complete application.
- Fix remaining issues.
- Improve validation messages and error handling.
- Review security and authorization.
- Optimize important database queries.
- Prepare the database seed data.
- Prepare deployment configuration.
- Configure production settings and secrets.
- Deploy the application to an appropriate hosting environment.
- Update the GitHub README.
- Add screenshots and system documentation.
- Prepare a demonstration scenario.

**Deliverables:**
- Completed Orvanta application.
- Deployment-ready configuration.
- Updated GitHub repository.
- Technical documentation.
- Demonstration-ready system.

## 6. Module Dependencies

The development order must respect the following dependencies:

| Module | Depends On |
|---|---|
| Authentication | Identity and database foundation |
| Master Data | Database foundation |
| Inventory | Products, units, and warehouses |
| Sales | Customers, products, warehouses, and inventory |
| Purchases | Suppliers, products, warehouses, and inventory |
| Customer Receipts | Customers and posted sales invoices |
| Supplier Payments | Suppliers and posted purchase invoices |
| Expenses | Expense categories and user authorization |
| Reports | Completed business transactions |
| Dashboard | Reports and operational data |
| Audit Logs | User identity and business operations |

## 7. Development Standards

The following standards will be applied throughout implementation:

- Keep business logic outside controllers.
- Use dependency injection.
- Separate responsibilities between application components.
- Validate input on the server side.
- Use database transactions for critical business operations.
- Use decimal types for financial calculations.
- Apply appropriate foreign keys, indexes, and constraints.
- Protect sensitive operations with authorization.
- Avoid deleting historical business records.
- Maintain consistent naming and coding conventions.
- Document important architectural decisions.
- Use Git commits to track meaningful development changes.

## 8. Completion Criteria

A development phase is considered complete when:

1. Its agreed functionality has been implemented.
2. Business rules are enforced.
3. Required authorization is applied.
4. Data is stored and retrieved correctly.
5. Relevant validation and error handling are implemented.
6. Important scenarios have been verified.
7. The code is integrated with the rest of the application.
8. The phase deliverables are available for review.

## 9. Project Delivery

The final delivery should include:

- ASP.NET Core MVC source code.
- SQL Server database implementation.
- Entity Framework Core migrations.
- Authentication and authorization.
- Business management modules.
- Operational and financial management reports.
- Audit logging.
- Technical documentation.
- GitHub repository.
- Deployment-ready application.
