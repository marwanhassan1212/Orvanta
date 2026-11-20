
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orvanta.Service.Unit;
using Orvanta.ViewModels.Units;

namespace Orvanta.Controllers
{
    [Authorize]
    public class UnitsController : Controller
    {
        private readonly IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }
        public async Task<IActionResult> Index(UnitFilterViewModel filter)
        {
            var result = await _unitService.GetFilteredAsync(filter);
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);

            if (unit == null)
                return NotFound();

            return View(unit);
        }

        [HttpGet]
        [Authorize(Policy = "ManagementAccess")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Create(UnitCreateViewModel model)

        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _unitService.CreateAsync(model);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Error ?? "Unable to create unit.");
                return View(model);
            }

            TempData["Success"] = "Unit created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Edit(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);
            if (unit == null)
                return NotFound();

            var model = new UnitUpdateViewModel
            {
                Id = unit.Id,
                Name = unit.Name,
                Symbol = unit.Symbol
            };
            return View(model);
        }

        // POST: Units/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Edit(int id, UnitUpdateViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var result = await _unitService.UpdateAsync(model);

            if (!result.Success)
            {
                if (result.Error == "Unit not found.")
                    return NotFound();

                ModelState.AddModelError(string.Empty, result.Error ?? "Unable to update unit.");
                return View(model);
            }

            TempData["Success"] = "Unit updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Units/ToggleStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _unitService.ToggleStatusAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Unit not found.")
                    return NotFound();

                TempData["Error"] = result.Error;
            }
            else
            {
                TempData["Success"] = "Unit status updated successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
