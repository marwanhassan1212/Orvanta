namespace Orvanta.Models
{
    public class CustomerReceipt
    {
        public int Id { get; set; }
        public string ReceiptNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateTime ReceiptDate { get; set; }
        public decimal Amount { get; set; }
        public DocumentStatus Status { get; set; }
        public string? Notes { get; set; }
        public string CreatedByUserId { get; set; } = string.Empty;
        public string? PostedByUserId { get; set; }
        public DateTime? PostedAt { get; set; }
        public string? VoidedByUserId { get; set; }
        public DateTime? VoidedAt { get; set; }
        public string? VoidReason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Receipt Allocations
        public ICollection<ReceiptAllocation> Allocations { get; set; } = new List<ReceiptAllocation>();

    }
}