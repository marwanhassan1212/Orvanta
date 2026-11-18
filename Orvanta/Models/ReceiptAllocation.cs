namespace Orvanta.Models
{
    public class ReceiptAllocation
    {
        public int Id { get; set; }
        public int CustomerReceiptId { get; set; }
        public CustomerReceipt CustomerReceipt { get; set; } = null!;
        public int SalesInvoiceId { get; set; }
        public SalesInvoice SalesInvoice { get; set; } = null!;
        public decimal AllocatedAmount { get; set; }
    }
}