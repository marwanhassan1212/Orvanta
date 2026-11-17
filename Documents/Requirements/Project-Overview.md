# Orvanta – Project Overview

## 1. Introduction

Orvanta is a Business Management System designed for medium-sized trading companies to manage and monitor their daily business operations through a centralized web-based application.

The system provides a unified environment for managing products, categories, suppliers, customers, warehouses, inventory, sales invoices, purchase invoices, cash receipts, supplier payments, and operational expenses.

Orvanta aims to reduce manual work, minimize data inconsistencies, improve inventory visibility, and provide management with accurate operational and financial information.

## 2. Business Background

Many small and medium-sized trading companies rely on spreadsheets, paper documents, and disconnected tools to manage their daily operations.

This approach can lead to several business problems:

- Difficulty tracking product quantities across multiple warehouses.
- Inconsistent or duplicated business records.
- Difficulty monitoring customer outstanding balances.
- Difficulty tracking supplier payments and purchase obligations.
- Limited visibility into sales, purchases, expenses, and profitability.
- Lack of structured approval processes for important transactions.
- Difficulty identifying who performed or modified important operations.

Orvanta addresses these challenges by centralizing business data and introducing structured workflows.

## 3. Project Vision

To provide trading companies with a reliable and easy-to-use business management platform that improves operational efficiency, inventory accuracy, financial visibility, and accountability.

## 4. Project Objectives

The main objectives are:

1. Centralize the company's business information.
2. Organize product and master data management.
3. Track inventory quantities across multiple warehouses.
4. Manage sales and purchase operations through structured invoices.
5. Introduce an approval workflow to improve transaction control.
6. Track customer receipts and supplier payments.
7. Manage and categorize operational expenses.
8. Calculate customer and supplier outstanding balances.
9. Provide operational and financial management reports.
10. Maintain an audit trail for important system activities.
11. Apply role-based access control to protect business operations.

## 5. Target Users

Orvanta is intended for the following business roles:

| Role | Main Responsibility |
|---|---|
| Admin | Manages users, roles, and company settings. |
| Manager | Supervises business operations, approvals, and reports. |
| Accountant | Manages receipts, supplier payments, expenses, and financial reports. |
| Sales Employee | Manages customers and sales invoices. |
| Warehouse Employee | Manages suppliers, purchase invoices, and warehouse-related operations. |

Each role has access to the operations required for its responsibilities.

## 6. System Overview

Orvanta consists of the following main business modules:

### 6.1 User and Access Management
- User authentication.
- User account management.
- Role-based authorization.
- Account activation and deactivation.

### 6.2 Master Data Management
- Product categories.
- Measurement units.
- Products.
- Customers.
- Suppliers.
- Warehouses.

### 6.3 Inventory Management
- Inventory tracking by product and warehouse.
- Opening stock balances.
- Inventory transaction history.
- Weighted Average Cost calculation.
- Stock availability validation.
- Prevention of negative stock.

### 6.4 Sales Management
- Customer sales invoices.
- Invoice line items.
- Invoice submission and approval.
- Inventory deduction after posting.
- Customer sales history.

### 6.5 Purchase Management
- Supplier purchase invoices.
- Purchase invoice line items.
- Invoice submission and approval.
- Inventory increase after posting.
- Weighted Average Cost updates.

### 6.6 Financial Operations
- Customer cash receipts.
- Receipt allocations.
- Supplier cash payments.
- Payment allocations.
- Operational expense management.

### 6.7 Reporting and Analytics
- Sales reports.
- Purchase reports.
- Inventory reports.
- Customer outstanding balances.
- Supplier outstanding balances.
- Expense reports.
- Gross profit and estimated operating profit reports.

### 6.8 Auditing and Configuration
- Important activity logging.
- Invoice approval history.
- Company information and settings.

## 7. Technology Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core MVC |
| Programming Language | C# |
| ORM | Entity Framework Core |
| Database | Microsoft SQL Server |
| Authentication | ASP.NET Core Identity |
| Authorization | Role-Based Authorization and Policies |
| Frontend | Razor Views, HTML, CSS, JavaScript |
| Version Control | Git and GitHub |

## 8. Project Characteristics

- Web-based business management application.
- Single-company environment.
- Multiple warehouse support.
- Egyptian Pound (EGP) as the default currency.
- Cash-based financial transactions.
- Weighted Average Cost inventory valuation.
- Maker–Checker approval workflow.
- Role-based access control.
- Centralized relational database.

## 9. Expected Outcomes

After implementation, the company should be able to:

- Manage its main business records from one platform.
- Monitor stock quantities across its warehouses.
- Process sales and purchases with controlled approval.
- Track customer and supplier financial balances.
- Monitor operational expenses.
- Review business performance through reports.
- Trace important actions performed by system users.

## 10. Project Limitations

Orvanta is an operational business management system for a trading company. It is not intended to replace a complete Enterprise Resource Planning (ERP) or accounting platform.

Advanced accounting, tax management, and other excluded features are outside the current project scope.