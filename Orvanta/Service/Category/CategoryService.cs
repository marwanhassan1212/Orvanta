using Orvanta.Data;
using Orvanta.Repositories;
using Orvanta.Services;
using Orvanta.ViewModels.Categories;
using Orvanta.ViewModels.Common;

namespace Orvanta.Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(IUnitOfWork unitOfWork , ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository; 
        }
        public async Task<(bool Success, string? Error)> CreateAsync(CategoryCreateViewModel model)
        {
            if (model == null)
                return (false, "Invalid category data.");

            var name = model.Name?.Trim();
            var description = model.Description?.Trim();

            if (string.IsNullOrWhiteSpace(name))
                return (false, "Category name is required.");

            if (await _categoryRepository.ExistsByNameAsync(name))
                return (false, "A category with this name already exists.");

            var category = new Models.Category
            {
                Name = name,
                Description = string.IsNullOrWhiteSpace(description) ? null : description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }

        public async Task<IReadOnlyList<CategoryListViewModel>> GetActiveAsync()
        {
            var categories = await _categoryRepository.GetActiveAsync();
            return categories.Select(c => new CategoryListViewModel
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive
            }).ToList();
        }

        public async Task<IReadOnlyList<CategoryListViewModel>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(x => new CategoryListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                ProductsCount = x.Products.Count
            }).ToList();
        }

        public async Task<CategoryDetailsViewModel?> GetByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return null;

            return new CategoryDetailsViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
                ProductsCount = category.Products.Count
            };
        }

        public async Task<(bool Success, string? Error)> ToggleStatusAsync(int id)
        {
            if (id <= 0)
                return (false, "Invalid category ID.");

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return (false, "Category not found.");

            if (category.IsActive)
            {
                var hasProducts = await _categoryRepository.HasProductsAsync(id);

                if (hasProducts)
                    return (false, "Cannot deactivate a category that has products.");
            }

            category.IsActive = !category.IsActive;
            category.UpdatedAt = DateTime.UtcNow;

            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Success, string? Error)> UpdateAsync(CategoryUpdateViewModel model)
        {
            if (model == null || model.Id <= 0)
                return (false, "Invalid category data.");

            var category = await _categoryRepository.GetByIdAsync(model.Id);

            if (category == null)
                return (false, "Category not found.");

            var name = model.Name?.Trim();
            var description = model.Description?.Trim();

            if (string.IsNullOrWhiteSpace(name))
                return (false, "Category name is required.");

            if (await _categoryRepository.ExistsByNameAsync(name, model.Id))
                return (false, "Another category already uses this name.");

            category.Name = name;
            category.Description = string.IsNullOrWhiteSpace(description) ? null : description;
            category.UpdatedAt = DateTime.UtcNow;

            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }


        public async Task<PagedResult<CategoryListViewModel>> GetFilteredAsync(CategoryFilterViewModel filter)
        {
            filter ??= new CategoryFilterViewModel();
            filter.PageNumber = Math.Max(1, filter.PageNumber);
            filter.PageSize = filter.PageSize switch
            {
                10 => 10,
                20 => 20,
                50 => 50,
                _ => 10
            };

            var allowedSortColumns = new[] { "name", "createdat" };

            if (!allowedSortColumns.Contains(filter.SortBy?.ToLowerInvariant()))
            {
                filter.SortBy = "name";
            }

            filter.SortBy = filter.SortBy?.ToLowerInvariant() ?? "name";

            filter.SortDirection = filter.SortDirection?.ToLowerInvariant() == "desc" ? "desc" : "asc";

            var result = await _categoryRepository.GetFilteredAsync(filter);

            return new PagedResult<CategoryListViewModel>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,

                Items = result.Items.Select(c => new CategoryListViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsActive = c.IsActive,
                    ProductsCount = c.Products.Count
                })
            };
        }

    }
}
