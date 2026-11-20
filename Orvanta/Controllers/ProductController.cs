
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orvanta.Service.Unit;
using Orvanta.Services;
using Orvanta.ViewModels.Products;

namespace Orvanta.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IUnitService _unitService;

        public ProductsController(IProductService productService, ICategoryService categoryService,
            IUnitService unitService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _unitService = unitService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ProductFilterViewModel filter)
        {
            var result = await _productService.GetFilteredAsync(filter);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpGet]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }

            var result = await _productService.CreateAsync(model);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Error ?? "Unable to create product.");
                await LoadDropdownsAsync();
                return View(model);
            }
            TempData["SuccessMessage"] = "Product created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            var model = new ProductUpdateViewModel
            {
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                Description = product.Description,
                CategoryId = product.CategoryId,
                UnitId = product.UnitId,
                PurchasePrice = product.PurchasePrice,
                SellingPrice = product.SellingPrice
            };

            await LoadDropdownsAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Edit(
            int id,
            ProductUpdateViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }

            var result = await _productService.UpdateAsync(model);

            if (!result.Success)
            {
                if (result.Error == "Product not found.")
                    return NotFound();

                ModelState.AddModelError(
                    string.Empty,
                    result.Error ?? "Unable to update product.");

                await LoadDropdownsAsync();
                return View(model);
            }

            TempData["SuccessMessage"] = "Product updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _productService.ToggleStatusAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Product not found.")
                    return NotFound();

                TempData["ErrorMessage"] = result.Error;
            }
            else
            {
                TempData["SuccessMessage"] = "Product status updated successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdownsAsync()
        {
            var categories = await _categoryService.GetActiveAsync();
            var units = await _unitService.GetActiveAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Units = new SelectList(units, "Id", "Name");
        }

    }
}
