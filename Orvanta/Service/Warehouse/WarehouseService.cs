
using Orvanta.Data;
using Orvanta.Models;
using Orvanta.Repositories;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Warehouses;

namespace Orvanta.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WarehouseService(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
        {
            _warehouseRepository = warehouseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<WarehouseListViewModel>> GetFilteredAsync(WarehouseFilterViewModel filter)
        {
            filter.PageNumber = Math.Max(1, filter.PageNumber);

            filter.PageSize = filter.PageSize switch
            {
                10 or 20 or 50 => filter.PageSize,
                _ => 10
            };

            filter.SortBy = filter.SortBy?.Trim().ToLowerInvariant() ?? "name";

            if (filter.SortBy != "name" &&
                filter.SortBy != "code" &&
                filter.SortBy != "createdat")
            {
                filter.SortBy = "name";
            }

            filter.SortDirection =
                filter.SortDirection?.Trim().ToLowerInvariant() == "desc" ? "desc" : "asc";

            var result = await _warehouseRepository.GetFilteredAsync(filter);

            return new PagedResult<WarehouseListViewModel>
            {
                Items = result.Items.Select(w => new WarehouseListViewModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    Code = w.Code,
                    Address = w.Address,
                    IsActive = w.IsActive,
                    CreatedAt = w.CreatedAt
                }).ToList(),

                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
        }

        public async Task<WarehouseDetailsViewModel?> GetByIdAsync(int id)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(id);

            if (warehouse == null)
                return null;

            return new WarehouseDetailsViewModel
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Code = warehouse.Code,
                Address = warehouse.Address,
                IsActive = warehouse.IsActive,
                CreatedAt = warehouse.CreatedAt,
                UpdatedAt = warehouse.UpdatedAt
            };
        }

        public async Task<(bool Success, string? Error)> CreateAsync(WarehouseCreateViewModel model)
        {
            var code = model.Code.Trim().ToUpperInvariant();

            if (await _warehouseRepository.CodeExistsAsync(code))
                return (false, "This warehouse code already exists.");

            var warehouse = new Warehouse
            {
                Name = model.Name.Trim(),
                Code = code,
                Address = NormalizeOptional(model.Address),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _warehouseRepository.AddAsync(warehouse);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Success, string? Error)> UpdateAsync(WarehouseUpdateViewModel model)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(model.Id);

            if (warehouse == null)
                return (false, "Warehouse not found.");

            var code = model.Code.Trim().ToUpperInvariant();

            if (await _warehouseRepository.CodeExistsAsync(code, model.Id))
                return (false, "This warehouse code already exists.");

            warehouse.Name = model.Name.Trim();
            warehouse.Code = code;
            warehouse.Address = NormalizeOptional(model.Address);
            warehouse.UpdatedAt = DateTime.UtcNow;

            _warehouseRepository.Update(warehouse);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Success, string? Error)> ToggleStatusAsync(int id)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(id);

            if (warehouse == null)
                return (false, "Warehouse not found.");

            warehouse.IsActive = !warehouse.IsActive;
            warehouse.UpdatedAt = DateTime.UtcNow;

            _warehouseRepository.Update(warehouse);
            await _unitOfWork.SaveChangesAsync();

            return (true, null);
        }

        public async Task<IEnumerable<WarehouseListViewModel>> GetActiveAsync()
        {
            var warehouses = await _warehouseRepository.GetActiveAsync();

            return warehouses.Select(w => new WarehouseListViewModel
            {
                Id = w.Id,
                Name = w.Name,
                Code = w.Code,
                Address = w.Address,
                IsActive = w.IsActive,
                CreatedAt = w.CreatedAt
            }).ToList();
        }

        private static string? NormalizeOptional(string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : value.Trim();
        }
    }
}
