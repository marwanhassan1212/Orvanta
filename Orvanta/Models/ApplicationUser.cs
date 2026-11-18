
using Microsoft.AspNetCore.Identity;

namespace Orvanta.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }


        // Sales Invoices
        public ICollection<SalesInvoice> CreatedSalesInvoices { get; set; } = new List<SalesInvoice>();
        public ICollection<SalesInvoice> ApprovedSalesInvoices { get; set; } = new List<SalesInvoice>();

        // Purchase Invoices
        public ICollection<PurchaseInvoice> CreatedPurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
        public ICollection<PurchaseInvoice> ApprovedPurchaseInvoices { get; set; } = new List<PurchaseInvoice>();



        // Inventory Transactions
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();

        // Customer Receipts
        public ICollection<CustomerReceipt> CreatedCustomerReceipts { get; set; } = new List<CustomerReceipt>();
        public ICollection<CustomerReceipt> PostedCustomerReceipts { get; set; } = new List<CustomerReceipt>();
        public ICollection<CustomerReceipt> VoidedCustomerReceipts { get; set; } = new List<CustomerReceipt>();



        // Supplier Payments
        public ICollection<SupplierPayment> CreatedSupplierPayments { get; set; } = new List<SupplierPayment>();
        public ICollection<SupplierPayment> PostedSupplierPayments { get; set; } = new List<SupplierPayment>();
        public ICollection<SupplierPayment> VoidedSupplierPayments { get; set; } = new List<SupplierPayment>();

        // Expenses
        public ICollection<Expense> CreatedExpenses { get; set; } = new List<Expense>();
        public ICollection<Expense> PostedExpenses { get; set; } = new List<Expense>();
        public ICollection<Expense> VoidedExpenses { get; set; } = new List<Expense>();
           


        // System Auditing
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

        // Invoice Approval History
        public ICollection<InvoiceApprovalHistory> ApprovalHistory { get; set; } = new List<InvoiceApprovalHistory>();

    }
}
