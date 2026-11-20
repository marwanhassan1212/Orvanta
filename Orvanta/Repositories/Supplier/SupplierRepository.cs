using Microsoft.EntityFrameworkCore;
using Orvanta.Data;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Suppliers;

namespace Orvanta.Repositories.Supplier
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _db;
        public SupplierRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Models.Supplier supplier)
        {
            await _db.Suppliers.AddAsync(supplier);
        }

        public async Task<Models.Supplier?> GetByIdAsync(int id)
        {
            return await _db.Suppliers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResult<Models.Supplier>> GetFilteredAsync(SupplierFilterViewModel filter)
        {
            var query = _db.Suppliers
               .AsNoTracking()
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var search = filter.SearchTerm.Trim();

                query = query.Where(s =>
                    s.Name.Contains(search) ||
                    (s.Phone != null && s.Phone.Contains(search)) ||
                    (s.Email != null && s.Email.Contains(search)));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(s => s.IsActive == filter.IsActive.Value);
            }

            var descending = filter.SortDirection == "desc";
            query = (filter.SortBy, descending) switch
            {
                ("createdat", false) => query.OrderBy(s => s.CreatedAt).ThenBy(s => s.Id),
                ("createdat", true) => query.OrderByDescending(s => s.CreatedAt).ThenBy(s => s.Id),


                (_, true) => query.OrderByDescending(s => s.Name).ThenBy(s => s.Id),
                _ => query.OrderBy(s => s.Name).ThenBy(s => s.Id)

            };

            var totalCount = await query.CountAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);


            filter.PageNumber = totalPages == 0 ? 1 : Math.Min(filter.PageNumber, totalPages);

            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Models.Supplier>
            {
                Items = items,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };
        }

        public void Update(Models.Supplier supplier)
        {
            _db.Suppliers.Update(supplier);
        }
    }
}
