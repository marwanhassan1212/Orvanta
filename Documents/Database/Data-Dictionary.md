# Orvanta – Database Data Dictionary

## 1. Document Information

| Property | Value |
|---|---|
| Project | Orvanta |
| Document | Database Data Dictionary |
| Database | Microsoft SQL Server |
| Backend | ASP.NET Core MVC |
| Currency | EGP |
| Database Design | Relational Database |
| Version | 1.0 |

## 2. Database Overview

Orvanta uses a relational database to manage the daily operations of a medium-sized trading company.

The database covers:

- User accounts and role-based authorization.
- Products, categories, units, customers, and suppliers.
- Multiple warehouses and inventory tracking.
- Sales and purchase invoices.
- Invoice submission and approval workflows.
- Customer receipts and supplier payments.
- Business expenses.
- Audit logging and company settings.

### Important Business Rules

1. The system supports one company with multiple warehouses.
2. The default currency is EGP.
3. All payments and receipts are cash-based.
4. Taxes are excluded from the current scope.
5. Discounts are applied at invoice level.
6. Inventory uses the Weighted Average Cost method per warehouse.
7. Negative stock quantities are not allowed.
8. Inventory changes only through opening balances and posted purchase or sales invoices.
9. Sales and purchase invoices follow a Maker–Checker approval workflow.
10. Posted invoices cannot be edited.
11. Receipts and payments must be fully allocated to posted invoices before posting.
12. Financial reports are management reports, not full accounting or General Ledger statements.

---

# 3. Database Tables

## 3.1 Identity and User Management

These tables are used by ASP.NET Core Identity to manage authentication, user accounts, roles, and authorization.

### Table 1: AspNetUsers

**Purpose:** Stores employee and system user account information.

| Column | Data Type | Description |
|---|---|---|
| Id | nvarchar(450) | Unique user identifier (Primary Key). |
| UserName | nvarchar(256) | User login name. |
| NormalizedUserName | nvarchar(256) | Normalized username for case-insensitive lookup. |
| Email | nvarchar(256) | User email address. |
| NormalizedEmail | nvarchar(256) | Normalized email address. |
| EmailConfirmed | bit | Indicates whether the email is confirmed. |
| PasswordHash | nvarchar(max) | Hashed user password. |
| SecurityStamp | nvarchar(max) | Security-related user identifier. |
| ConcurrencyStamp | nvarchar(max) | Used for concurrency management. |
| PhoneNumber | nvarchar(max) | User phone number. |
| PhoneNumberConfirmed | bit | Indicates whether the phone number is confirmed. |
| TwoFactorEnabled | bit | Indicates whether two-factor authentication is enabled. |
| LockoutEnd | datetimeoffset | Date and time when account lockout ends. |
| LockoutEnabled | bit | Indicates whether account lockout is enabled. |
| AccessFailedCount | int | Number of failed login attempts. |
| FirstName | nvarchar(100) | User first name. |
| LastName | nvarchar(100) | User last name. |
| IsActive | bit | Indicates whether the account is active. |
| CreatedAt | datetime2 | Account creation date. |
| UpdatedAt | datetime2 | Last account update date. |

**Relationships:**
- One user can have multiple roles through AspNetUserRoles.
- One user can create invoices, receipts, payments, and expenses.
- One user can perform approval actions.
- One user can generate multiple audit log records.

### Table 2: AspNetRoles

**Purpose:** Stores the system roles used for authorization.

| Column | Data Type | Description |
|---|---|---|
| Id | nvarchar(450) | Unique role identifier (Primary Key). |
| Name | nvarchar(256) | Role name. |
| NormalizedName | nvarchar(256) | Normalized role name. |
| ConcurrencyStamp | nvarchar(max) | Used for concurrency management. |

**Main Roles:**
- Admin
- Manager
- Accountant
- SalesEmployee
- WarehouseEmployee

**Relationships:**
- One role can be assigned to multiple users through AspNetUserRoles.

### Table 3: AspNetUserRoles

**Purpose:** Connects users with their assigned roles.

| Column | Data Type | Description |
|---|---|---|
| UserId | nvarchar(450) | References AspNetUsers.Id. |
| RoleId | nvarchar(450) | References AspNetRoles.Id. |

**Primary Key:** UserId + RoleId.

**Relationships:**
- Many-to-many relationship between users and roles.

### Table 4: AspNetUserClaims

