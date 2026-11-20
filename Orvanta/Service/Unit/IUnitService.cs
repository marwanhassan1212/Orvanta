using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Units;

namespace Orvanta.Service.Unit
{
    public interface IUnitService
    {
        Task<IReadOnlyList<UnitListViewModel>> GetAllAsync();
        Task<IReadOnlyList<UnitListViewModel>> GetActiveAsync();
        Task<UnitDetailsViewModel?> GetByIdAsync(int id);
        Task<(bool Success, string? Error)> CreateAsync(UnitCreateViewModel model);
        Task<(bool Success, string? Error)> UpdateAsync(UnitUpdateViewModel model);
        Task<PagedResult<UnitListViewModel>> GetFilteredAsync(UnitFilterViewModel filter);
        Task<(bool Success, string? Error)> ToggleStatusAsync(int id);
    }
}
