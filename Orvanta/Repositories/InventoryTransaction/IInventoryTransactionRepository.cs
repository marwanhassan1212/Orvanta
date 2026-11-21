using Orvanta.Models;

namespace Orvanta.Repositories.Interfaces
{
    public interface IInventoryTransactionRepository
    {
        Task<IEnumerable<Models.InventoryTransaction>> GetByProductAndWarehouseAsync(int productId, int warehouseId);
        Task<IEnumerable<Models.InventoryTransaction>> GetByWarehouseIdAsync(int warehouseId);
        Task<IEnumerable<Models.InventoryTransaction>> GetByProductIdAsync(int productId);
        Task<bool> ExistsForPurchaseInvoiceItemAsync(int purchaseInvoiceItemId);
        Task<bool> ExistsForSalesInvoiceItemAsync(int salesInvoiceItemId);
        Task AddAsync(Models.InventoryTransaction transaction);
    }
}