using Orvanta.Data;
using Orvanta.Repositories;
using Orvanta.Services;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Suppliers;

namespace Orvanta.Service.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SupplierService(ISupplierRepository supplierRepository , IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool Success, string? Error)> CreateAsync(SupplierCreateViewModel model)
        {
            var supplier = new Models.Supplier
            {
                Name = model.Name.Trim(),
                Phone = NormalizeOptional(model.Phone),
                Email = NormalizeOptional(model.Email),
                Address = NormalizeOptional(model.Address),
                Notes = NormalizeOptional(model.Notes),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _supplierRepository.AddAsync(supplier);
            await _unitOfWork.SaveChangesAsync();

            return (true, null);
        }

        public async Task<SupplierDetailsViewModel?> GetByIdAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
                return null;

            return new SupplierDetailsViewModel
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Address = supplier.Address,
                Notes = supplier.Notes,
                IsActive = supplier.IsActive,
                CreatedAt = supplier.CreatedAt,
                UpdatedAt = supplier.UpdatedAt
            };
        }

        public async Task<PagedResult<SupplierListViewModel>> GetFilteredAsync(SupplierFilterViewModel filter)
        {
            filter.PageNumber = Math.Max(1, filter.PageNumber);
            filter.PageSize = filter.PageSize switch
            {
                10 or 20 or 50 => filter.PageSize,
                _ => 10
            };

            filter.SortBy = filter.SortBy?.Trim().ToLowerInvariant() ?? "name";

            if (filter.SortBy != "name" && filter.SortBy != "createdat")
                filter.SortBy = "name";

            filter.SortDirection = filter.SortDirection?.Trim().ToLowerInvariant() == "desc" ? "desc" : "asc";
            var result = await _supplierRepository.GetFilteredAsync(filter);

            return new PagedResult<SupplierListViewModel>
            {
                Items = result.Items.Select(s => new SupplierListViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone,
                    Email = s.Email,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt
                }).ToList(),

                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
        }

        public async Task<(bool Success, string? Error)> ToggleStatusAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);

            if (supplier == null)
                return (false, "Supplier not found.");

            supplier.IsActive = !supplier.IsActive;
            supplier.UpdatedAt = DateTime.UtcNow;

            _supplierRepository.Update(supplier);
            await _unitOfWork.SaveChangesAsync();

            return (true, null);
        }

        public async Task<(bool Success, string? Error)> UpdateAsync(SupplierUpdateViewModel model)
        {
            var supplier = await _supplierRepository.GetByIdAsync(model.Id);

            if (supplier == null)
                return (false, "Supplier not found.");

            supplier.Name = model.Name.Trim();
            supplier.Phone = NormalizeOptional(model.Phone);
            supplier.Email = NormalizeOptional(model.Email);
            supplier.Address = NormalizeOptional(model.Address);
            supplier.Notes = NormalizeOptional(model.Notes);
            supplier.UpdatedAt = DateTime.UtcNow;

            _supplierRepository.Update(supplier);
            await _unitOfWork.SaveChangesAsync();

            return (true, null);
        }

        private static string? NormalizeOptional(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
