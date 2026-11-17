# Use Cases – Orvanta

## 1. Introduction

This document describes the main interactions between Orvanta users and the system.

Each use case defines the actor, preconditions, main workflow, alternative flows, and expected outcome.

## 2. Actors

| Actor | Description |
|---|---|
| Admin | Manages users, settings, and overall system operations. |
| Manager | Supervises business operations and approves invoices. |
| Accountant | Handles receipts, supplier payments, and expenses. |
| Sales Employee | Manages customers and sales invoices. |
| Warehouse Employee | Manages suppliers, purchasing, and inventory operations. |

## 3. Authentication Use Cases

### UC-01 – Login

**Primary Actor:** All Users

**Preconditions:**
- The user has an existing account.
- The account is active.

**Main Flow:**
1. The user opens the login page.
2. The user enters their username or email and password.
3. The system validates the credentials.
4. The system verifies that the account is active.
5. The system creates an authenticated session.
6. The user is redirected to the dashboard.

**Alternative Flows:**
- Invalid credentials: The system displays an appropriate error message.
- Inactive account: Access is denied.
- Locked account: The system informs the user that the account is temporarily locked.

**Postconditions:**
- The user is authenticated and can access authorized resources.

### UC-02 – Change Password

**Primary Actor:** Authenticated User

**Preconditions:**
- The user is authenticated.

**Main Flow:**
1. The user opens the change password page.
2. The user enters the current password.
3. The user enters and confirms the new password.
4. The system validates the password requirements.
5. The system updates the password.

**Postconditions:**
- The user's password is updated.

## 4. User Management Use Cases

### UC-03 – Manage Users

**Primary Actor:** Admin

**Preconditions:**
- The Admin is authenticated.

**Main Flow:**
1. The Admin opens user management.
2. The system displays the user list.
3. The Admin can create, view, edit, activate, or deactivate user accounts.
4. The Admin assigns an appropriate role to each user.
5. The system validates and saves the changes.

**Alternative Flows:**
- Invalid user data: The system displays validation errors.
- Duplicate username or email: The system rejects the operation.

**Postconditions:**
- User account information is updated.

## 5. Master Data Use Cases

### UC-04 – Manage Customers

**Primary Actors:** Admin, Manager, Sales Employee

**Main Flow:**
1. The user opens customer management.
2. The system displays existing customers.
3. The user creates or updates customer information.
4. The system validates required fields and unique customer codes.
5. The system saves the customer record.

**Alternative Flows:**
- Invalid data: The system displays validation errors.
- Existing customer has historical transactions: The customer can be deactivated but not permanently deleted.

### UC-05 – Manage Suppliers

**Primary Actors:** Admin, Manager, Warehouse Employee

**Main Flow:**
1. The user opens supplier management.
2. The system displays existing suppliers.
3. The user creates or updates supplier information.
4. The system validates required fields and unique supplier codes.
5. The system saves the supplier record.

**Alternative Flows:**
- Invalid data: The system displays validation errors.
- Existing supplier has historical transactions: The supplier can be deactivated but not permanently deleted.

### UC-06 – Manage Products and Catalog Data

**Primary Actors:** Admin, Manager

**Main Flow:**
1. The user opens product management.
2. The system displays existing products.
3. The user creates or updates product information.
4. The user selects a category and unit.
5. The system validates product data and SKU uniqueness.
6. The system saves the product.

**Alternative Flows:**
- Duplicate SKU: The system rejects the operation.
- Product has historical transactions: The product can be deactivated but not permanently deleted.

### UC-07 – Manage Warehouses

**Primary Actors:** Admin, Manager

**Main Flow:**
1. The user opens warehouse management.
2. The system displays existing warehouses.
3. The user creates or updates warehouse information.
4. The system validates the warehouse code.
5. The system saves the warehouse.

**Alternative Flows:**
- Duplicate warehouse code: The system rejects the operation.
- Warehouse has historical inventory transactions: The warehouse can be deactivated but not permanently deleted.

## 6. Inventory Use Cases

### UC-08 – View Inventory

**Primary Actors:** Admin, Manager, Accountant, Sales Employee, Warehouse Employee

**Main Flow:**
1. The user opens the inventory module.
2. The system displays product quantities by warehouse.
3. The user searches or filters inventory records.
4. The system displays the available quantities and average costs according to the user's authorization.

**Postconditions:**
- Inventory information is displayed without modifying stock.

### UC-09 – Record Opening Inventory Balances

**Primary Actors:** Admin, Manager, Authorized Warehouse Employee

**Preconditions:**
- The product and warehouse exist and are active.
- The opening balance has not already been recorded for the same product and warehouse during initial setup.

**Main Flow:**
1. The user opens the opening balances page.
2. The user selects a warehouse.
3. The user selects products and enters their initial quantities and unit costs.
4. The system validates the entered data.
5. The system records opening balance inventory transactions.
6. The system updates the corresponding inventory stock records.

