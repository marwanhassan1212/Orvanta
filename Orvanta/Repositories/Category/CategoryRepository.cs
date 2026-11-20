
using Microsoft.EntityFrameworkCore;
using Orvanta.Data;
using Orvanta.ViewModels.Categories;
using Orvanta.ViewModels.Common;

namespace Orvanta.Repositories.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(Models.Category category)
        {
            await _db.Categories.AddAsync(category);
        }

        public async Task<bool> ExistsByNameAsync(string name, int? excludedId = null)
        {
            return await _db.Categories.AnyAsync(x => x.Name == name
             && (!excludedId.HasValue || x.Id != excludedId.Value));
        }

        public async Task<IReadOnlyList<Models.Category>> GetActiveAsync()
        {
            return await _db.Categories.AsNoTracking().Where(x => x.IsActive)
                .OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IReadOnlyList<Models.Category>> GetAllAsync()
        {
            return await _db.Categories.AsNoTracking().OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Models.Category?> GetByIdAsync(int id)
        {
            return await _db.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> HasProductsAsync(int categoryId)
        {
            return await _db.Products.AnyAsync(x => x.CategoryId == categoryId);
        }

        public void Update(Models.Category category)
        {
            _db.Categories.Update(category);
        }


        public async Task<PagedResult<Models.Category>> GetFilteredAsync(CategoryFilterViewModel filter)
        {
            var query = _db.Categories
                .AsNoTracking()
                .Include(c => c.Products)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var search = filter.SearchTerm.Trim();
                query = query.Where(c => c.Name.Contains(search));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(c => c.IsActive == filter.IsActive.Value);
            }

            var descending = filter.SortDirection == "desc";
            query = (filter.SortBy, descending) switch
            {
                ("createdat", false) => query.OrderBy(c => c.CreatedAt).ThenBy(c => c.Id),
                ("createdat", true) => query.OrderByDescending(c => c.CreatedAt).ThenBy(c => c.Id),
                (_, true) => query.OrderByDescending(c => c.Name).ThenBy(c => c.Id),
                _ => query.OrderBy(c => c.Name).ThenBy(c => c.Id)
            };

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
            filter.PageNumber = totalPages == 0 ? 1 : Math.Min(filter.PageNumber, totalPages);

            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Models.Category>
            {
                Items = items,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };
        }

    }
}
