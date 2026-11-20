
using Orvanta.Models;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Units;

namespace Orvanta.Repositories
{
    public interface IUnitRepository
    {
        Task<IReadOnlyList<Models.Unit>> GetAllAsync();
        Task<IReadOnlyList<Models.Unit>> GetActiveAsync();
        Task<Models.Unit?> GetByIdAsync(int id);
        Task<bool> ExistsByNameAsync(string name, int? excludedId = null);
        Task<bool> ExistsBySymbolAsync(string symbol, int? excludedId = null);
        Task<bool> HasActiveProductsAsync(int unitId);
        Task<PagedResult<Models.Unit>> GetFilteredAsync(UnitFilterViewModel filter);
        Task AddAsync(Models.Unit unit);
        void Update(Models.Unit unit);
    }
}