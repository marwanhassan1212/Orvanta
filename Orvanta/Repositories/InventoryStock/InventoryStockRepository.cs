
using Microsoft.EntityFrameworkCore;
using Orvanta.Data;
using Orvanta.Models;
using Orvanta.Repositories.Interfaces;

namespace Orvanta.Repositories.InventoryStock
{
    public class InventoryStockRepository : IInventoryStockRepository
    {
        private readonly ApplicationDbContext _db;

        public InventoryStockRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Models.InventoryStock>> GetAllAsync()
        {
            return await _db.InventoryStocks
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Warehouse)
                .OrderBy(x => x.Product.Name)
                .ThenBy(x => x.Warehouse.Name)
                .ToListAsync();
        }

        public async Task<Models.InventoryStock?> GetByProductAndWarehouseAsync(
            int productId, int warehouseId)
        {
            return await _db.InventoryStocks
                .Include(x => x.Product)
                .Include(x => x.Warehouse)
                .FirstOrDefaultAsync(x =>
                    x.ProductId == productId &&
                    x.WarehouseId == warehouseId);
        }

        public async Task<IEnumerable<Models.InventoryStock>> GetByProductIdAsync(
            int productId)
        {
            return await _db.InventoryStocks
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Warehouse)
                .Where(x => x.ProductId == productId)
                .OrderBy(x => x.Warehouse.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Models.InventoryStock>> GetByWarehouseIdAsync(
            int warehouseId)
        {
            return await _db.InventoryStocks
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Warehouse)
                .Where(x => x.WarehouseId == warehouseId)
                .OrderBy(x => x.Product.Name)
                .ToListAsync();
        }

        public async Task AddAsync(Models.InventoryStock inventoryStock)
        {
            await _db.InventoryStocks.AddAsync(inventoryStock);
        }

        public void Update(Models.InventoryStock inventoryStock)
        {
            _db.InventoryStocks.Update(inventoryStock);
        }
    }
}
