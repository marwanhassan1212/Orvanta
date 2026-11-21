
namespace Orvanta.ViewModels.Inventory
{
    public class InventoryStockDetailsViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal AverageCost { get; set; }
        public decimal TotalValue => Quantity * AverageCost;
        public DateTime UpdatedAt { get; set; }
    }
}
