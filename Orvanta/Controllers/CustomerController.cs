
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orvanta.Services;
using Orvanta.ViewModels.Customers;

namespace Orvanta.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CustomerFilterViewModel filter)
        {
            var result = await _customerService.GetFilteredAsync(filter);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            return View(customer);
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
        public async Task<IActionResult> Create(CustomerCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _customerService.CreateAsync(model);

            if (!result.Success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    result.Error ?? "Unable to create customer.");

                return View(model);
            }

            TempData["SuccessMessage"] = "Customer created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            var model = new CustomerUpdateViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                Notes = customer.Notes
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Edit(
            int id,
            CustomerUpdateViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var result = await _customerService.UpdateAsync(model);

            if (!result.Success)
            {
                if (result.Error == "Customer not found.")
                    return NotFound();

                ModelState.AddModelError(
                    string.Empty,
                    result.Error ?? "Unable to update customer.");

                return View(model);
            }

            TempData["SuccessMessage"] = "Customer updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _customerService.ToggleStatusAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Customer not found.")
                    return NotFound();

                TempData["ErrorMessage"] = result.Error;
            }
            else
            {
                TempData["SuccessMessage"] = "Customer status updated successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
