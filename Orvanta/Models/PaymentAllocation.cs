namespace Orvanta.Models
{
    public class PaymentAllocation
    {
        public int Id { get; set; }
        public int SupplierPaymentId { get; set; }
        public SupplierPayment SupplierPayment { get; set; } = null!;
        public int PurchaseInvoiceId { get; set; }
        public PurchaseInvoice PurchaseInvoice { get; set; } = null!;
        public decimal AllocatedAmount { get; set; }
    }
}