**Alternative Flows:**
- Invalid quantity or cost: The system rejects the entry.
- Duplicate opening balance: The system prevents duplicate initial stock recording.

**Postconditions:**
- Initial stock quantities and average costs are recorded.

## 7. Sales Use Cases

### UC-10 – Create Sales Invoice

**Primary Actors:** Admin, Manager, Sales Employee

**Preconditions:**
- The customer is active.
- The selected warehouse is active.
- Selected products are active.

**Main Flow:**
1. The user opens the sales invoice creation page.
2. The user selects a customer and warehouse.
3. The user adds products, quantities, and selling prices.
4. The system calculates line totals and subtotal.
5. The user enters an optional invoice-level discount.
6. The system calculates the invoice total.
7. The user saves the invoice as Draft.

**Alternative Flows:**
- Invalid product or quantity: The system displays validation errors.
- Discount exceeds subtotal: The system rejects the invalid value.

**Postconditions:**
- A Draft sales invoice is created.
- Inventory is not affected.

### UC-11 – Submit Sales Invoice for Approval

**Primary Actor:** Sales Invoice Creator

**Preconditions:**
- The invoice is Draft or Rejected.
- The invoice contains at least one valid item.

**Main Flow:**
1. The creator opens the invoice.
2. The creator reviews its information.
3. The creator submits it for approval.
4. The system validates the invoice.
5. The system changes its status to PendingApproval.
6. The system records the submission in approval history.

**Alternative Flows:**
- Invalid invoice: Submission is rejected and validation errors are displayed.
- Unauthorized user: Submission is denied.

**Postconditions:**
- The invoice is awaiting review.

### UC-12 – Approve or Reject Sales Invoice

**Primary Actors:** Admin, Manager, Authorized Sales Employee, Authorized Warehouse Employee

**Preconditions:**
- The invoice is PendingApproval.
- The approver is authorized.
- The approver is not the invoice creator.

**Main Flow:**
1. The approver opens the pending invoice.
2. The system displays invoice details.
3. The approver reviews the invoice.
4. The approver chooses Approve or Reject.

**Approval Flow:**
1. The system revalidates the invoice and stock availability.
2. The system posts the invoice.
3. The system deducts stock.
4. The system records inventory transactions.
5. The system stores the applicable average cost snapshots.
6. The system records the approval action.
7. The invoice status becomes Posted.

**Rejection Flow:**
1. The approver enters a rejection reason.
2. The system changes the invoice status to Rejected.
3. The system records the rejection in approval history.
4. The invoice is returned to its creator for correction.

**Alternative Flows:**
- Creator attempts to approve their own invoice: The system denies the operation.
- Insufficient stock: Posting fails and inventory remains unchanged.
- Rejection reason is empty: The system rejects the action.
- Posting fails: All related database changes are rolled back.

**Postconditions:**
- The invoice is either Posted or Rejected.
- Every approval action is preserved in history.

## 8. Purchasing Use Cases

### UC-13 – Create Purchase Invoice

**Primary Actors:** Admin, Manager, Warehouse Employee

**Preconditions:**
- The supplier is active.
- The receiving warehouse is active.
- Selected products are active.

**Main Flow:**
1. The user opens purchase invoice creation.
2. The user selects a supplier and warehouse.
3. The user adds products, quantities, and purchase costs.
4. The system calculates invoice totals.
5. The user saves the invoice as Draft.

**Alternative Flows:**
- Invalid quantity or cost: The system displays validation errors.
- Invalid supplier or warehouse: The system rejects the operation.

**Postconditions:**
- A Draft purchase invoice is created.
- Inventory is not affected.

### UC-14 – Submit Purchase Invoice for Approval

**Primary Actor:** Purchase Invoice Creator

**Preconditions:**
- The invoice is Draft or Rejected.
- The invoice contains at least one valid item.

**Main Flow:**
1. The creator opens the purchase invoice.
2. The creator reviews its details.
3. The creator submits it for approval.
4. The system validates the invoice.
5. The system changes its status to PendingApproval.
6. The system records the submission in approval history.

**Postconditions:**
- The purchase invoice is awaiting review.

### UC-15 – Approve or Reject Purchase Invoice

**Primary Actors:** Admin, Manager, Authorized Warehouse Employee, Authorized Sales Employee

**Preconditions:**
- The invoice is PendingApproval.
- The approver is authorized.
- The approver is not the invoice creator.

**Main Flow:**
1. The approver opens the pending purchase invoice.
2. The approver reviews its details.
3. The approver chooses Approve or Reject.

**Approval Flow:**
1. The system validates the invoice.
2. The system posts the invoice.
3. The system increases stock in the receiving warehouse.
4. The system updates the weighted average cost.
5. The system records inventory transactions.
6. The system records the approval action.
7. The invoice status becomes Posted.

**Rejection Flow:**
1. The approver enters a rejection reason.
2. The system changes the invoice status to Rejected.
3. The system records the rejection in approval history.
4. The invoice is returned to its creator.

