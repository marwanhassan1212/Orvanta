# Orvanta – Functional Requirements

## 1. Introduction

This document defines the functional requirements of Orvanta.

Functional requirements describe the specific operations, services, and business behaviors that the system must provide to its users.

Each requirement is assigned a unique identifier to support development, traceability, and verification.

## 2. User and Access Management

### FR-001: User Login
The system shall allow registered users to log in using their credentials.

### FR-002: User Logout
The system shall allow authenticated users to securely log out.

### FR-003: User Management
The Admin shall be able to create, view, and update system user accounts.

### FR-004: User Activation
The Admin shall be able to activate or deactivate user accounts.

### FR-005: Role Assignment
The Admin shall be able to assign predefined roles to users.

### FR-006: Role-Based Authorization
The system shall restrict access to business operations according to the user's assigned roles and authorization policies.

### FR-007: Unauthorized Access Prevention
The system shall prevent unauthorized users from accessing restricted pages, actions, and business operations.

### FR-008: User Information
The system shall allow authorized users to view and update permitted user information.

## 3. Master Data Management

### FR-009: Category Management
Authorized users shall be able to create, view, update, and deactivate product categories.

### FR-010: Unit Management
Authorized users shall be able to create, view, update, and deactivate measurement units.

### FR-011: Product Management
Authorized users shall be able to create, view, update, and deactivate products.

### FR-012: Product Classification
The system shall associate each product with an existing category and measurement unit.

### FR-013: Product Identification
The system shall assign or maintain a unique SKU for each product.

### FR-014: Customer Management
Authorized users shall be able to create, view, update, and deactivate customer records.

### FR-015: Supplier Management
Authorized users shall be able to create, view, update, and deactivate supplier records.

### FR-016: Warehouse Management
Authorized users shall be able to create, view, update, and deactivate warehouse records.

### FR-017: Historical Data Preservation
The system shall prevent the deletion of master records that are referenced by historical business transactions.

## 4. Inventory Management

### FR-018: Inventory Tracking
The system shall maintain the current stock quantity of each product in each warehouse.

### FR-019: Opening Stock Balance
Authorized users shall be able to register initial stock quantities and their corresponding unit costs.

### FR-020: Inventory Transaction History
The system shall record inventory movements with their transaction type, quantity, cost, warehouse, and related source document when applicable.

### FR-021: Purchase Inventory Increase
The system shall increase product stock when a purchase invoice is successfully posted.

### FR-022: Sales Inventory Deduction
The system shall decrease product stock when a sales invoice is successfully posted.

### FR-023: Negative Stock Prevention
The system shall prevent inventory quantities from becoming negative.

### FR-024: Warehouse-Specific Stock
The system shall maintain separate inventory quantities and average costs for each product in each warehouse.

### FR-025: Weighted Average Cost
The system shall calculate the weighted average cost of products separately for each warehouse when new stock is received.

### FR-026: Sales Cost Snapshot
The system shall record the applicable weighted average unit cost for each sales invoice item when its invoice is posted.

### FR-027: Inventory Valuation
The system shall calculate inventory valuation using the current stock quantity and weighted average cost.

### FR-028: Concurrent Stock Updates
The system shall handle concurrent inventory updates to prevent inconsistent stock quantities and costs.

### FR-029: Inventory Movement Validation
The system shall validate that each inventory transaction has a valid source according to its transaction type.

### FR-030: Zero Stock Cost Preservation
The system shall retain the last known average cost when the stock quantity reaches zero.

### FR-031: Subsequent Stock Receipt
When a product has zero stock, the system shall use the new receipt's effective unit cost as the new average cost.

## 5. Sales Management

### FR-032: Create Sales Invoice
Authorized sales users shall be able to create sales invoices for existing customers.

### FR-033: Sales Invoice Items
The system shall allow users to add products, quantities, and selling prices to a sales invoice.

### FR-034: Sales Invoice Validation
The system shall validate required invoice information, product availability, valid quantities, and applicable prices.

### FR-035: Invoice Subtotal
The system shall calculate the sales invoice subtotal from its line totals.

### FR-036: Invoice Discount
The system shall allow authorized users to apply a discount at invoice level.

### FR-037: Invoice Total
The system shall calculate the final invoice total after applying the invoice-level discount.

### FR-038: Save Sales Draft
The system shall allow users to save sales invoices in Draft status.

### FR-039: Submit Sales Invoice
The system shall allow the invoice creator to submit a valid draft invoice for approval.

### FR-040: Approve Sales Invoice
The system shall allow an authorized user other than the creator to approve a submitted sales invoice.

