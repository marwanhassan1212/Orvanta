
using Orvanta.Repositories;

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
        public UnitOfWork(
            ApplicationDbContext db,
            IUnitRepository unitRepository,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            ISupplierRepository supplierRepository,
            IWarehouseRepository warehouseRepository)
        {
            _db = db;
            Units = unitRepository;
            Categories = categoryRepository;
            Products = productRepository;
            Customers = customerRepository;
            Suppliers = supplierRepository;
            Warehouses = warehouseRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}