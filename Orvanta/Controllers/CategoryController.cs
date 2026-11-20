
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orvanta.Services;
using Orvanta.ViewModels.Categories;

namespace Orvanta.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CategoryFilterViewModel filter)
        {
            var result = await _categoryService.GetFilteredAsync(filter);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            return View(category);
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
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _categoryService.CreateAsync(model);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Error ?? "Unable to create category.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Category created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            var model = new CategoryUpdateViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Edit(
            int id,
            CategoryUpdateViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var result = await _categoryService.UpdateAsync(model);

            if (!result.Success)
            {
                if (result.Error == "Category not found.")
                    return NotFound();

                ModelState.AddModelError(string.Empty, result.Error ?? "Unable to update category.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Category updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _categoryService.ToggleStatusAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Category not found.")
                    return NotFound();

                TempData["ErrorMessage"] = result.Error;
            }
            else
            {
                TempData["SuccessMessage"] = "Category status updated successfully.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