**Purpose:** Stores additional claims assigned to individual users.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique claim identifier (Primary Key). |
| UserId | nvarchar(450) | References AspNetUsers.Id. |
| ClaimType | nvarchar(max) | Type of the claim. |
| ClaimValue | nvarchar(max) | Value of the claim. |

### Table 5: AspNetRoleClaims

**Purpose:** Stores claims assigned to roles.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique claim identifier (Primary Key). |
| RoleId | nvarchar(450) | References AspNetRoles.Id. |
| ClaimType | nvarchar(max) | Type of the claim. |
| ClaimValue | nvarchar(max) | Value of the claim. |

### Table 6: AspNetUserLogins

**Purpose:** Stores external login provider information for users.

| Column | Data Type | Description |
|---|---|---|
| LoginProvider | nvarchar(128) | External authentication provider. |
| ProviderKey | nvarchar(128) | User identifier provided by the external provider. |
| ProviderDisplayName | nvarchar(max) | Display name of the external provider. |
| UserId | nvarchar(450) | References AspNetUsers.Id. |

**Primary Key:** LoginProvider + ProviderKey.

### Table 7: AspNetUserTokens

**Purpose:** Stores authentication tokens associated with users.

| Column | Data Type | Description |
|---|---|---|
| UserId | nvarchar(450) | References AspNetUsers.Id. |
| LoginProvider | nvarchar(128) | Token provider name. |
| Name | nvarchar(128) | Token name. |
| Value | nvarchar(max) | Token value. |

**Primary Key:** UserId + LoginProvider + Name.

---

## 3.2 Master Data Management

These tables store the main business entities used throughout the system.

### Table 8: Categories

**Purpose:** Organizes products into categories to simplify management and reporting.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique category identifier (Primary Key). |
| Name | nvarchar(150) | Category name. |
| Description | nvarchar(500) | Optional category description. |
| IsActive | bit | Indicates whether the category is active. |
| CreatedAt | datetime2 | Category creation date. |
| UpdatedAt | datetime2 | Last update date. |

**Relationships:**
- One category can contain multiple products.

### Table 9: Units

**Purpose:** Stores measurement units used for products.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique unit identifier (Primary Key). |
| Name | nvarchar(100) | Unit name, such as Piece or Box. |
| Symbol | nvarchar(20) | Short unit symbol. |
| IsActive | bit | Indicates whether the unit is active. |

**Relationships:**
- One unit can be assigned to multiple products.

### Table 10: Products

**Purpose:** Stores product information managed by the trading company.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique product identifier (Primary Key). |
| Name | nvarchar(200) | Product name. |
| SKU | nvarchar(100) | Unique product code. |
| Description | nvarchar(1000) | Product description. |
| CategoryId | int | References Categories.Id. |
| UnitId | int | References Units.Id. |
| PurchasePrice | decimal(18,2) | Reference purchase price. |
| SellingPrice | decimal(18,2) | Default selling price. |
| IsActive | bit | Indicates whether the product is active. |
| CreatedAt | datetime2 | Product creation date. |
| UpdatedAt | datetime2 | Last product update date. |

**Relationships:**
- Many products belong to one category.
- Many products use one measurement unit.
- One product can have multiple inventory stock records.
- One product can appear in multiple sales and purchase invoice items.

### Table 11: Customers

**Purpose:** Stores customer information for sales operations.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique customer identifier (Primary Key). |
| Name | nvarchar(200) | Customer name. |
| Phone | nvarchar(30) | Customer phone number. |
| Email | nvarchar(256) | Customer email address. |
| Address | nvarchar(500) | Customer address. |
| Notes | nvarchar(1000) | Additional customer information. |
| IsActive | bit | Indicates whether the customer is active. |
| CreatedAt | datetime2 | Customer creation date. |
| UpdatedAt | datetime2 | Last customer update date. |

**Relationships:**
- One customer can have multiple sales invoices.
- One customer can have multiple cash receipts.

### Table 12: Suppliers

**Purpose:** Stores supplier information for purchase operations.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique supplier identifier (Primary Key). |
| Name | nvarchar(200) | Supplier name. |
| Phone | nvarchar(30) | Supplier phone number. |
| Email | nvarchar(256) | Supplier email address. |
| Address | nvarchar(500) | Supplier address. |
| Notes | nvarchar(1000) | Additional supplier information. |
| IsActive | bit | Indicates whether the supplier is active. |
| CreatedAt | datetime2 | Supplier creation date. |
| UpdatedAt | datetime2 | Last supplier update date. |

