
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Suppliers;

namespace Orvanta.Services
{
    public interface ISupplierService
    {
        Task<PagedResult<SupplierListViewModel>> GetFilteredAsync(SupplierFilterViewModel filter);
        Task<SupplierDetailsViewModel?> GetByIdAsync(int id);
        Task<(bool Success, string? Error)> CreateAsync(SupplierCreateViewModel model);
        Task<(bool Success, string? Error)> UpdateAsync(SupplierUpdateViewModel model);
        Task<(bool Success, string? Error)> ToggleStatusAsync(int id);
    }
}
