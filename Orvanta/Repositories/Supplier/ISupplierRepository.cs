
using Orvanta.Models;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Suppliers;

namespace Orvanta.Repositories
{
    public interface ISupplierRepository
    {
        Task<PagedResult<Models.Supplier>> GetFilteredAsync(SupplierFilterViewModel filter);
        Task<Models.Supplier?> GetByIdAsync(int id);
        Task AddAsync(Models.Supplier supplier);
        void Update(Models.Supplier supplier);
    }
}
