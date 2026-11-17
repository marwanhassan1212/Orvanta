# Orvanta – Project Scope

## 1. Introduction

This document defines the boundaries of the Orvanta project by identifying the features, business processes, and capabilities included in the current implementation, as well as the features explicitly excluded from the project.

The purpose is to maintain a clear and realistic development scope and prevent uncontrolled feature expansion.

## 2. Project Scope Statement

Orvanta will provide a centralized business management solution for a medium-sized trading company.

The system will manage products, customers, suppliers, warehouses, inventory, sales, purchases, cash receipts, supplier payments, expenses, approvals, and management reporting.

The application will operate within a single-company environment and support multiple warehouses.

## 3. In-Scope Features

The following features are included in the current project.

### 3.1 User and Access Management
- User login and logout.
- User account management.
- User activation and deactivation.
- Role assignment.
- Role-based access control.
- Authorization of sensitive business operations.

### 3.2 Master Data Management
- Product category management.
- Measurement unit management.
- Product management.
- Customer management.
- Supplier management.
- Warehouse management.
- Activation and deactivation of master records.

### 3.3 Inventory Management
- Multiple warehouse support.
- Product stock tracking per warehouse.
- Opening stock balance registration.
- Inventory transaction history.
- Weighted Average Cost calculation per warehouse.
- Stock availability validation.
- Prevention of negative inventory.
- Concurrency handling during stock updates.
- Automatic stock updates after invoice posting.

### 3.4 Sales Management
- Creating sales invoices.
- Adding and managing invoice line items.
- Selecting customers and warehouses.
- Applying invoice-level discounts.
- Saving invoices as drafts.
- Submitting invoices for approval.
- Approving or rejecting submitted invoices.
- Recording approval history.
- Deducting inventory after successful posting.
- Preventing modifications to posted invoices.
- Cancelling draft invoices.

### 3.5 Purchase Management
- Creating purchase invoices.
- Selecting suppliers and warehouses.
- Adding purchased products and quantities.
- Applying invoice-level discounts.
- Saving invoices as drafts.
- Submitting invoices for approval.
- Approving or rejecting submitted invoices.
- Recording approval history.
- Allocating purchase discounts proportionally across invoice items for inventory costing.
- Increasing inventory after successful posting.
- Updating weighted average cost.
- Preventing modifications to posted invoices.
- Cancelling draft invoices.

### 3.6 Customer Receipt Management
- Creating cash receipts.
- Selecting customers.
- Allocating receipts to posted sales invoices.
- Supporting multiple invoice allocations per receipt.
- Tracking receipt status.
- Posting receipts.
- Voiding posted receipts with authorization and a reason.
- Calculating customer outstanding balances.

### 3.7 Supplier Payment Management
- Creating cash payments to suppliers.
- Selecting suppliers.
- Allocating payments to posted purchase invoices.
- Supporting multiple invoice allocations per payment.
- Tracking payment status.
- Posting payments.
- Voiding posted payments with authorization and a reason.
- Calculating supplier outstanding balances.

### 3.8 Expense Management
- Managing expense categories.
- Creating operational expenses.
- Categorizing expenses.
- Posting expenses.
- Voiding posted expenses with authorization and a reason.
- Including posted expenses in management reports.

### 3.9 Reporting and Analytics
- Sales reports.
- Purchase reports.
- Inventory quantity reports.
- Inventory valuation reports.
- Customer outstanding reports.
- Supplier outstanding reports.
- Cash receipt reports.
- Supplier payment reports.
- Expense reports.
- Gross profit reports.
- Estimated operating profit reports.

### 3.10 Auditing and Company Settings
- Logging important system activities.
- Recording relevant old and new values.
- Tracking invoice approval and rejection history.
- Managing basic company information.
- Configuring the default currency.

## 4. Out-of-Scope Features

The following features are explicitly excluded from the current implementation.

| Feature | Reason for Exclusion |
|---|---|
| Multi-company management | Current system supports one company. |
| Multi-currency | EGP is the only supported currency. |
| Tax management | Taxes are excluded from the current business scope. |
| Online payment gateways | Financial operations are cash-based. |
| Sales returns | Deferred to a future version. |
| Purchase returns | Deferred to a future version. |
| Warehouse transfers | Deferred to a future version. |
| Advanced accounting / General Ledger | The system provides management reports, not full accounting. |
| Double-entry bookkeeping | Not part of the current implementation. |
| Bank reconciliation | Cash-based operations only. |
| Payroll management | Not required for the current business scenario. |
| Employee attendance | Not required for the current business scenario. |
| Supplier/customer portals | External self-service access is not included. |
| Mobile applications | The current deliverable is a web application. |
| Advanced dynamic permission management | The current version uses predefined roles and authorization policies. |
| Automated stock adjustments | Inventory changes are limited to opening balances and posted purchase/sales invoices. |
| Barcode and QR scanning | Deferred to a future version. |
| AI-based forecasting | Not included in the current implementation. |

## 5. Business Constraints and Assumptions

### 5.1 Business Constraints

1. The system operates for one company.
2. The company can manage multiple warehouses.
3. The default currency is EGP.
4. All receipts and payments are cash-based.
5. Negative stock is prohibited.
6. Inventory valuation uses Weighted Average Cost per warehouse.
7. Invoice discounts are applied at invoice level.
8. Posted sales and purchase invoices cannot be edited.
9. Invoice creators cannot approve their own invoices.
10. Financial reports are management-oriented and not statutory accounting statements.

### 5.2 Assumptions

1. Company employees have individual user accounts.
2. Each employee is assigned one or more authorized system roles.
3. The company maintains accurate product, supplier, and customer information.
4. Opening stock balances are entered by authorized employees during initial setup.
5. Business users have access to a modern web browser and an internet or local network connection.
6. The company follows the defined approval process before invoices affect inventory.

## 6. Project Boundaries

### 6.1 Organizational Boundary

The system is designed for a single medium-sized trading company and its employees.

### 6.2 Operational Boundary

The system covers the operational lifecycle of:

- Product and inventory management.
- Purchasing from suppliers.
- Selling to customers.
- Collecting customer payments.
- Paying suppliers.
- Recording operational expenses.
- Reviewing business reports.

### 6.3 Technical Boundary

The project will be implemented as a web application using ASP.NET Core MVC, Entity Framework Core, and Microsoft SQL Server.

## 7. Scope Change Management

Any feature not explicitly listed as in-scope must be evaluated before implementation.

Changes to the project scope should consider:

- Business value.
- Implementation complexity.
- Impact on existing workflows.
- Database and architecture impact.
- Available development time.
- Testing and maintenance requirements.

Features must not be added solely to increase the number of modules or database tables.