**Relationships:**
- One supplier can have multiple purchase invoices.
- One supplier can have multiple supplier payments.

### Table 13: Warehouses

**Purpose:** Stores the company's warehouse locations.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique warehouse identifier (Primary Key). |
| Name | nvarchar(150) | Warehouse name. |
| Code | nvarchar(50) | Unique warehouse code. |
| Address | nvarchar(500) | Warehouse address. |
| IsActive | bit | Indicates whether the warehouse is active. |
| CreatedAt | datetime2 | Warehouse creation date. |
| UpdatedAt | datetime2 | Last warehouse update date. |

**Relationships:**
- One warehouse can contain multiple inventory stock records.
- One warehouse can be referenced by multiple sales and purchase invoices.

---

## 3.3 Inventory Management

These tables track product quantities and inventory movements across warehouses.

### Table 14: InventoryStocks

**Purpose:** Stores the current quantity and weighted average cost of each product in each warehouse.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique stock record identifier (Primary Key). |
| ProductId | int | References Products.Id. |
| WarehouseId | int | References Warehouses.Id. |
| Quantity | decimal(18,3) | Current available stock quantity. |
| AverageCost | decimal(18,4) | Current weighted average cost per unit. |
| RowVersion | rowversion | Used for optimistic concurrency control. |
| UpdatedAt | datetime2 | Last stock update date. |

**Constraints:**
- ProductId + WarehouseId must be unique.
- Quantity cannot be negative.
- AverageCost cannot be negative.

**Relationships:**
- Each stock record belongs to one product and one warehouse.

### Table 15: InventoryTransactions

**Purpose:** Records every inventory movement to maintain a historical inventory ledger.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique inventory transaction identifier (Primary Key). |
| ProductId | int | References Products.Id. |
| WarehouseId | int | References Warehouses.Id. |
| TransactionType | nvarchar(30) | OpeningBalance, PurchaseIn, or SaleOut. |
| Quantity | decimal(18,3) | Positive quantity involved in the movement. |
| UnitCost | decimal(18,4) | Cost per unit for the movement. |
| TotalCost | decimal(18,2) | Total cost of the inventory movement. |
| PurchaseInvoiceItemId | int | Optional reference to PurchaseInvoiceItems.Id. |
| SalesInvoiceItemId | int | Optional reference to SalesInvoiceItems.Id. |
| CreatedByUserId | nvarchar(450) | References AspNetUsers.Id. |
| CreatedAt | datetime2 | Transaction creation date. |

**Transaction Types:**
- OpeningBalance: Initial stock entry.
- PurchaseIn: Stock received from a posted purchase invoice.
- SaleOut: Stock deducted from a posted sales invoice.

**Business Rules:**
- Quantity must be greater than zero.
- OpeningBalance must not reference a sales or purchase invoice item.
- PurchaseIn must reference a purchase invoice item only.
- SaleOut must reference a sales invoice item only.
- Inventory movements are generated through authorized business workflows, not direct stock editing.

---

## 3.4 Sales Management

These tables manage customer sales invoices and their line items.

### Table 16: SalesInvoices

**Purpose:** Stores sales invoice header information and approval status.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique sales invoice identifier (Primary Key). |
| InvoiceNumber | nvarchar(50) | Unique sales invoice number. |
| CustomerId | int | References Customers.Id. |
| WarehouseId | int | References Warehouses.Id. |
| InvoiceDate | datetime2 | Invoice date. |
| Status | nvarchar(30) | Draft, PendingApproval, Posted, or Rejected. |
| Subtotal | decimal(18,2) | Total of invoice line amounts before discount. |
| DiscountAmount | decimal(18,2) | Invoice-level discount amount. |
| TotalAmount | decimal(18,2) | Final invoice amount after discount. |
| Notes | nvarchar(1000) | Optional invoice notes. |
| LastRejectionReason | nvarchar(1000) | Latest rejection reason, if applicable. |
| CreatedByUserId | nvarchar(450) | References AspNetUsers.Id; invoice creator. |
| ApprovedByUserId | nvarchar(450) | References AspNetUsers.Id; approving user. |
| SubmittedAt | datetime2 | Latest submission date. |
| ApprovedAt | datetime2 | Approval date. |
| CreatedAt | datetime2 | Invoice creation date. |
| UpdatedAt | datetime2 | Last invoice update date. |

