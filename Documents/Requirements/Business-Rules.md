# Business Rules – Orvanta

## 1. General Business Rules

### BR-GEN-01 – Single Company
The system shall support one company in the initial release.

### BR-GEN-02 – Default Currency
All financial transactions shall be recorded in Egyptian Pounds (EGP).

### BR-GEN-03 – Cash Payments Only
The system shall support cash payments only in the initial release.

### BR-GEN-04 – No Tax Calculations
The system shall not apply tax calculations in the initial release.

### BR-GEN-05 – Historical Data Preservation
Business records shall remain available for historical reference even when related customers, suppliers, products, or warehouses are deactivated.

### BR-GEN-06 – No Hard Deletion of Historical Transactions
Posted financial and inventory transactions shall not be permanently deleted through normal application operations.

---

## 2. User and Authorization Rules

### BR-AUTH-01 – Active Users
Only active users shall be allowed to access the system.

### BR-AUTH-02 – Role-Based Access
Users shall perform operations according to their assigned roles.

### BR-AUTH-03 – Sensitive Operations
Sensitive operations shall be authorized on the server side, not only controlled by hiding interface elements.

### BR-AUTH-04 – Separation of Duties
The user who creates a sales or purchase invoice shall not approve the same invoice.

### BR-AUTH-05 – Authorized Approver
Only an authorized user different from the invoice creator may approve or reject the invoice.

### BR-AUTH-06 – Approval Rejection Reason
An invoice rejection shall require a non-empty reason.

---

## 3. Customer and Supplier Rules

### BR-PARTY-01 – Required Information
Customer and supplier names and phone numbers are required.

### BR-PARTY-02 – Unique Codes
Each customer and supplier shall have a unique system-generated code.

### BR-PARTY-03 – Deactivation
Customers and suppliers used in historical transactions shall be deactivated instead of permanently deleted.

### BR-PARTY-04 – Active Customer
New sales invoices shall only be created for active customers.

### BR-PARTY-05 – Active Supplier
New purchase invoices shall only be created for active suppliers.

---

## 4. Product and Catalog Rules

### BR-PROD-01 – Unique SKU
Each product shall have a unique SKU.

### BR-PROD-02 – Required Product Data
Each product shall have a name, SKU, category, unit, purchase price, and selling price.

### BR-PROD-03 – Non-Negative Prices
Purchase and selling prices shall not be negative.

### BR-PROD-04 – Active Products
Only active products shall be added to new invoice items.

### BR-PROD-05 – Product Deactivation
Products referenced by business transactions shall not be permanently deleted.

### BR-PROD-06 – Product Snapshot
Invoice items shall preserve relevant product information at the time of the transaction, including product name and SKU.

---

## 5. Warehouse and Inventory Rules

### BR-INV-01 – Multiple Warehouses
The system shall maintain inventory separately for each warehouse.

### BR-INV-02 – Unique Warehouse Stock
Each product shall have only one inventory stock record per warehouse.

### BR-INV-03 – No Negative Stock
Available stock shall never become negative.

### BR-INV-04 – Controlled Stock Movements
Stock quantities shall only change through approved inventory operations.

### BR-INV-05 – Purchase Stock Increase
Posting an approved purchase invoice shall increase the stock of its receiving warehouse.

### BR-INV-06 – Sales Stock Decrease
Posting an approved sales invoice shall decrease the stock of its selected warehouse.

### BR-INV-07 – Opening Balances
Initial inventory quantities may be recorded through opening balance transactions.

### BR-INV-08 – Inventory Transaction History
Every stock movement shall create a corresponding inventory transaction record.

### BR-INV-09 – Weighted Average Cost
Inventory costing shall use the weighted average cost method separately for each product in each warehouse.

### BR-INV-10 – Weighted Average Calculation
The new weighted average cost shall be calculated as follows:

New Average Cost =
((Previous Quantity × Previous Average Cost) + (Received Quantity × Purchase Unit Cost))
÷ (Previous Quantity + Received Quantity)

### BR-INV-11 – Zero Previous Quantity
If the previous stock quantity is zero, the new average cost shall equal the purchase unit cost.

### BR-INV-12 – Sales Cost Snapshot
When a sales invoice is posted, the system shall store the applicable inventory average cost for each sold product as its cost snapshot.

### BR-INV-13 – Inventory Concurrency
The system shall validate stock availability during posting and protect stock updates against concurrent operations.

### BR-INV-14 – Active Warehouse
New invoices shall use active warehouses. Warehouse availability shall also be validated before posting.

---

## 6. Sales Invoice Rules

