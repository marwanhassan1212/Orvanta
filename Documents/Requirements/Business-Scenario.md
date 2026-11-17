# Business Scenario – Orvanta

## 1. Company Background

Orvanta is designed for a medium-sized trading company that purchases products from different suppliers and resells them to customers.

The company operates through multiple warehouses and has several employees responsible for sales, purchasing, accounting, inventory, and management.

Currently, the company relies on spreadsheets and manual processes to manage its daily operations.

The company needs a centralized system to organize its business activities and maintain accurate records.

## 2. Current Business Challenges

The company faces several operational challenges:

- Inventory information is maintained manually.
- Tracking stock across multiple warehouses is difficult.
- Sales and purchase records are stored separately.
- Customer outstanding balances are difficult to monitor.
- Supplier payment records require manual tracking.
- Management reports take time to prepare.
- Employees may make mistakes when updating spreadsheets.
- There is no centralized history of important business activities.

## 3. Proposed Business Workflow

### 3.1 Purchasing Process

### 3.1 Purchasing Process

1. The company selects an existing supplier.
2. An authorized employee creates a purchase invoice.
3. The employee selects the receiving warehouse.
4. Products, quantities, and purchase costs are added to the invoice.
5. The invoice is saved as a Draft.
6. The creator submits the invoice for approval.
7. A different authorized employee reviews the invoice.
8. The approver can approve or reject the invoice.
9. If rejected, the invoice is returned to the creator with a rejection reason for correction and resubmission.
10. If approved, the invoice is posted.
11. The system increases the warehouse stock.
12. The system updates the weighted average inventory cost.
13. The system records inventory transactions.
14. The system stores the creator and approver information.
15. The purchase invoice becomes immutable after posting.

**Approval Rules:**
- The creator cannot approve their own invoice.
- The approver must be a different authorized user.
- Rejection requires a reason.
- Inventory is not affected before successful approval and posting.

### 3.2 Inventory Management Process

1. Products are stored across multiple warehouses.
2. The system maintains stock quantities for each product in each warehouse.
3. Inventory increases through posted purchase invoices and opening balances.
4. Inventory decreases through posted sales invoices.
5. The system prevents stock from becoming negative.
6. Inventory movements are recorded for traceability.
7. The system calculates weighted average inventory cost separately for each warehouse.

### 3.3 Sales Process

1. An employee selects an existing customer.
2. The employee selects the warehouse from which products will be sold.
3. Products, quantities, and selling prices are added to the invoice.
4. The system checks available stock.
5. The invoice is saved as a Draft.
6. The creator submits the invoice for approval.
7. A different authorized employee reviews the invoice.
8. The approver can approve or reject the invoice.
9. If rejected, the invoice is returned to the creator with a rejection reason for correction and resubmission.
10. If approved, the invoice is posted.
11. The system decreases warehouse stock.
12. The system calculates and stores the cost of goods sold.
13. The system records inventory transactions.
14. The system stores the creator and approver information.
15. The posted invoice becomes immutable.

**Approval Rules:**
- The creator cannot approve their own invoice.
- The approver must be a different authorized user.
- Rejection requires a reason.
- Inventory is not affected before successful approval and posting.

### 3.4 Customer Receipts Process

1. The company receives cash from a customer.
2. An authorized employee creates a customer receipt.
3. The employee selects the customer.
4. The employee allocates the receipt amount to one or more posted sales invoices.
5. The system validates the allocated amounts.
6. The receipt is posted.
7. The system updates the outstanding balances.

### 3.5 Supplier Payments Process

1. The company prepares a cash payment to a supplier.
2. An authorized employee creates a supplier payment.
3. The employee selects the supplier.
4. The payment is allocated to one or more posted purchase invoices.
5. The system validates the allocated amounts.
6. The payment is posted.
7. The system updates the outstanding supplier balances.

### 3.6 Expense Management Process

1. An authorized employee records a business expense.
2. The employee selects an expense category.
3. The employee enters the amount, date, and description.
4. The expense is posted.
5. The system includes the expense in relevant financial reports.

### 3.7 Reporting Process

Management and authorized employees can access reports such as:

- Sales reports.
- Purchase reports.
- Inventory reports.
- Low-stock reports.
- Customer outstanding balances.
- Supplier outstanding balances.
- Cash receipts and payments.
- Expense reports.
- Estimated gross profit.
- Estimated operating profit.

## 4. Employee Responsibilities

The company has different employee roles:

- Admin: manages system users and overall settings.
- Manager: supervises business operations and reviews reports.
- Accountant: manages receipts, supplier payments, and expenses.
- Sales Employee: manages customers and sales invoices.
- Warehouse Employee: manages purchasing and inventory-related operations.

Access to operations is controlled by assigned roles and permissions.

## 5. Important Business Characteristics

- The company operates as a single business entity.
- The company uses multiple warehouses.
- All financial transactions are recorded in EGP.
- All payments are cash-based.
- The company does not use online payment gateways.
- The initial release does not include tax calculations.
- Inventory costing uses the Weighted Average Cost method.
- Negative inventory is not allowed.
- Posted invoices cannot be edited or deleted.

## 6. Expected Business Benefits

Orvanta is expected to:

- Reduce manual administrative work.
- Improve inventory accuracy.
- Organize sales and purchasing operations.
- Provide clearer customer and supplier balances.
- Improve financial visibility.
- Reduce operational errors.
- Provide reliable business activity records.
- Help management make better decisions.