**Alternative Flows:**
- Creator attempts to approve their own invoice: The system denies the operation.
- Posting fails: All related database changes are rolled back.
- Rejection reason is empty: The system rejects the action.

**Postconditions:**
- The invoice is either Posted or Rejected.
- Inventory changes only after successful posting.

## 9. Finance Use Cases

### UC-16 – Record Customer Receipt

**Primary Actor:** Accountant

**Preconditions:**
- The customer exists.
- The allocated sales invoices are Posted.

**Main Flow:**
1. The Accountant creates a customer receipt.
2. The Accountant selects the customer.
3. The Accountant selects one or more posted sales invoices.
4. The Accountant enters allocation amounts.
5. The system validates customer matching and outstanding balances.
6. The Accountant posts the receipt.
7. The system updates customer outstanding balances.

**Alternative Flows:**
- Allocation exceeds invoice outstanding: The system rejects the operation.
- Allocation total differs from receipt amount: The system rejects posting.
- Invoice belongs to another customer: The system rejects the allocation.

**Postconditions:**
- A Posted receipt is recorded.
- Customer outstanding balances are updated.

### UC-17 – Void Customer Receipt

**Primary Actor:** Authorized Accountant, Admin

**Preconditions:**
- The receipt is Posted.
- The user is authorized to void it.

**Main Flow:**
1. The user opens the posted receipt.
2. The user requests to void it.
3. The user enters a mandatory reason.
4. The system validates the operation.
5. The system marks the receipt as Voided.
6. The system records the user, timestamp, and reason.

**Postconditions:**
- The receipt is excluded from outstanding balance calculations.

### UC-18 – Record Supplier Payment

**Primary Actor:** Accountant

**Preconditions:**
- The supplier exists.
- The allocated purchase invoices are Posted.

**Main Flow:**
1. The Accountant creates a supplier payment.
2. The Accountant selects the supplier.
3. The Accountant selects one or more posted purchase invoices.
4. The Accountant enters allocation amounts.
5. The system validates supplier matching and outstanding balances.
6. The Accountant posts the payment.
7. The system updates supplier outstanding balances.

**Alternative Flows:**
- Allocation exceeds invoice outstanding: The system rejects the operation.
- Allocation total differs from payment amount: The system rejects posting.
- Invoice belongs to another supplier: The system rejects the allocation.

**Postconditions:**
- A Posted supplier payment is recorded.

### UC-19 – Void Supplier Payment

**Primary Actor:** Authorized Accountant, Admin

**Preconditions:**
- The supplier payment is Posted.
- The user is authorized to void it.

**Main Flow:**
1. The user opens the posted payment.
2. The user requests to void it.
3. The user enters a mandatory reason.
4. The system validates the operation.
5. The system marks the payment as Voided.
6. The system records the user, timestamp, and reason.

**Postconditions:**
- The payment is excluded from supplier outstanding balance calculations.

### UC-20 – Manage Expenses

**Primary Actor:** Accountant, Admin

**Main Flow:**
1. The user opens expense management.
2. The user creates an expense.
3. The user selects an active expense category.
4. The user enters the amount, date, and description.
5. The system validates the expense.
6. The expense is saved as Draft.
7. The authorized user posts the expense.

**Alternative Flows:**
- Invalid amount or category: The system rejects the operation.

**Postconditions:**
- A Posted expense is included in relevant financial reports.

### UC-21 – Void Expense

**Primary Actor:** Authorized Accountant, Admin

**Preconditions:**
- The expense is Posted.
- The user is authorized to void it.

**Main Flow:**
1. The user opens the posted expense.
2. The user requests to void it.
3. The user enters a mandatory reason.
4. The system validates the operation.
5. The system marks the expense as Voided.
6. The system records the voiding information.

**Postconditions:**
- The expense is excluded from financial reports.

## 10. Reporting Use Cases

### UC-22 – View Business Reports

**Primary Actors:** Admin, Manager, Accountant, Authorized Sales Employee, Authorized Warehouse Employee

**Main Flow:**
1. The user opens the reports module.
2. The user selects a report.
3. The user enters optional filters such as date range, warehouse, customer, or supplier.
4. The system validates the filters.
5. The system retrieves the authorized report data.
6. The system displays the report.

**Available Reports:**
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

## 11. Audit Use Cases

### UC-23 – View Audit Logs

**Primary Actor:** Admin

**Preconditions:**
- The Admin is authenticated.

**Main Flow:**
1. The Admin opens the audit log module.
2. The system displays recorded activities.
3. The Admin filters records by user, action, entity, or date.
4. The system displays the matching audit records.

**Postconditions:**
- Audit information is displayed without modifying historical records.

## 12. Company Settings Use Cases

### UC-24 – Manage Company Profile

**Primary Actor:** Admin

**Main Flow:**
1. The Admin opens company settings.
2. The system displays the current company information.
3. The Admin updates the company name, address, phone, email, or logo.
4. The system validates the information.
5. The system saves the updated company profile.

**Postconditions:**
- Company profile information is updated.