### BR-SALES-01 – Invoice Lifecycle
Sales invoices shall follow this lifecycle:

Draft → PendingApproval → Posted

A submitted invoice may also be Rejected, corrected, and resubmitted. A Draft may be Cancelled.

### BR-SALES-02 – Invoice Number
Every sales invoice shall have a unique system-generated invoice number.

### BR-SALES-03 – Minimum Invoice Items
A sales invoice must contain at least one valid item before it can be submitted for approval.

### BR-SALES-04 – Positive Quantity
Every sales invoice item shall have a quantity greater than zero.

### BR-SALES-05 – Invoice Subtotal
The invoice subtotal shall equal the sum of its item totals.

### BR-SALES-06 – Invoice Discount
Only invoice-level discounts shall be supported.

### BR-SALES-07 – Discount Limit
The discount amount shall not exceed the invoice subtotal.

### BR-SALES-08 – Invoice Total
The invoice total shall equal:

Total Amount = Subtotal - Discount Amount

### BR-SALES-09 – Stock Availability
The system shall validate available stock before posting a sales invoice.

### BR-SALES-10 – Approval Before Posting
A sales invoice shall not affect inventory until it has been approved by a different authorized user and successfully posted.

### BR-SALES-11 – Atomic Posting
Sales invoice posting, inventory deduction, inventory transaction creation, and cost snapshot recording shall succeed or fail together.

### BR-SALES-12 – Posted Invoice Immutability
A posted sales invoice and its items shall not be edited or permanently deleted.

### BR-SALES-13 – Rejected Invoice Correction
A rejected invoice shall be returned to its creator for correction and resubmission.

### BR-SALES-14 – No Posted Invoice Cancellation
Posted sales invoices shall not be cancelled or voided in the initial release.

### BR-SALES-15 – Duplicate Product Lines
A product shall appear only once in an invoice. Additional quantities for the same product shall be merged into its existing line.

---

## 7. Purchase Invoice Rules

### BR-PUR-01 – Invoice Lifecycle
Purchase invoices shall follow this lifecycle:

Draft → PendingApproval → Posted

A submitted invoice may also be Rejected, corrected, and resubmitted. A Draft may be Cancelled.

### BR-PUR-02 – Internal Invoice Number
Every purchase invoice shall have a unique system-generated internal invoice number.

### BR-PUR-03 – Supplier Invoice Reference
A purchase invoice may include an external supplier invoice number.

### BR-PUR-04 – Minimum Invoice Items
A purchase invoice must contain at least one valid item before it can be submitted for approval.

### BR-PUR-05 – Positive Quantity
Every purchase invoice item shall have a quantity greater than zero.

### BR-PUR-06 – Purchase Cost
Each purchase invoice item shall have a non-negative purchase unit cost.

### BR-PUR-07 – Invoice Subtotal
The purchase invoice subtotal shall equal the sum of its item totals.

### BR-PUR-08 – Approval Before Posting
A purchase invoice shall not affect inventory until it has been approved by a different authorized user and successfully posted.

### BR-PUR-09 – Stock Increase
Posting a purchase invoice shall increase stock in the selected warehouse.

### BR-PUR-10 – Weighted Average Update
Posting a purchase invoice shall update the weighted average cost for the affected products in the selected warehouse.

### BR-PUR-11 – Atomic Posting
Purchase invoice posting, stock increase, inventory transaction creation, and weighted average cost updates shall succeed or fail together.

### BR-PUR-12 – Posted Invoice Immutability
A posted purchase invoice and its items shall not be edited or permanently deleted.

### BR-PUR-13 – Rejected Invoice Correction
A rejected invoice shall be returned to its creator for correction and resubmission.

### BR-PUR-14 – Duplicate Product Lines
A product shall appear only once in a purchase invoice. Additional quantities for the same product shall be merged into its existing line.

---

## 8. Financial Calculation Rules

### BR-FIN-01 – Monetary Precision
All monetary calculations shall use the decimal data type and shall be rounded to two decimal places.

### BR-FIN-02 – Rounding Strategy
The system shall use `MidpointRounding.AwayFromZero` when rounding monetary values to two decimal places.

### BR-FIN-03 – Consistent Rounding
The same rounding strategy shall be applied consistently across invoice calculations, receipts, supplier payments, expenses, and financial reports.

## 9. Customer Receipt Rules

### BR-REC-01 – Posted Invoice Allocation
Customer receipts may only be allocated to posted sales invoices.

### BR-REC-02 – Customer Matching
A receipt may only be allocated to sales invoices belonging to the same customer.