### FR-041: Reject Sales Invoice
The system shall allow an authorized user to reject a submitted sales invoice with a mandatory rejection reason.

### FR-042: Sales Invoice Posting
The system shall post an approved sales invoice and deduct the required quantities from the selected warehouse.

### FR-043: Sales Invoice Cost Recording
The system shall record the applicable inventory cost for each sales invoice item at posting time.

### FR-044: Sales Invoice Cancellation
The system shall allow authorized users to cancel Draft sales invoices.

### FR-045: Posted Sales Invoice Protection
The system shall prevent direct modification or deletion of posted sales invoices.

### FR-046: Sales Invoice History
The system shall maintain the submission, approval, and rejection history of sales invoices.

### FR-047: Sales Invoice Viewing
Authorized users shall be able to view sales invoices and their associated line items.

### FR-048: Rejected Invoice Resubmission
The system shall allow the invoice creator to correct a rejected invoice and submit it again for approval, while preserving previous approval history.

## 6. Purchase Management

### FR-049: Create Purchase Invoice
Authorized warehouse or purchasing users shall be able to create purchase invoices for existing suppliers.

### FR-050: Purchase Invoice Items
The system shall allow users to add products, quantities, and supplier unit prices to a purchase invoice.

### FR-051: Purchase Invoice Validation
The system shall validate required invoice information, valid quantities, and applicable prices.

### FR-052: Purchase Invoice Subtotal
The system shall calculate the purchase invoice subtotal from its line totals before discount.

### FR-053: Purchase Invoice Discount
The system shall allow authorized users to apply a discount at invoice level.

### FR-054: Purchase Discount Allocation
The system shall allocate the invoice-level purchase discount proportionally across purchase invoice items.

### FR-055: Effective Purchase Unit Cost
The system shall calculate the effective unit cost of each purchase invoice item after discount allocation.

### FR-056: Purchase Invoice Total
The system shall calculate the final purchase invoice total after applying the invoice-level discount.

### FR-057: Save Purchase Draft
The system shall allow users to save purchase invoices in Draft status.

### FR-058: Submit Purchase Invoice
The system shall allow the invoice creator to submit a valid draft purchase invoice for approval.

### FR-059: Approve Purchase Invoice
The system shall allow an authorized user other than the creator to approve a submitted purchase invoice.

### FR-060: Reject Purchase Invoice
The system shall allow an authorized user to reject a submitted purchase invoice with a mandatory rejection reason.

### FR-061: Purchase Invoice Posting
The system shall post an approved purchase invoice and increase the stock quantities in the selected warehouse.

### FR-062: Average Cost Update
The system shall update the weighted average cost using the effective purchase unit cost when a purchase invoice is posted.

### FR-063: Purchase Invoice Cancellation
The system shall allow authorized users to cancel Draft purchase invoices.

### FR-064: Posted Purchase Invoice Protection
The system shall prevent direct modification or deletion of posted purchase invoices.

### FR-065: Purchase Invoice History
The system shall maintain the submission, approval, and rejection history of purchase invoices.

### FR-066: Rejected Purchase Resubmission
The system shall allow the invoice creator to correct a rejected purchase invoice and submit it again while preserving previous approval history.

## 7. Invoice Approval Workflow

### FR-067: Invoice Status Management
The system shall support the following invoice lifecycle:

Draft → PendingApproval → Posted or Rejected.

### FR-068: Maker–Checker Enforcement
The system shall prevent an invoice creator from approving their own invoice.

### FR-069: Authorized Approver Validation
The system shall verify that the approving user has the required authorization.

### FR-070: Approval History Recording
The system shall record every invoice submission, approval, and rejection attempt.

### FR-071: Rejection Reason
The system shall require a rejection reason whenever an invoice is rejected.

### FR-072: Approval Timestamps
The system shall record the date and time of invoice submission and approval.

### FR-073: Posting Integrity
The system shall ensure that inventory changes and invoice posting are performed consistently within a database transaction.

## 8. Customer Receipt Management

### FR-074: Create Customer Receipt
Authorized users shall be able to create cash receipts for existing customers.

### FR-075: Receipt Amount
The system shall require a valid positive receipt amount.

### FR-076: Receipt Allocation
The system shall allow a receipt to be allocated across one or more posted sales invoices belonging to the same customer.

### FR-077: Allocation Validation
The system shall prevent allocations that exceed the outstanding balance of a sales invoice.

### FR-078: Full Receipt Allocation
The system shall require the total allocated amount to equal the receipt amount before posting.

