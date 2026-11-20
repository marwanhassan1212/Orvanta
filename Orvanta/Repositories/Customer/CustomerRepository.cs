using Microsoft.EntityFrameworkCore;
using Orvanta.Data;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Customers;

namespace Orvanta.Repositories.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Models.Customer customer)
        {
            await _db.Customers.AddAsync(customer);
        }

        public async Task<Models.Customer?> GetByIdAsync(int id)
        {
            return await _db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ViewModels.Common.PagedResult<Models.Customer>> GetFilteredAsync(CustomerFilterViewModel filter)
        {
            var query = _db.Customers
               .AsNoTracking()
               .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var search = filter.SearchTerm.Trim();

                query = query.Where(c =>
                    c.Name.Contains(search) ||
                    (c.Phone != null && c.Phone.Contains(search)) ||
                    (c.Email != null && c.Email.Contains(search)));
            }

            // Filter by status
            if (filter.IsActive.HasValue)
            {
                query = query.Where(c =>
                    c.IsActive == filter.IsActive.Value);
            }

            // Sorting
            var descending = filter.SortDirection == "desc";

            query = (filter.SortBy, descending) switch
            {
                ("createdat", false) =>
                    query.OrderBy(c => c.CreatedAt).ThenBy(c => c.Id),

                ("createdat", true) =>
                    query.OrderByDescending(c => c.CreatedAt).ThenBy(c => c.Id),

                (_, true) =>
                    query.OrderByDescending(c => c.Name).ThenBy(c => c.Id),

                _ =>
                    query.OrderBy(c => c.Name).ThenBy(c => c.Id)
            };

            // Count
            var totalCount = await query.CountAsync();

            // Correct page number
            var totalPages = (int)Math.Ceiling(
                totalCount / (double)filter.PageSize);

            filter.PageNumber = totalPages == 0
                ? 1
                : Math.Min(filter.PageNumber, totalPages);

            // Pagination
            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Models.Customer>
            {
                Items = items,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };
        }

        public void Update(Models.Customer customer)
        {
            _db.Customers.Update(customer);
        }
    }
}