**Relationships:**
- One customer can have multiple sales invoices.
- One warehouse can be referenced by multiple sales invoices.
- One sales invoice contains multiple sales invoice items.
- One sales invoice can receive multiple receipt allocations.

**Business Rules:**
- The creator cannot approve their own invoice.
- Only an authorized different user can approve it.
- Inventory is deducted only after successful posting.
- Posted invoices cannot be edited.
- Rejected invoices require a rejection reason.

### Table 17: SalesInvoiceItems

**Purpose:** Stores individual products and quantities included in a sales invoice.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique sales invoice item identifier (Primary Key). |
| SalesInvoiceId | int | References SalesInvoices.Id. |
| ProductId | int | References Products.Id. |
| ProductNameSnapshot | nvarchar(200) | Product name saved at invoice creation. |
| SKUSnapshot | nvarchar(100) | Product SKU saved at invoice creation. |
| Quantity | decimal(18,3) | Quantity sold. |
| UnitPrice | decimal(18,2) | Selling price per unit. |
| UnitCostSnapshot | decimal(18,4) | Weighted average cost per unit at posting time. |
| LineTotal | decimal(18,2) | Quantity multiplied by UnitPrice. |

**Relationships:**
- Each item belongs to one sales invoice.
- Each item references one product.
- An item can be referenced by an inventory SaleOut transaction.

**Business Rules:**
- UnitCostSnapshot is populated when the invoice is posted.
- LineTotal represents the line amount before invoice-level discount.
- Invoice-level discount is not stored separately on each line.

---

## 3.5 Purchase Management

These tables manage supplier purchase invoices and their line items.

### Table 18: PurchaseInvoices

**Purpose:** Stores purchase invoice header information and approval status.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique purchase invoice identifier (Primary Key). |
| InvoiceNumber | nvarchar(50) | Unique purchase invoice number. |
| SupplierId | int | References Suppliers.Id. |
| WarehouseId | int | References Warehouses.Id. |
| InvoiceDate | datetime2 | Invoice date. |
| Status | nvarchar(30) | Draft, PendingApproval, Posted, or Rejected. |
| Subtotal | decimal(18,2) | Total of invoice line amounts before discount. |
| DiscountAmount | decimal(18,2) | Invoice-level discount amount. |
| TotalAmount | decimal(18,2) | Final invoice amount after discount. |
| Notes | nvarchar(1000) | Optional invoice notes. |
| LastRejectionReason | nvarchar(1000) | Latest rejection reason, if applicable. |
| CreatedByUserId | nvarchar(450) | References AspNetUsers.Id; invoice creator. |
| ApprovedByUserId | nvarchar(450) | References AspNetUsers.Id; approving user. |
| SubmittedAt | datetime2 | Latest submission date. |
| ApprovedAt | datetime2 | Approval date. |
| CreatedAt | datetime2 | Invoice creation date. |
| UpdatedAt | datetime2 | Last invoice update date. |

**Relationships:**
- One supplier can have multiple purchase invoices.
- One warehouse can receive multiple purchase invoices.
- One purchase invoice contains multiple purchase invoice items.
- One purchase invoice can receive multiple supplier payment allocations.

**Business Rules:**
- The creator cannot approve their own invoice.
- Inventory increases only after successful posting.
- Posted invoices cannot be edited.
- Rejected invoices require a rejection reason.

### Table 19: PurchaseInvoiceItems

**Purpose:** Stores individual products and quantities included in a purchase invoice.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique purchase invoice item identifier (Primary Key). |
| PurchaseInvoiceId | int | References PurchaseInvoices.Id. |
| ProductId | int | References Products.Id. |
| ProductNameSnapshot | nvarchar(200) | Product name saved at invoice creation. |
| SKUSnapshot | nvarchar(100) | Product SKU saved at invoice creation. |
| Quantity | decimal(18,3) | Purchased quantity. |
| UnitPrice | decimal(18,2) | Supplier price per unit before allocated invoice discount. |
| AllocatedDiscount | decimal(18,2) | Portion of invoice discount allocated to this line. |
| LineTotal | decimal(18,2) | Net line amount after allocated discount. |
| UnitCost | decimal(18,4) | Effective unit cost after discount allocation. |

