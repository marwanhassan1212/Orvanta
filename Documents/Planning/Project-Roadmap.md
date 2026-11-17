# Orvanta – Project Roadmap

## 1. Introduction

This document presents the high-level development roadmap for the Orvanta Business Management System.

It defines the major milestones, implementation sequence, expected outcomes, and dependencies required to deliver the project.

The roadmap provides a clear overview of the project's progress from initial preparation to final delivery.

## 2. Roadmap Overview

The project is divided into eight major milestones.

| Milestone | Name | Main Outcome |
|---|---|---|
| M1 | Project Foundation | Prepared solution and architecture |
| M2 | Database and Security | Implemented database and authentication |
| M3 | Master Data | Completed core business records |
| M4 | Inventory | Working inventory and costing |
| M5 | Sales and Purchasing | Completed invoice workflows |
| M6 | Financial Operations | Receipts, payments, and expenses |
| M7 | Reporting and Auditing | Management reports and activity tracking |
| M8 | Quality and Delivery | Verified and deployment-ready application |

## 3. Development Roadmap

### M1 – Project Foundation

**Objective:** Establish the technical and documentation foundation.

**Activities:**
- Finalize project requirements.
- Review project scope and business rules.
- Review the architecture design.
- Review the database ERD.
- Create the ASP.NET Core MVC solution.
- Establish the project structure.
- Configure GitHub and development tools.

**Expected Outcome:**
A prepared application foundation with approved project documentation.

**Completion Criteria:**
- Solution runs successfully.
- Repository is initialized.
- Project structure is established.
- Architecture and database design are reviewed.

### M2 – Database and Security

**Objective:** Build the database and implement secure access.

**Activities:**
- Implement database entities.
- Configure Entity Framework Core.
- Create database migrations.
- Configure ASP.NET Core Identity.
- Implement authentication.
- Implement user management.
- Implement predefined roles.
- Apply authorization policies.

**Expected Outcome:**
A working database foundation and secure user access system.

**Completion Criteria:**
- Database is created successfully.
- Identity authentication works.
- Roles are seeded.
- Restricted operations are protected.

### M3 – Master Data Management

**Objective:** Implement the main records required for business operations.

**Activities:**
- Category management.
- Unit management.
- Product management.
- Customer management.
- Supplier management.
- Warehouse management.

**Expected Outcome:**
A centralized and functional master data management system.

**Completion Criteria:**
- Required records can be created and managed.
- Validation is applied.
- Unique identifiers are enforced.
- Historical records are protected.

### M4 – Inventory Management

**Objective:** Implement accurate inventory tracking and valuation.

**Activities:**
- Opening stock balances.
- Inventory stock records.
- Inventory transaction history.
- Warehouse-specific inventory.
- Weighted Average Cost.
- Stock validation.
- Negative stock prevention.
- Concurrency handling.

**Expected Outcome:**
A working inventory system that tracks product quantities and costs across warehouses.

**Completion Criteria:**
- Opening balances work correctly.
- Inventory movements are recorded.
- Average cost calculations are verified.
- Negative stock is prevented.
- Concurrent updates are handled safely.

### M5 – Sales and Purchasing

**Objective:** Implement the main trading operations.

**Sales Activities:**
- Sales invoice creation.
- Invoice item management.
- Invoice discounts.
- Submission and approval.
- Rejection and resubmission.
- Inventory deduction.
- Sales cost snapshots.

**Purchasing Activities:**
- Purchase invoice creation.
- Invoice item management.
- Invoice discounts.
- Discount allocation.
- Submission and approval.
- Rejection and resubmission.
- Inventory receiving.
- Average cost updates.

**Expected Outcome:**
Complete sales and purchasing workflows integrated with inventory.

**Completion Criteria:**
- Invoice lifecycle works correctly.
- Maker–Checker rules are enforced.
- Inventory changes only after posting.
- Purchase costing is correct.
- Posted invoices cannot be modified.

### M6 – Financial Operations

**Objective:** Implement customer collections, supplier payments, and operational expenses.