### FR-079: Post Customer Receipt
Authorized users shall be able to post valid customer receipts.

### FR-080: Void Customer Receipt
Authorized users shall be able to void posted customer receipts with a mandatory reason.

### FR-081: Customer Outstanding Balance
The system shall calculate each customer's outstanding balance based on posted sales invoices and valid posted receipt allocations.

### FR-082: Receipt History
The system shall allow authorized users to view customer receipt records and their allocations.

## 9. Supplier Payment Management

### FR-083: Create Supplier Payment
Authorized users shall be able to create cash payments for existing suppliers.

### FR-084: Payment Amount
The system shall require a valid positive supplier payment amount.

### FR-085: Payment Allocation
The system shall allow a supplier payment to be allocated across one or more posted purchase invoices belonging to the same supplier.

### FR-086: Payment Allocation Validation
The system shall prevent allocations that exceed the outstanding balance of a purchase invoice.

### FR-087: Full Payment Allocation
The system shall require the total allocated amount to equal the payment amount before posting.

### FR-088: Post Supplier Payment
Authorized users shall be able to post valid supplier payments.

### FR-089: Void Supplier Payment
Authorized users shall be able to void posted supplier payments with a mandatory reason.

### FR-090: Supplier Outstanding Balance
The system shall calculate each supplier's outstanding balance based on posted purchase invoices and valid posted payment allocations.

### FR-091: Supplier Payment History
The system shall allow authorized users to view supplier payment records and their allocations.

## 10. Expense Management

### FR-092: Expense Category Management
Authorized users shall be able to create, view, update, and deactivate expense categories.

### FR-093: Create Expense
Authorized users shall be able to create operational expense records.

### FR-094: Expense Validation
The system shall require a valid expense category, date, description, and positive amount.

### FR-095: Post Expense
Authorized users shall be able to post valid expense records.

### FR-096: Void Expense
Authorized users shall be able to void posted expenses with a mandatory reason.

### FR-097: Expense Reporting
The system shall include only posted and non-voided expenses in expense and profit reports.

### FR-098: Expense History
The system shall allow authorized users to view expense records and their statuses.

## 11. Reports and Analytics

### FR-099: Sales Reports
The system shall provide sales reports based on relevant date ranges and available filters.

### FR-100: Purchase Reports
The system shall provide purchase reports based on relevant date ranges and available filters.

### FR-101: Inventory Reports
The system shall provide inventory quantity and valuation reports by product and warehouse.

### FR-102: Customer Outstanding Reports
The system shall provide reports showing customer outstanding balances.

### FR-103: Supplier Outstanding Reports
The system shall provide reports showing supplier outstanding balances.

### FR-104: Receipt Reports
The system shall provide reports for posted and voided customer receipts.

### FR-105: Supplier Payment Reports
The system shall provide reports for posted and voided supplier payments.

### FR-106: Expense Reports
The system shall provide expense reports grouped by category and date.

### FR-107: Gross Profit Report
The system shall calculate gross profit as net sales minus the cost of goods sold.

### FR-108: Estimated Operating Profit Report
The system shall calculate estimated operating profit as gross profit minus posted operational expenses.

### FR-109: Report Filtering
The system shall allow authorized users to filter supported reports by relevant dates, products, warehouses, customers, suppliers, and categories.

### FR-110: Report Data Accuracy
The system shall calculate reports using valid posted transactions and exclude voided financial documents where applicable.

## 12. Audit and Company Settings

### FR-111: Audit Logging
The system shall record important user activities and business operations.

### FR-112: Audit Information
Audit records shall include the responsible user when available, action, affected entity, record identifier, and activity timestamp.

### FR-113: Change Tracking
The system shall store relevant previous and updated values for selected important operations.

### FR-114: Audit Log Viewing
Authorized users shall be able to view audit records according to their permissions.

### FR-115: Company Information
Authorized users shall be able to view and update basic company information.

### FR-116: Default Currency
The system shall use EGP as the default company currency.

---

## 13. Requirement Verification

Each functional requirement must be verified during implementation and testing.

Verification may include:

- Confirming that the requested operation is available to authorized users.
- Confirming that invalid data is rejected.
- Confirming that unauthorized operations are blocked.
- Confirming that database records are created and updated correctly.
- Confirming that business rules are enforced.
- Confirming that related inventory and financial calculations remain consistent.

## 14. Requirement Traceability

Requirements are grouped by business module and assigned unique identifiers.

The identifiers will be used to connect requirements with:

- Use cases.
- Database entities.
- Application services.
- Controllers and user interfaces.
- Test cases.
