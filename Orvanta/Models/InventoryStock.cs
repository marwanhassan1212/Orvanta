
namespace Orvanta.Models
{
    public class InventoryStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
        public decimal Quantity { get; set; }
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
        public decimal AverageCost { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
