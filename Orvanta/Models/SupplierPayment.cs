namespace Orvanta.Models
{
    public class SupplierPayment
    {
        public int Id { get; set; }
        public string PaymentNumber { get; set; } = string.Empty;
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
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

        public ICollection<PaymentAllocation> Allocations { get; set; } = new List<PaymentAllocation>();

    }
}