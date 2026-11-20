
using Orvanta.Models;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Products;

namespace Orvanta.Repositories
{
    public interface IProductRepository
    {
        Task<PagedResult<Models.Product>> GetFilteredAsync(ProductFilterViewModel filter);
        Task<Models.Product?> GetByIdAsync(int id);
        Task<bool> ExistsBySKUAsync(string sku, int? excludedId = null);
        Task<bool> CategoryExistsAndActiveAsync(int categoryId);
        Task<bool> UnitExistsAndActiveAsync(int unitId);
        Task AddAsync(Models.Product product);
        void Update(Models.Product product);
    }
}
