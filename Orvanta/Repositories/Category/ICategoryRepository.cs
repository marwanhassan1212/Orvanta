
using Orvanta.Models;
using Orvanta.ViewModels.Categories;
using Orvanta.ViewModels.Common;

namespace Orvanta.Repositories
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyList<Models.Category>> GetAllAsync();
        Task<IReadOnlyList<Models.Category>> GetActiveAsync();
        Task<Models.Category?> GetByIdAsync(int id);
        Task<bool> ExistsByNameAsync(string name, int? excludedId = null);
        Task<PagedResult<Models.Category>> GetFilteredAsync(CategoryFilterViewModel filter);
        Task<bool> HasProductsAsync(int categoryId);
        Task AddAsync(Models.Category category);
        void Update(Models.Category category);
    }
}