**Activities:**
- Customer receipts.
- Receipt allocations.
- Receipt posting and voiding.
- Supplier payments.
- Payment allocations.
- Payment posting and voiding.
- Expense categories.
- Expense management.
- Customer and supplier outstanding balances.

**Expected Outcome:**
A functional cash-based financial operations module.

**Completion Criteria:**
- Receipt allocations are validated.
- Payment allocations are validated.
- Full allocation is required before posting.
- Voiding requires authorization and a reason.
- Outstanding balances are calculated correctly.

### M7 – Reporting and Auditing

**Objective:** Provide business visibility and operational accountability.

**Activities:**
- Sales reports.
- Purchase reports.
- Inventory reports.
- Inventory valuation.
- Customer outstanding reports.
- Supplier outstanding reports.
- Expense reports.
- Gross profit reports.
- Estimated operating profit reports.
- Management dashboard.
- Audit logging.
- Company settings.

**Expected Outcome:**
A centralized management dashboard with reliable operational and financial reports.

**Completion Criteria:**
- Reports use valid transaction data.
- Date and relevant filters work.
- Voided records are excluded where applicable.
- Profit calculations are verified.
- Important activities are logged.

### M8 – Quality Assurance and Delivery

**Objective:** Verify the complete system and prepare it for release.

**Activities:**
- Test critical business workflows.
- Write and execute automated tests.
- Perform integration testing.
- Perform manual end-to-end testing.
- Fix defects.
- Review security and authorization.
- Review database performance.
- Prepare deployment configuration.
- Update GitHub documentation.
- Prepare screenshots and demonstration data.

**Expected Outcome:**
A stable, documented, and demonstration-ready Orvanta application.

**Completion Criteria:**
- Critical business scenarios pass verification.
- Important defects are resolved.
- Security rules are reviewed.
- Deployment configuration is prepared.
- Documentation is updated.
- GitHub repository is ready for portfolio presentation.

## 4. Milestone Dependencies

The roadmap follows this dependency sequence:

```text
M1: Project Foundation
          |
          v
M2: Database and Security
          |
          v
M3: Master Data Management
          |
          v
M4: Inventory Management
          |
          v
M5: Sales and Purchasing
          |
          v
M6: Financial Operations
          |
          v
M7: Reporting and Auditing
          |
          v
M8: Quality Assurance and Delivery
```

Some implementation activities may overlap when their dependencies are satisfied, but critical business modules must follow the defined sequence.

## 5. Major Project Milestones

| Milestone | Expected Result |
|---|---|
| M1 Completed | Project foundation ready |
| M2 Completed | Database and authentication working |
| M3 Completed | Core business data available |
| M4 Completed | Inventory calculations working |
| M5 Completed | Sales and purchase workflows working |
| M6 Completed | Cash and expense operations working |
| M7 Completed | Reports and audit functionality available |
| M8 Completed | Application verified and ready for delivery |

## 6. Progress Tracking

Each milestone will be tracked using the following statuses:

- Not Started
- In Progress
- Under Review
- Completed
- Blocked

A milestone must not be marked as Completed until its completion criteria have been verified.

## 7. Risk Management

| Risk | Potential Impact | Mitigation |
|---|---|---|
| Incorrect database relationships | Data integrity problems | Review ERD and constraints before implementation. |
| Inventory calculation errors | Incorrect stock valuation | Verify costing formulas with realistic scenarios. |
| Concurrent stock updates | Inconsistent inventory | Use database transactions and RowVersion. |
| Unauthorized invoice approval | Business control violations | Enforce Maker–Checker rules in backend services. |
| Incorrect payment allocations | Incorrect outstanding balances | Validate allocations before posting. |
| Uncontrolled feature expansion | Project delays | Follow the approved project scope. |
| Insufficient testing | Undetected business defects | Verify critical workflows throughout development. |
| Poor query performance | Slow reports and screens | Use suitable indexes and review database queries. |

## 8. Roadmap Completion

The Orvanta roadmap is considered complete when all approved milestones have been delivered, critical business workflows have been verified, and the application is ready for demonstration or deployment.

---

**End of Document**