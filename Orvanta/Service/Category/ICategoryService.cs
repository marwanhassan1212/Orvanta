
using Orvanta.ViewModels.Categories;
using Orvanta.ViewModels.Common;

namespace Orvanta.Services
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryListViewModel>> GetAllAsync();
        Task<IReadOnlyList<CategoryListViewModel>> GetActiveAsync();
        Task<CategoryDetailsViewModel?> GetByIdAsync(int id);
        Task<(bool Success, string? Error)> CreateAsync(CategoryCreateViewModel model);
        Task<(bool Success, string? Error)> UpdateAsync(CategoryUpdateViewModel model);
        Task<(bool Success, string? Error)> ToggleStatusAsync(int id);
        Task<PagedResult<CategoryListViewModel>> GetFilteredAsync(CategoryFilterViewModel filter);

    }
}
