using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Products;
namespace Orvanta.Services
{
    public interface IProductService
    {
        Task<PagedResult<ProductListViewModel>> GetFilteredAsync(ProductFilterViewModel filter);
        Task<ProductDetailsViewModel?> GetByIdAsync(int id);
        Task<(bool Success, string? Error)> CreateAsync(ProductCreateViewModel model);
        Task<(bool Success, string? Error)> UpdateAsync(ProductUpdateViewModel model);
        Task<(bool Success, string? Error)> ToggleStatusAsync(int id);
    }
}
