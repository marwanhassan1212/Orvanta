using Microsoft.EntityFrameworkCore;
using Orvanta.Data;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Warehouses;

namespace Orvanta.Repositories.Warehouse
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly ApplicationDbContext _db;
        public WarehouseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(Models.Warehouse warehouse)
        {
            await _db.Warehouses.AddAsync(warehouse);
        }

        public async Task<bool> CodeExistsAsync(string code, int? excludeId = null)
        {
            return await _db.Warehouses.AnyAsync(x => x.Code == code
            && (!excludeId.HasValue || x.Id != excludeId.Value));
        }

        public async Task<Models.Warehouse?> GetByIdAsync(int id)
        {
            return await _db.Warehouses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResult<Models.Warehouse>> GetFilteredAsync(WarehouseFilterViewModel filter)
        {
            var query = _db.Warehouses
               .AsNoTracking()
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var search = filter.SearchTerm.Trim();

                query = query.Where(w =>
                    w.Name.Contains(search) ||
                    w.Code.Contains(search) ||
                    (w.Address != null && w.Address.Contains(search)));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(w =>
                    w.IsActive == filter.IsActive.Value);
            }

            var descending = filter.SortDirection == "desc";

            query = (filter.SortBy, descending) switch
            {
                ("createdat", false) =>
                    query.OrderBy(w => w.CreatedAt).ThenBy(w => w.Id),

                ("createdat", true) =>
                    query.OrderByDescending(w => w.CreatedAt).ThenBy(w => w.Id),

                ("code", false) =>
                    query.OrderBy(w => w.Code).ThenBy(w => w.Id),

                ("code", true) =>
                    query.OrderByDescending(w => w.Code).ThenBy(w => w.Id),

                (_, true) =>
                    query.OrderByDescending(w => w.Name).ThenBy(w => w.Id),

                _ =>
                    query.OrderBy(w => w.Name).ThenBy(w => w.Id)
            };

            var totalCount = await query.CountAsync();

            var totalPages = (int)Math.Ceiling(
                totalCount / (double)filter.PageSize);

            filter.PageNumber = totalPages == 0
                ? 1
                : Math.Min(filter.PageNumber, totalPages);

            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Models.Warehouse>
            {
                Items = items,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };
        }

        public void Update(Models.Warehouse warehouse)
        {
            _db.Warehouses.Update(warehouse);
        }

        public async Task<IEnumerable<Models.Warehouse>> GetActiveAsync()
        {
            return await _db.Warehouses.AsNoTracking().Where(w => w.IsActive)
                .OrderBy(w => w.Name).ToListAsync();



        }
    }
}
