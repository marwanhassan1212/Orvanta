namespace Orvanta.Models
{
    public class PurchaseInvoiceItem
    {
        public int Id { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public PurchaseInvoice PurchaseInvoice { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public string ProductNameSnapshot { get; set; } = string.Empty;
        public string SKUSnapshot { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal AllocatedDiscount { get; set; }
        public decimal LineTotal { get; set; }
        public decimal UnitCost { get; set; }

        public InventoryTransaction? InventoryTransaction { get; set; }
    }
}