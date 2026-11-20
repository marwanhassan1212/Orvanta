
using Orvanta.Data;
using Orvanta.Models;
using Orvanta.Repositories;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Products;

namespace Orvanta.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<ProductListViewModel>> GetFilteredAsync(ProductFilterViewModel filter)

        {
            filter ??= new ProductFilterViewModel();
            filter.PageNumber = Math.Max(1, filter.PageNumber);
            filter.PageSize = filter.PageSize switch
            {
                10 => 10,
                20 => 20,
                50 => 50,
                _ => 10
            };

            var allowedSortColumns = new[]
            {
                "name", "sku", "price", "createdat"
            };

            if (!allowedSortColumns.Contains(filter.SortBy?.ToLowerInvariant()))
            {
                filter.SortBy = "name";
            }

            filter.SortDirection = filter.SortDirection?.ToLowerInvariant() == "desc" ? "desc" : "asc";
            var result = await _productRepository.GetFilteredAsync(filter);

            return new PagedResult<ProductListViewModel>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,

                Items = result.Items.Select(p =>
                    new ProductListViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        SKU = p.SKU,
                        CategoryName = p.Category.Name,
                        UnitName = p.Unit.Name,
                        SellingPrice = p.SellingPrice,
                        IsActive = p.IsActive
                    })
            };
        }

        public async Task<ProductDetailsViewModel?> GetByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductDetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                UnitId = product.UnitId,
                UnitName = product.Unit.Name,
                PurchasePrice = product.PurchasePrice,
                SellingPrice = product.SellingPrice,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public async Task<(bool Success, string? Error)> CreateAsync(ProductCreateViewModel model)
        {
            if (model == null)
                return (false, "Invalid product data.");

            var name = model.Name?.Trim();
            var sku = model.SKU?.Trim().ToUpperInvariant();
            var description = model.Description?.Trim();

            if (string.IsNullOrWhiteSpace(name))
                return (false, "Product name is required.");

            if (string.IsNullOrWhiteSpace(sku))
                return (false, "Product SKU is required.");

            if (model.PurchasePrice < 0 || model.SellingPrice < 0)
                return (false, "Product prices cannot be negative.");

            if (await _productRepository.ExistsBySKUAsync(sku))
                return (false, "A product with this SKU already exists.");

            if (!await _productRepository.CategoryExistsAndActiveAsync(
                model.CategoryId))
            {
                return (false, "Selected category does not exist or is inactive.");
            }

            if (!await _productRepository.UnitExistsAndActiveAsync(
                model.UnitId))
            {
                return (false, "Selected unit does not exist or is inactive.");
            }

            var product = new Product
            {
                Name = name,
                SKU = sku,
                Description = string.IsNullOrWhiteSpace(description) ? null : description,
                CategoryId = model.CategoryId,
                UnitId = model.UnitId,
                PurchasePrice = model.PurchasePrice,
                SellingPrice = model.SellingPrice,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return (true, null);
        }

        public async Task<(bool Success, string? Error)> UpdateAsync(ProductUpdateViewModel model)

        {
            if (model == null || model.Id <= 0)
                return (false, "Invalid product data.");

            var product = await _productRepository.GetByIdAsync(model.Id);

            if (product == null)
                return (false, "Product not found.");

            var name = model.Name?.Trim();
            var sku = model.SKU?.Trim().ToUpperInvariant();
            var description = model.Description?.Trim();

            if (string.IsNullOrWhiteSpace(name))
                return (false, "Product name is required.");

            if (string.IsNullOrWhiteSpace(sku))
                return (false, "Product SKU is required.");

            if (model.PurchasePrice < 0 || model.SellingPrice < 0)
                return (false, "Product prices cannot be negative.");

            if (await _productRepository.ExistsBySKUAsync(sku, model.Id))
                return (false, "Another product already uses this SKU.");

            if (!await _productRepository.CategoryExistsAndActiveAsync(
                model.CategoryId))
            {
                return (false, "Selected category does not exist or is inactive.");
            }

            if (!await _productRepository.UnitExistsAndActiveAsync(
                model.UnitId))
            {
                return (false, "Selected unit does not exist or is inactive.");
            }

            product.Name = name;
            product.SKU = sku;
            product.Description = string.IsNullOrWhiteSpace(description) ? null : description;
            product.CategoryId = model.CategoryId;
            product.UnitId = model.UnitId;
            product.PurchasePrice = model.PurchasePrice;
            product.SellingPrice = model.SellingPrice;
            product.UpdatedAt = DateTime.UtcNow;

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Success, string? Error)> ToggleStatusAsync(int id)

        {
            if (id <= 0)
                return (false, "Invalid product ID.");

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return (false, "Product not found.");

            product.IsActive = !product.IsActive;
            product.UpdatedAt = DateTime.UtcNow;

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return (true, null);
        }
    }
}