**Relationships:**
- Each item belongs to one purchase invoice.
- Each item references one product.
- An item can be referenced by an inventory PurchaseIn transaction.

**Business Rules:**
- Invoice-level discounts are allocated proportionally across purchase invoice items.
- Effective UnitCost is calculated using the net line amount divided by quantity.
- The effective cost is used when updating weighted average inventory cost.

---

## 3.6 Invoice Approval Management

### Table 20: InvoiceApprovalHistory

**Purpose:** Maintains a complete history of invoice submission, approval, and rejection attempts.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique approval history identifier (Primary Key). |
| InvoiceType | nvarchar(20) | Identifies Sales or Purchase invoice. |
| InvoiceId | int | Identifier of the related invoice. |
| Action | nvarchar(30) | Submitted, Approved, or Rejected. |
| PerformedByUserId | nvarchar(450) | References AspNetUsers.Id. |
| PerformedAt | datetime2 | Date and time of the action. |
| RejectionReason | nvarchar(1000) | Rejection reason when the action is Rejected. |

**Relationships:**
- Each history record references a user.
- InvoiceId identifies a sales or purchase invoice according to InvoiceType.

**Important Implementation Note:**
InvoiceId is a polymorphic reference. It cannot have a normal foreign key to both SalesInvoices and PurchaseInvoices simultaneously. The application must validate the invoice type and identifier.

**Business Rules:**
- Every submission attempt must be recorded.
- Every approval and rejection must be recorded.
- Rejection requires a reason.
- The latest rejection reason may also be stored on the invoice for convenient display.

---

## 3.7 Customer Receipt Management

### Table 21: CustomerReceipts

**Purpose:** Stores cash receipts collected from customers.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique receipt identifier (Primary Key). |
| ReceiptNumber | nvarchar(50) | Unique receipt number. |
| CustomerId | int | References Customers.Id. |
| ReceiptDate | datetime2 | Receipt date. |
| Amount | decimal(18,2) | Total cash received. |
| Status | nvarchar(30) | Draft, Posted, or Voided. |
| Notes | nvarchar(1000) | Optional receipt notes. |
| CreatedByUserId | nvarchar(450) | References AspNetUsers.Id. |
| PostedByUserId | nvarchar(450) | User who posted the receipt. |
| PostedAt | datetime2 | Receipt posting date. |
| VoidedByUserId | nvarchar(450) | User who voided the receipt. |
| VoidedAt | datetime2 | Receipt void date. |
| VoidReason | nvarchar(1000) | Reason for voiding the receipt. |
| CreatedAt | datetime2 | Receipt creation date. |

**Relationships:**
- One customer can have multiple receipts.
- One receipt can have multiple allocations.

### Table 22: ReceiptAllocations

**Purpose:** Connects customer receipts to the sales invoices they settle.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique allocation identifier (Primary Key). |
| CustomerReceiptId | int | References CustomerReceipts.Id. |
| SalesInvoiceId | int | References SalesInvoices.Id. |
| AllocatedAmount | decimal(18,2) | Amount allocated to the sales invoice. |

**Relationships:**
- One receipt can be allocated to multiple sales invoices.
- One sales invoice can receive allocations from multiple receipts.

**Business Rules:**
- Allocations must reference posted sales invoices.
- The receipt and invoice must belong to the same customer.
- Total allocations must equal the receipt amount before posting.
- Allocations cannot exceed the outstanding invoice balance.
- Customer advances and unallocated balances are excluded from the current scope.

---

## 3.8 Supplier Payment Management

### Table 23: SupplierPayments

**Purpose:** Stores cash payments made to suppliers.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique supplier payment identifier (Primary Key). |
| PaymentNumber | nvarchar(50) | Unique payment number. |
| SupplierId | int | References Suppliers.Id. |
| PaymentDate | datetime2 | Payment date. |
| Amount | decimal(18,2) | Total cash paid. |
| Status | nvarchar(30) | Draft, Posted, or Voided. |
| Notes | nvarchar(1000) | Optional payment notes. |
| CreatedByUserId | nvarchar(450) | References AspNetUsers.Id. |
| PostedByUserId | nvarchar(450) | User who posted the payment. |
| PostedAt | datetime2 | Payment posting date. |
| VoidedByUserId | nvarchar(450) | User who voided the payment. |
| VoidedAt | datetime2 | Payment void date. |
| VoidReason | nvarchar(1000) | Reason for voiding the payment. |
| CreatedAt | datetime2 | Payment creation date. |

