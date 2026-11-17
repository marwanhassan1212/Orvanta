# User Roles and Permissions – Orvanta

## 1. Introduction

This document defines the user roles and their permissions within Orvanta.

The system uses role-based access control to ensure that employees can access only the operations required for their responsibilities.

## 2. User Roles

Orvanta defines the following roles:

### 2.1 Admin

The Admin has full system administration privileges.

Responsibilities:
- Manage system users.
- Assign and manage user roles.
- Manage company settings.
- Manage master data.
- Access all business modules.
- View audit logs.
- Perform authorized administrative operations.

Restrictions:
- The Admin cannot approve an invoice created by the same Admin account.

### 2.2 Manager

The Manager supervises business operations and monitors company performance.

Responsibilities:
- View the dashboard and management reports.
- Manage customers and suppliers.
- Manage products, categories, and units.
- Manage warehouses.
- Review sales and purchase operations.
- Approve or reject invoices when authorized.
- View inventory and financial reports.
- Supervise operational activities.

Restrictions:
- Cannot create or manage Admin accounts.
- Cannot change system-wide user role assignments.
- Cannot approve an invoice created by the same account.

### 2.3 Accountant

The Accountant manages financial operations.

Responsibilities:
- View customer and supplier records.
- View posted sales and purchase invoices.
- Create and manage customer receipts.
- Post customer receipts.
- Void customer receipts when authorized.
- Create and manage supplier payments.
- Post supplier payments.
- Void supplier payments when authorized.
- Create and manage expenses.
- Post and void expenses when authorized.
- View financial reports.
- View customer and supplier outstanding balances.

Restrictions:
- Cannot manage system users or roles.
- Cannot modify posted invoices.
- Cannot approve an invoice created by the same account.

### 2.4 Sales Employee

The Sales Employee handles customer-related sales operations.

Responsibilities:
- View customers.
- Create and update customer records.
- Create sales invoices.
- Edit Draft or Rejected sales invoices created by the employee.
- Submit sales invoices for approval.
- View sales invoice statuses.
- View relevant product and inventory information.

Restrictions:
- Cannot approve or reject invoices created by the same account.
- Cannot modify posted sales invoices.
- Cannot manage system users or roles.
- Cannot perform financial void operations unless separately authorized.

### 2.5 Warehouse Employee

The Warehouse Employee handles purchasing and inventory-related operations.

Responsibilities:
- View suppliers.
- Create and update supplier records.
- Create purchase invoices.
- Edit Draft or Rejected purchase invoices created by the employee.
- Submit purchase invoices for approval.
- View inventory quantities.
- View inventory transactions.
- Record opening inventory balances when authorized.
- View product and warehouse information.

Restrictions:
- Cannot approve or reject invoices created by the same account.
- Cannot modify posted purchase invoices.
- Cannot directly edit inventory quantities.
- Cannot manage system users or roles.

## 3. Permission Matrix

The following matrix defines the default permissions for each role.

| Module / Operation | Admin | Manager | Accountant | Sales Employee | Warehouse Employee |
|---|---|---|---|---|---|
| View Dashboard | Yes | Yes | Yes | Yes | Yes |
| Manage Users | Yes | No | No | No | No |
| Manage Company Settings | Yes | No | No | No | No |
| Manage Customers | Yes | Yes | View | Yes | View |
| Manage Suppliers | Yes | Yes | View | View | Yes |
| Manage Products | Yes | Yes | View | View | View |
| Manage Categories and Units | Yes | Yes | No | No | No |
| Manage Warehouses | Yes | Yes | View | View | View |
| View Inventory | Yes | Yes | Yes | Yes | Yes |
| Manage Opening Balances | Yes | Yes | No | No | Authorized |
| Create Sales Invoices | Yes | Yes | No | Yes | No |
| Submit Sales Invoices | Yes | Yes | No | Yes | No |
| Approve Sales Invoices | Yes | Yes | No | Authorized | Authorized |
| Create Purchase Invoices | Yes | Yes | No | No | Yes |
| Submit Purchase Invoices | Yes | Yes | No | No | Yes |
| Approve Purchase Invoices | Yes | Yes | No | Authorized | Authorized |
| Manage Customer Receipts | Yes | No | Yes | No | No |
| Manage Supplier Payments | Yes | No | Yes | No | No |
| Manage Expenses | Yes | No | Yes | No | No |
| View Financial Reports | Yes | Yes | Yes | No | No |
| View Sales Reports | Yes | Yes | Yes | Yes | No |
| View Purchase Reports | Yes | Yes | Yes | No | Yes |
| View Inventory Reports | Yes | Yes | Yes | No | Yes |
| View Audit Logs | Yes | No | No | No | No |

Note: "Authorized" means that the operation is available only to users who satisfy the applicable authorization rules and approval restrictions.

## 4. Invoice Approval Rules

### 4.1 Maker–Checker Principle

Sales and purchase invoices must follow the Maker–Checker principle.

The user who creates an invoice cannot approve or reject the same invoice.

### 4.2 Approval Requirements

- Only authorized users may approve or reject invoices.
- The approver must be different from the invoice creator.
- Rejection requires a reason.
- An invoice must be submitted before it can be reviewed.
- Approval results in posting only after all business validations succeed.
- Inventory changes occur only after successful posting.

### 4.3 Approval History

The system shall maintain a separate invoice approval history containing:
- Invoice type and identifier.
- Action performed.
- User who performed the action.
- Action timestamp.
- Rejection reason when applicable.

The history shall preserve all submission, approval, and rejection events, including repeated approval cycles.

## 5. Authorization Principles

### 5.1 Server-Side Enforcement

All protected operations shall be validated on the server side.

Hiding buttons or menu items shall not be considered sufficient authorization.

### 5.2 Role-Based Authorization

The system shall use ASP.NET Core Identity roles and ASP.NET Core authorization policies.

### 5.3 Ownership and Resource Validation

Where applicable, employees may edit only Draft or Rejected invoices they are authorized to manage.

The system shall validate access to individual business records rather than relying only on module-level permissions.

### 5.4 Separation of Duties

The system shall enforce the separation between invoice creators and approvers in the application business logic.

### 5.5 Administrative Restrictions

Only Admin users shall manage system-wide user roles and company settings.

## 6. Initial Release Authorization Scope

The initial release shall use predefined roles and permissions.

A custom dynamic permission-management system is excluded from the initial release.

Permissions may be refined during implementation when required by actual business workflows, without violating the core authorization rules.