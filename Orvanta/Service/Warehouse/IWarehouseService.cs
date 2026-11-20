
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Warehouses;

namespace Orvanta.Services
{
    public interface IWarehouseService
    {
        Task<PagedResult<WarehouseListViewModel>> GetFilteredAsync(WarehouseFilterViewModel filter);
        Task<WarehouseDetailsViewModel?> GetByIdAsync(int id);
        Task<(bool Success, string? Error)> CreateAsync(WarehouseCreateViewModel model);
        Task<(bool Success, string? Error)> UpdateAsync(WarehouseUpdateViewModel model);
        Task<(bool Success, string? Error)> ToggleStatusAsync(int id);
        Task<IEnumerable<WarehouseListViewModel>> GetActiveAsync();
    }
}
