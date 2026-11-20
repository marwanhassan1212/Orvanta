
using Microsoft.EntityFrameworkCore;
using Orvanta.Data;
using Orvanta.Models;
using Orvanta.Repositories;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Products;

namespace Orvanta.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Product>> GetFilteredAsync(ProductFilterViewModel filter)

        {
            var query = _context.Products.AsNoTracking().Include(p => p.Category).Include(p => p.Unit)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var search = filter.SearchTerm.Trim();
                query = query.Where(p => p.Name.Contains(search) || p.SKU.Contains(search));
            }

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
            }

            if (filter.UnitId.HasValue)
            {
                query = query.Where(p => p.UnitId == filter.UnitId.Value);
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(p => p.IsActive == filter.IsActive.Value);
            }

            query = ApplySorting(query, filter.SortBy, filter.SortDirection);
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Items = items,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };
        }

        private static IQueryable<Product> ApplySorting(IQueryable<Product> query, string? sortBy, string? sortDirection)
        {
            var descending = sortDirection?.ToLower() == "desc";

            return (sortBy?.ToLower(), descending) switch
            {
                ("sku", false) => query.OrderBy(p => p.SKU).ThenBy(p => p.Id),
                ("sku", true) => query.OrderByDescending(p => p.SKU).ThenBy(p => p.Id),

                ("price", false) => query.OrderBy(p => p.SellingPrice).ThenBy(p => p.Id),
                ("price", true) => query.OrderByDescending(p => p.SellingPrice).ThenBy(p => p.Id),

                ("createdat", false) => query.OrderBy(p => p.CreatedAt).ThenBy(p => p.Id),
                ("createdat", true) => query.OrderByDescending(p => p.CreatedAt).ThenBy(p => p.Id),

                (_, true) => query.OrderByDescending(p => p.Name).ThenBy(p => p.Id),
                _ => query.OrderBy(p => p.Name).ThenBy(p => p.Id)
            };
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Unit)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsBySKUAsync(string sku, int? excludedId = null)


        {
            return await _context.Products.AnyAsync(p => p.SKU == sku && (!excludedId.HasValue || p.Id != excludedId.Value));
        }

        public async Task<bool> CategoryExistsAndActiveAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId && c.IsActive);
        }

        public async Task<bool> UnitExistsAndActiveAsync(int unitId)
        {
            return await _context.Units.AnyAsync(u => u.Id == unitId && u.IsActive);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
