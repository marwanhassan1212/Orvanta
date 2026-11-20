
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orvanta.Services;
using Orvanta.ViewModels.Warehouses;

namespace Orvanta.Controllers
{
    [Authorize(Policy = "ManagementAccess")]
    public class WarehousesController : Controller
    {
        private readonly IWarehouseService _warehouseService;

        public WarehousesController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        public async Task<IActionResult> Index(WarehouseFilterViewModel filter)
        {
            var result = await _warehouseService.GetFilteredAsync(filter);
            ViewBag.Filter = filter;
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var warehouse = await _warehouseService.GetByIdAsync(id);

            if (warehouse == null)
                return NotFound();

            return View(warehouse);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new WarehouseCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WarehouseCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _warehouseService.CreateAsync(model);

            if (!result.Success)
            {
                ModelState.AddModelError(nameof(model.Code), result.Error!);
                return View(model);
            }

            TempData["Success"] = "Warehouse created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var warehouse = await _warehouseService.GetByIdAsync(id);

            if (warehouse == null)
                return NotFound();

            var model = new WarehouseUpdateViewModel
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Code = warehouse.Code,
                Address = warehouse.Address
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WarehouseUpdateViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var result = await _warehouseService.UpdateAsync(model);

            if (!result.Success)
            {
                if (result.Error == "Warehouse not found.")
                    return NotFound();

                ModelState.AddModelError(nameof(model.Code), result.Error!);
                return View(model);
            }

            TempData["Success"] = "Warehouse updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _warehouseService.ToggleStatusAsync(id);

            if (!result.Success)
                return NotFound();

            TempData["Success"] = "Warehouse status updated successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
