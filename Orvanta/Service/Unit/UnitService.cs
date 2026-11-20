using Orvanta.Data;
using Orvanta.Models;
using Orvanta.Repositories;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Units;

namespace Orvanta.Service.Unit
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UnitService(IUnitRepository unitRepository , IUnitOfWork unitOfWork)
        {
            _unitRepository = unitRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool Success, string? Error)> CreateAsync(UnitCreateViewModel model)
        {
            if (model == null)
                return (false, "Invalid unit data.");

            var name = model.Name?.Trim();
            var symbol = model.Symbol?.Trim();

            if (string.IsNullOrWhiteSpace(name))
                return (false, "Unit name is required.");

            if (string.IsNullOrWhiteSpace(symbol))
                return (false, "Unit symbol is required.");

            if (await _unitRepository.ExistsByNameAsync(name))
                return (false, "A unit with this name already exists.");

            if (await _unitRepository.ExistsBySymbolAsync(symbol))
                return (false, "A unit with this symbol already exists.");

            var unit = new Models.Unit
            {
                Name = name,
                Symbol = symbol,
                IsActive = true
            };

            await _unitRepository.AddAsync(unit);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }

        public async Task<IReadOnlyList<UnitListViewModel>> GetActiveAsync()
        {
            var activeUnits = await _unitRepository.GetActiveAsync();
            return activeUnits.Select(x => new UnitListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Symbol = x.Symbol,
                IsActive = x.IsActive
            }).ToList();
        }

        public async Task<IReadOnlyList<UnitListViewModel>> GetAllAsync()
        {
            var units = await _unitRepository.GetAllAsync();
            return units.Select(x => new UnitListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Symbol = x.Symbol,
                IsActive = x.IsActive
            }).ToList();
        }

        public async Task<UnitDetailsViewModel?> GetByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var unit = await _unitRepository.GetByIdAsync(id);
            if (unit == null)
                return null;

            return new UnitDetailsViewModel
            {
                Id = unit.Id,
                Name = unit.Name,
                Symbol = unit.Symbol,
                IsActive = unit.IsActive,
                ProductsCount = unit.Products.Count
            };
        }

        public async Task<(bool Success, string? Error)> ToggleStatusAsync(int id)
        {
            if (id <= 0)
                return (false, "Invalid unit ID.");

            var unit = await _unitRepository.GetByIdAsync(id);

            if (unit == null)
                return (false, "Unit not found.");

            if (unit.IsActive)
            {
                var hasActiveProducts = await _unitRepository.HasActiveProductsAsync(id);
                if (hasActiveProducts)
                {
                    return (false, "Cannot deactivate this unit because it is assigned to active products.");
                }
            }

            unit.IsActive = !unit.IsActive;
            _unitRepository.Update(unit);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Success, string? Error)> UpdateAsync(UnitUpdateViewModel model)
        {
            if (model == null)
                return (false, "Invalid unit data.");

            if (model.Id <= 0)
                return (false, "Invalid unit ID.");

            var unit = await _unitRepository.GetByIdAsync(model.Id);

            if (unit == null)
                return (false, "Unit not found.");

            var name = model.Name?.Trim();
            var symbol = model.Symbol?.Trim();

            if (string.IsNullOrWhiteSpace(name))
                return (false, "Unit name is required.");

            if (string.IsNullOrWhiteSpace(symbol))
                return (false, "Unit symbol is required.");

            if (await _unitRepository.ExistsByNameAsync(name, model.Id))
                return (false, "Another unit already uses this name.");

            if (await _unitRepository.ExistsBySymbolAsync(symbol, model.Id))
                return (false, "Another unit already uses this symbol.");

            unit.Name = name;
            unit.Symbol = symbol;
            _unitRepository.Update(unit);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }


        public async Task<PagedResult<UnitListViewModel>> GetFilteredAsync(UnitFilterViewModel filter)

        {
            filter ??= new UnitFilterViewModel();
            filter.PageNumber = Math.Max(1, filter.PageNumber);
            filter.PageSize = filter.PageSize switch
            {
                10 => 10,
                20 => 20,
                50 => 50,
                _ => 10
            };

            var allowedSortColumns = new[] { "name", "symbol" };

            if (!allowedSortColumns.Contains(filter.SortBy?.ToLowerInvariant()))
            {
                filter.SortBy = "name";
            }

            filter.SortBy = filter.SortBy?.ToLowerInvariant() ?? "name";

            filter.SortDirection = filter.SortDirection?.ToLowerInvariant() == "desc" ? "desc" : "asc";
            var result = await _unitRepository.GetFilteredAsync(filter);

            return new PagedResult<UnitListViewModel>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,

                Items = result.Items.Select(u => new UnitListViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Symbol = u.Symbol,
                    IsActive = u.IsActive,
                    ProductsCount = u.Products.Count
                })
            };
        }

    }
}
