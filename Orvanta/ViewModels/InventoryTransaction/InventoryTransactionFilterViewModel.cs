
using System.ComponentModel.DataAnnotations;
using Orvanta.Models;

namespace Orvanta.ViewModels.Inventory
{
    public class InventoryTransactionFilterViewModel
    {
        public int? ProductId { get; set; }
        public int? WarehouseId { get; set; }
        public InventoryTransactionType? TransactionType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 10;
        public string SortDirection { get; set; } = "desc";
    }
}
