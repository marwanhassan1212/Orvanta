using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Orvanta.Repositories;
using Orvanta.Repositories.Interfaces;

namespace Orvanta.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IUnitRepository Units { get; }
        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; }
        public ICustomerRepository Customers { get; }
        public ISupplierRepository Suppliers { get; }
        public IWarehouseRepository Warehouses { get; }
        public IInventoryStockRepository InventoryStocks { get; }
        public IInventoryTransactionRepository Transactions { get; }
        public UnitOfWork(
            ApplicationDbContext db,
            IUnitRepository unitRepository,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            ISupplierRepository supplierRepository,
            IWarehouseRepository warehouseRepository,
            IInventoryStockRepository inventoryStockRepository,
            IInventoryTransactionRepository inventoryTransactionRepository)
        {
            _db = db;
            Units = unitRepository;
            Categories = categoryRepository;
            Products = productRepository;
            Customers = customerRepository;
            Suppliers = supplierRepository;
            Warehouses = warehouseRepository;
            InventoryStocks = inventoryStockRepository;
            Transactions = inventoryTransactionRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        private IDbContextTransaction? _transaction;

        public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_transaction != null)
                throw new InvalidOperationException("A transaction is already active.");
            _transaction = await _db.Database.BeginTransactionAsync(isolationLevel);
            return _transaction;
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction.");

            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
                return;

            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}