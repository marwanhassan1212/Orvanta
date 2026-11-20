
using Orvanta.Models;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Warehouses;

namespace Orvanta.Repositories
{
    public interface IWarehouseRepository
    {
        Task<PagedResult<Models.Warehouse>> GetFilteredAsync(WarehouseFilterViewModel filter);
        Task<Models.Warehouse?> GetByIdAsync(int id);
        Task<bool> CodeExistsAsync(string code, int? excludeId = null);

        Task<IEnumerable<Models.Warehouse>> GetActiveAsync();
        Task AddAsync(Models.Warehouse warehouse);
        void Update(Models.Warehouse warehouse);
    }
}
