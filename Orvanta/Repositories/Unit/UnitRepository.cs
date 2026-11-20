using Microsoft.EntityFrameworkCore;
using Orvanta.Data;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Units;

namespace Orvanta.Repositories.Unit
{
    public class UnitRepository  : IUnitRepository
    {
        private readonly ApplicationDbContext _db;
        public UnitRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Models.Unit unit)
        {
            await _db.Units.AddAsync(unit);
        }

        public async Task<bool> ExistsByNameAsync(string name, int? excludedId = null)
        {
            return await _db.Units.AnyAsync(x => x.Name == name
            && (!excludedId.HasValue || x.Id != excludedId.Value));
        }

        public async Task<bool> ExistsBySymbolAsync(string symbol, int? excludedId = null)
        {
            return await _db.Units.AnyAsync(x => x.Symbol == symbol
            && (!excludedId.HasValue || x.Id != excludedId.Value));
        }

        public async Task<IReadOnlyList<Models.Unit>> GetActiveAsync()
        {
            return await _db.Units.AsNoTracking().Where(x => x.IsActive)
                .OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IReadOnlyList<Models.Unit>> GetAllAsync()
        {
            return await _db.Units.AsNoTracking().OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Models.Unit?> GetByIdAsync(int id)
        {
            return await _db.Units.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> HasActiveProductsAsync(int unitId)
        {
            return await _db.Products.AnyAsync(p =>
                 p.UnitId == unitId && p.IsActive);
        }

        public void Update(Models.Unit unit)
        {
            _db.Units.Update(unit);
        }


        public async Task<PagedResult<Models.Unit>> GetFilteredAsync(
            UnitFilterViewModel filter)
        {
            var query = _db.Units.AsNoTracking().Include(u => u.Products).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var search = filter.SearchTerm.Trim();
                query = query.Where(u => u.Name.Contains(search) || u.Symbol.Contains(search));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(u => u.IsActive == filter.IsActive.Value);
            }

            var descending = filter.SortDirection == "desc";

            query = (filter.SortBy, descending) switch
            {
                ("symbol", false) => query.OrderBy(u => u.Symbol).ThenBy(u => u.Id),
                ("symbol", true) => query.OrderByDescending(u => u.Symbol).ThenBy(u => u.Id),
                (_, true) => query.OrderByDescending(u => u.Name).ThenBy(u => u.Id),
                _ => query.OrderBy(u => u.Name).ThenBy(u => u.Id)
            };

            var totalCount = await query.CountAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
            filter.PageNumber = totalPages == 0 ? 1 : Math.Min(filter.PageNumber, totalPages);

            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Models.Unit>
            {
                Items = items,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };
        }

    }
}