**Relationships:**
- One supplier can have multiple payments.
- One payment can have multiple allocations.

### Table 24: PaymentAllocations

**Purpose:** Connects supplier payments to the purchase invoices they settle.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique allocation identifier (Primary Key). |
| SupplierPaymentId | int | References SupplierPayments.Id. |
| PurchaseInvoiceId | int | References PurchaseInvoices.Id. |
| AllocatedAmount | decimal(18,2) | Amount allocated to the purchase invoice. |

**Relationships:**
- One supplier payment can be allocated to multiple purchase invoices.
- One purchase invoice can receive allocations from multiple supplier payments.

**Business Rules:**
- Allocations must reference posted purchase invoices.
- The payment and invoice must belong to the same supplier.
- Total allocations must equal the payment amount before posting.
- Allocations cannot exceed the outstanding invoice balance.
- Supplier advances and unallocated balances are excluded from the current scope.

---

## 3.9 Expense Management

### Table 25: ExpenseCategories

**Purpose:** Organizes business expenses into categories for reporting.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique expense category identifier (Primary Key). |
| Name | nvarchar(150) | Expense category name. |
| Description | nvarchar(500) | Optional category description. |
| IsActive | bit | Indicates whether the category is active. |
| CreatedAt | datetime2 | Category creation date. |

**Relationships:**
- One expense category can contain multiple expenses.

### Table 26: Expenses

**Purpose:** Stores operational expenses incurred by the company.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Unique expense identifier (Primary Key). |
| ExpenseNumber | nvarchar(50) | Unique expense reference number. |
| ExpenseCategoryId | int | References ExpenseCategories.Id. |
| ExpenseDate | datetime2 | Expense date. |
| Description | nvarchar(500) | Description of the expense. |
| Amount | decimal(18,2) | Expense amount. |
| Status | nvarchar(30) | Draft, Posted, or Voided. |
| Notes | nvarchar(1000) | Optional additional notes. |
| CreatedByUserId | nvarchar(450) | References AspNetUsers.Id. |
| PostedByUserId | nvarchar(450) | User who posted the expense. |
| PostedAt | datetime2 | Expense posting date. |
| VoidedByUserId | nvarchar(450) | User who voided the expense. |
| VoidedAt | datetime2 | Expense void date. |
| VoidReason | nvarchar(1000) | Reason for voiding the expense. |
| CreatedAt | datetime2 | Expense creation date. |

**Relationships:**
- One expense belongs to one expense category.
- One expense can reference its creator and posting/voiding users.

**Business Rules:**
- Expense amount must be greater than zero.
- Only active expense categories can be used for new expenses.
- Only posted expenses are included in financial reports.
- Voiding an expense requires authorization and a reason.

---

## 3.10 System Management and Auditing

### Table 27: AuditLogs

**Purpose:** Records important system activities for accountability and traceability.

| Column | Data Type | Description |
|---|---|---|
| Id | bigint | Unique audit log identifier (Primary Key). |
| UserId | nvarchar(450) | Optional reference to AspNetUsers.Id. |
| Action | nvarchar(100) | Action performed by the user. |
| EntityName | nvarchar(150) | Name of the affected entity. |
| EntityId | nvarchar(100) | Identifier of the affected record. |
| OldValues | nvarchar(max) | Previous values serialized as JSON. |
| NewValues | nvarchar(max) | Updated values serialized as JSON. |
| IpAddress | nvarchar(45) | IP address associated with the request. |
| CreatedAt | datetime2 | Date and time of the activity. |

**Examples of Logged Activities:**
- Creating or updating a product.
- Changing user account status.
- Posting or voiding a receipt.
- Creating or updating business records.
- Important approval and administrative actions.

**Important Note:**
AuditLogs and InvoiceApprovalHistory serve different purposes. AuditLogs records system activity, while InvoiceApprovalHistory specifically tracks invoice workflow events.

### Table 28: CompanySettings

**Purpose:** Stores the main company information and system-wide settings.

