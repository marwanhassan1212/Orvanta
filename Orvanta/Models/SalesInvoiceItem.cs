
namespace Orvanta.Models
{
    public class SalesInvoiceItem
    {
        public int Id { get; set; }
        public int SalesInvoiceId { get; set; }
        public SalesInvoice SalesInvoice { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public string ProductNameSnapshot { get; set; } = string.Empty;
        public string SKUSnapshot { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitCostSnapshot { get; set; }
        public decimal LineTotal { get; set; }

        public InventoryTransaction? InventoryTransaction { get; set; }
    }
}
