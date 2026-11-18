
namespace Orvanta.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<InventoryStock> InventoryStocks { get; set; } = new List<InventoryStock>();
        public ICollection<SalesInvoiceItem> SalesInvoiceItems { get; set; } = new List<SalesInvoiceItem>();
        public ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; } = new List<PurchaseInvoiceItem>();
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();

    }
}
