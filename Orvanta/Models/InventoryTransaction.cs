
namespace Orvanta.Models
{
    public class InventoryTransaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
        public InventoryTransactionType TransactionType { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
        public int? PurchaseInvoiceItemId { get; set; }
        public int? SalesInvoiceItemId { get; set; }
        public string CreatedByUserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public PurchaseInvoiceItem? PurchaseInvoiceItem { get; set; }
        public SalesInvoiceItem? SalesInvoiceItem { get; set; }
    }
}
