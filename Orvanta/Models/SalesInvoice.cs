namespace Orvanta.Models
{
    public class SalesInvoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
        public DateTime InvoiceDate { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }
        public string? LastRejectionReason { get; set; }
        public string CreatedByUserId { get; set; } = string.Empty;
        public string? ApprovedByUserId { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Sales Invoice Items
        public ICollection<SalesInvoiceItem> Items { get; set; } = new List<SalesInvoiceItem>();

        // Receipt Allocations
        public ICollection<ReceiptAllocation> ReceiptAllocations { get; set; } = new List<ReceiptAllocation>();

    }
}