
using Orvanta.Repositories;

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
        Task<int> SaveChangesAsync();
    }
}