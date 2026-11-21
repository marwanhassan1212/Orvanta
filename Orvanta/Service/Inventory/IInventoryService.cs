using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Inventory;

namespace Orvanta.Service.InventoryStock
{
    public interface IInventoryService
    {
        Task<PagedResult<InventoryStockListViewModel>> GetStocksAsync(InventoryStockFilterViewModel filter);
        Task<InventoryStockDetailsViewModel?> GetStockDetailsAsync(int productId, int warehouseId);
        Task<PagedResult<InventoryTransactionListViewModel>> GetTransactionsAsync(InventoryTransactionFilterViewModel filter);
        Task<bool> CreateOpeningBalanceAsync(int productId, int warehouseId,
            decimal quantity, decimal unitCost, string createdByUserId);
        Task<bool> IsStockAvailableAsync(int productId, int warehouseId, decimal requestedQuantity);
        Task<decimal> GetAvailableQuantityAsync(int productId, int warehouseId);
        Task<bool> ProcessPurchaseInAsync(int productId, int warehouseId, decimal quantity,
            decimal unitCost, int purchaseInvoiceItemId, string createdByUserId);
        Task<bool> ProcessSaleOutAsync(int productId, int warehouseId, decimal quantity, int salesInvoiceItemId,
            string createdByUserId);




    }
}
