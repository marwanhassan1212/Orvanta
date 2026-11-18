namespace Orvanta.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Sales Invoices
        public ICollection<SalesInvoice> SalesInvoices { get; set; } = new List<SalesInvoice>();

        // Customer Receipts
        public ICollection<CustomerReceipt> CustomerReceipts { get; set; } = new List<CustomerReceipt>();

    }
}