### BR-REC-03 – Multiple Invoice Allocation
A single receipt may be allocated across multiple sales invoices.

### BR-REC-04 – Multiple Receipts
A sales invoice may receive multiple customer receipts.

### BR-REC-05 – No Advance Payments
Unallocated customer advance payments shall not be supported in the initial release.

### BR-REC-06 – Exact Allocation
The total allocated amount shall equal the receipt amount before posting.

### BR-REC-07 – No Overpayment
The allocated amount for an invoice shall not exceed its outstanding balance.

### BR-REC-08 – Receipt Lifecycle
Customer receipts shall follow this lifecycle:

Draft → Posted → Voided

### BR-REC-09 – Receipt Voiding
A posted receipt may only be voided by an authorized user with a mandatory reason.

### BR-REC-10 – Outstanding Balance
Customer outstanding balances shall be recalculated based on posted sales invoices and posted receipt allocations, excluding voided receipts.

---

## 10. Supplier Payment Rules

### BR-PAY-01 – Posted Invoice Allocation
Supplier payments may only be allocated to posted purchase invoices.

### BR-PAY-02 – Supplier Matching
A supplier payment may only be allocated to purchase invoices belonging to the same supplier.

### BR-PAY-03 – Multiple Invoice Allocation
A single supplier payment may be allocated across multiple purchase invoices.

### BR-PAY-04 – Multiple Payments
A purchase invoice may receive multiple supplier payments.

### BR-PAY-05 – No Advance Payments
Unallocated supplier advance payments shall not be supported in the initial release.

### BR-PAY-06 – Exact Allocation
The total allocated amount shall equal the supplier payment amount before posting.

### BR-PAY-07 – No Overpayment
The allocated amount for a purchase invoice shall not exceed its outstanding balance.

### BR-PAY-08 – Supplier Payment Lifecycle
Supplier payments shall follow this lifecycle:

Draft → Posted → Voided

### BR-PAY-09 – Payment Voiding
A posted supplier payment may only be voided by an authorized user with a mandatory reason.

### BR-PAY-10 – Supplier Outstanding Balance
Supplier outstanding balances shall be calculated using posted purchase invoices and posted supplier payment allocations, excluding voided payments.

---

## 11. Expense Management Rules

### BR-EXP-01 – Expense Category
Every expense shall belong to an active expense category.

### BR-EXP-02 – Positive Amount
Expense amounts shall be greater than zero.

### BR-EXP-03 – Expense Lifecycle
Expenses shall follow this lifecycle:

Draft → Posted → Voided

### BR-EXP-04 – Expense Posting
Only posted expenses shall be included in financial reports.

### BR-EXP-05 – Expense Voiding
A posted expense may only be voided by an authorized user with a mandatory reason.

---

## 12. Financial Reporting Rules

### BR-REP-01 – Customer Outstanding
Customer outstanding balance shall equal:

Posted Sales Invoice Totals - Posted Customer Receipt Allocations

### BR-REP-02 – Supplier Outstanding
Supplier outstanding balance shall equal:

Posted Purchase Invoice Totals - Posted Supplier Payment Allocations

### BR-REP-03 – Gross Profit
Estimated gross profit shall equal:

Net Sales - Cost of Goods Sold

### BR-REP-04 – Operating Profit Estimate
Estimated operating profit shall equal:

Estimated Gross Profit - Posted Expenses

### BR-REP-05 – Reporting Limitations
Financial reports are management reports and do not represent a complete accounting ledger or statutory financial statements.

### BR-REP-06 – Voided Transactions
Voided receipts, supplier payments, and expenses shall be excluded from relevant financial calculations.

---

## 13. Audit Rules

### BR-AUD-01 – Important Activity Logging
Important business operations shall be recorded in the audit log.

### BR-AUD-02 – Audit Information
Audit records should include the responsible user, action, affected entity, timestamp, and relevant change information.

### BR-AUD-03 – Audit Immutability
Audit records shall not be modified or deleted through normal application operations.

### BR-AUD-04 – Successful Operation Consistency
Audit records for critical business operations shall remain consistent with the success or failure of the related operation.

---

## 14. Explicitly Deferred Business Rules

The following operations are excluded from the initial release:

- Sales returns and refunds.
- Purchase returns.
- Warehouse transfers.
- Manual inventory adjustments after opening balances.
- Negative inventory.
- Multi-currency transactions.
- Tax calculations.
- Online payment processing.
- Multi-company operations.
- General ledger and double-entry accounting.
- Posted invoice reversal or cancellation.