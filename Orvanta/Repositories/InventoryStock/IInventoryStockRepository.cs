
using Orvanta.Models;

namespace Orvanta.Repositories.Interfaces
{
    public interface IInventoryStockRepository
    {
        Task<IEnumerable<Models.InventoryStock>> GetAllAsync();
        Task<Models.InventoryStock?> GetByProductAndWarehouseAsync(int productId, int warehouseId);
        Task<IEnumerable<Models.InventoryStock>> GetByWarehouseIdAsync(int warehouseId);
        Task<IEnumerable<Models.InventoryStock>> GetByProductIdAsync(int productId);
        Task AddAsync(Models.InventoryStock inventoryStock);
        void Update(Models.InventoryStock inventoryStock);
    }
}