| Column | Data Type | Description |
|---|---|---|
| Id | int | Settings record identifier (Primary Key). |
| CompanyName | nvarchar(200) | Company name. |
| Phone | nvarchar(30) | Company contact phone number. |
| Email | nvarchar(256) | Company email address. |
| Address | nvarchar(500) | Company address. |
| CurrencyCode | nvarchar(10) | Default currency code, EGP. |
| CreatedAt | datetime2 | Settings creation date. |
| UpdatedAt | datetime2 | Last settings update date. |

**Business Rules:**
- The system uses one company settings record.
- The default currency is EGP.
- The settings record is managed by authorized users.

---

# 4. Main Database Relationships

| Parent Table | Child Table | Relationship |
|---|---|---|
| Categories | Products | One-to-Many |
| Units | Products | One-to-Many |
| Products | InventoryStocks | One-to-Many |
| Warehouses | InventoryStocks | One-to-Many |
| Products | InventoryTransactions | One-to-Many |
| Warehouses | InventoryTransactions | One-to-Many |
| Customers | SalesInvoices | One-to-Many |
| Suppliers | PurchaseInvoices | One-to-Many |
| Warehouses | SalesInvoices | One-to-Many |
| Warehouses | PurchaseInvoices | One-to-Many |
| SalesInvoices | SalesInvoiceItems | One-to-Many |
| PurchaseInvoices | PurchaseInvoiceItems | One-to-Many |
| CustomerReceipts | ReceiptAllocations | One-to-Many |
| SalesInvoices | ReceiptAllocations | One-to-Many |
| SupplierPayments | PaymentAllocations | One-to-Many |
| PurchaseInvoices | PaymentAllocations | One-to-Many |
| ExpenseCategories | Expenses | One-to-Many |
| AspNetUsers | AspNetUserRoles | One-to-Many |
| AspNetRoles | AspNetUserRoles | One-to-Many |

---

# 5. Important Database Constraints

The following constraints must be implemented through database constraints, indexes, and application-level validation as appropriate.

| Area | Constraint |
|---|---|
| Products | SKU must be unique. |
| Categories | Category name must be unique. |
| Warehouses | Warehouse code must be unique. |
| Invoices | Invoice number must be unique. |
| Inventory | One stock record per product per warehouse. |
| Inventory | Stock quantity cannot be negative. |
| Inventory | Inventory transaction quantity must be positive. |
| Invoice Items | Prevent duplicate product lines within the same invoice. |
| Receipts | Receipt number must be unique. |
| Payments | Payment number must be unique. |
| Expenses | Expense number must be unique. |
| Allocations | Prevent duplicate allocation rows for the same document and invoice. |
| Financial Values | Use decimal types, never floating-point types. |
| Historical Data | Use deactivation instead of deleting records referenced by historical transactions. |

## 6. Financial and Inventory Calculations

### 6.1 Weighted Average Cost

The system calculates weighted average cost separately for each product in each warehouse.

\[
NewAverageCost =
\frac{(OldQuantity \times OldAverageCost)+(ReceivedQuantity \times EffectiveUnitCost)}
{OldQuantity+ReceivedQuantity}
\]

If the previous quantity is zero, the new average cost becomes the effective purchase unit cost.

When stock reaches zero, the last average cost is retained. The next receipt recalculates the cost using the new purchase cost.

### 6.2 Customer Outstanding Balance

Customer outstanding balance is calculated from posted sales invoices minus valid posted receipt allocations.

### 6.3 Supplier Outstanding Balance

Supplier outstanding balance is calculated from posted purchase invoices minus valid posted supplier payment allocations.

### 6.4 Gross Profit

\[
GrossProfit = NetSales - CostOfGoodsSold
\]

### 6.5 Operating Profit Estimate

\[
OperatingProfitEstimate = GrossProfit - PostedExpenses
\]

These calculations support management reporting and do not represent a complete accounting system.

---

# 7. Implementation Notes

1. Primary and foreign key relationships must be configured consistently in Entity Framework Core.
2. Financial values must use appropriate decimal precision.
3. Important unique constraints and indexes must be created in the database.
4. Inventory posting operations must execute within database transactions.
5. RowVersion must be used to help prevent concurrent stock updates.
6. Posted invoices, receipts, payments, and expenses must follow their defined immutability and voiding rules.
7. Invoice approval history must retain all submission, approval, and rejection attempts.
8. Historical business records must remain accessible even if related master data is deactivated.
9. Sensitive operations must be protected through authorization policies and service-level validation.
10. The database must not allow business workflows to bypass inventory and financial consistency rules.

---

**End of Document**