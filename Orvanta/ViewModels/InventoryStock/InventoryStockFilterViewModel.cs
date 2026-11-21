
using System.ComponentModel.DataAnnotations;

namespace Orvanta.ViewModels.Inventory
{
    public class InventoryStockFilterViewModel
    {
        [StringLength(150)]
        public string? SearchTerm { get; set; }
        public int? ProductId { get; set; }
        public int? WarehouseId { get; set; }

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "ProductName";
        public string SortDirection { get; set; } = "asc";
        public bool? IsLowStock { get; set; }
    }
}
