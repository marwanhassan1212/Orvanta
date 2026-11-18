namespace Orvanta.Models
{
    public class InvoiceApprovalHistory
    {
        public int Id { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceApprovalAction Action { get; set; }
        public string PerformedByUserId { get; set; } = string.Empty;
        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
        public string? RejectionReason { get; set; }
    }
}