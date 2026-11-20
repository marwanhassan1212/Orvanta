
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orvanta.Services;
using Orvanta.ViewModels.Suppliers;

namespace Orvanta.Controllers
{
    [Authorize(Policy = "SupplierReadAccess")]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IActionResult> Index(SupplierFilterViewModel filter)
        {
            var result = await _supplierService.GetFilteredAsync(filter);
            ViewBag.Filter = filter;
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);

            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        [Authorize(Policy = "ManagementAccess")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new SupplierCreateViewModel());
        }

        [Authorize(Policy = "ManagementAccess")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _supplierService.CreateAsync(model);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                return View(model);
            }

            TempData["Success"] = "Supplier created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "ManagementAccess")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);

            if (supplier == null)
                return NotFound();

            var model = new SupplierUpdateViewModel
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Address = supplier.Address,
                Notes = supplier.Notes
            };

            return View(model);
        }

        [Authorize(Policy = "ManagementAccess")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            SupplierUpdateViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var result = await _supplierService.UpdateAsync(model);

            if (!result.Success)
            {
                if (result.Error == "Supplier not found.")
                    return NotFound();

                ModelState.AddModelError(string.Empty, result.Error!);
                return View(model);
            }

            TempData["Success"] = "Supplier updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "ManagementAccess")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _supplierService.ToggleStatusAsync(id);

            if (!result.Success)
                return NotFound();

            TempData["Success"] = "Supplier status updated successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
