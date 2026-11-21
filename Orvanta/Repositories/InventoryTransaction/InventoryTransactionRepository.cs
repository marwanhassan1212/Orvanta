
using Microsoft.EntityFrameworkCore;
using Orvanta.Data;
using Orvanta.Repositories.Interfaces;

namespace Orvanta.Repositories.InventoryTransaction
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly ApplicationDbContext _db;

        public InventoryTransactionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Models.InventoryTransaction transaction)
        {
            await _db.InventoryTransactions.AddAsync(transaction);
        }

        public async Task<bool> ExistsForPurchaseInvoiceItemAsync(int purchaseInvoiceItemId)
        {
            return await _db.InventoryTransactions.AnyAsync(x => x.PurchaseInvoiceItemId == purchaseInvoiceItemId);

        }

        public async Task<bool> ExistsForSalesInvoiceItemAsync(int salesInvoiceItemId)
        {
            return await _db.InventoryTransactions.AnyAsync(x => x.SalesInvoiceItemId == salesInvoiceItemId);
        }

        public async Task<IEnumerable<Models.InventoryTransaction>> GetByProductAndWarehouseAsync(int productId, int warehouseId)
        {
            return await _db.InventoryTransactions
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Warehouse)
                .Where(x => x.ProductId == productId &&
                            x.WarehouseId == warehouseId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Models.InventoryTransaction>> GetByProductIdAsync(int productId)
        {
            return await _db.InventoryTransactions
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Warehouse)
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Models.InventoryTransaction>> GetByWarehouseIdAsync(int warehouseId)
        {
            return await _db.InventoryTransactions
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Warehouse)
                .Where(x => x.WarehouseId == warehouseId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
