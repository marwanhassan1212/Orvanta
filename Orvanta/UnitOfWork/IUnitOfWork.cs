using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Orvanta.Repositories;
using Orvanta.Repositories.Interfaces;

namespace Orvanta.Data
{
    public interface IUnitOfWork
    {
        IUnitRepository Units { get; }
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        ISupplierRepository Suppliers { get; }
        IWarehouseRepository Warehouses { get; }
        IInventoryStockRepository InventoryStocks { get; }
        IInventoryTransactionRepository Transactions { get; }
        Task<int> SaveChangesAsync();

        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}