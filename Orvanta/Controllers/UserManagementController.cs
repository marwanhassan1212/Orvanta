using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orvanta.Models;
using Orvanta.ViewModels.Users;

namespace Orvanta.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.OrderBy(user => user.FirstName)
                .ThenBy(user => user.LastName).ToListAsync();

            var userList = new List<UserListViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserListViewModel
                {
                    Id = user.Id,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email ?? string.Empty,
                    Role = roles.FirstOrDefault() ?? "No Role",
                    IsActive = user.IsActive
                });
            }
            return View(userList);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadRolesAsync();
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadRolesAsync();
                return View(model);
            }

            var normalizedEmail = model.Email.Trim();

            if (await _userManager.FindByEmailAsync(normalizedEmail) != null)
            {
                ModelState.AddModelError(nameof(model.Email), "This email is already registered.");
                await LoadRolesAsync();
                return View(model);
            }

            var allowedRoles = await _roleManager.Roles.Select(role => role.Name!).ToListAsync();

            if (!allowedRoles.Contains(model.Role))
            {
                ModelState.AddModelError(nameof(model.Role), "Selected role is invalid.");
                await LoadRolesAsync();
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = normalizedEmail,
                Email = normalizedEmail,
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
                IsActive = true,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                await LoadRolesAsync();
                return View(model);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, model.Role);

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                await LoadRolesAsync();
                return View(model);
            }

            TempData["SuccessMessage"] = "User created successfully.";
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            return View(new EditUserViewModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                Role = roles.FirstOrDefault() ?? string.Empty,
                IsActive = user.IsActive
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();

            if (user.Id == _userManager.GetUserId(User) && !model.IsActive)
            {
                ModelState.AddModelError(nameof(model.IsActive), "You cannot deactivate your own account.");
                return View(model);
            }

            var allowedRoles = new[]
            {
                "Admin",
                "Manager",
                "Accountant",
                "SalesEmployee",
                "WarehouseEmployee"
            };

            if (!allowedRoles.Contains(model.Role))
            {
                ModelState.AddModelError(nameof(model.Role), "Selected role is invalid.");
                return View(model);
            }

            var normalizedEmail = model.Email.Trim();
            var existingUser = await _userManager.FindByEmailAsync(normalizedEmail);

            if (existingUser != null && existingUser.Id != user.Id)
            {
                ModelState.AddModelError(nameof(model.Email), "This email is already registered.");
                return View(model);
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var currentRole = currentRoles.FirstOrDefault();

            // Protect the last active Admin.
            if (currentRole == "Admin" && (model.Role != "Admin" || !model.IsActive))

            {
                var activeAdmins = await _userManager.GetUsersInRoleAsync("Admin");
                var activeAdminCount = activeAdmins.Count(admin => admin.IsActive);

                if (user.IsActive && activeAdminCount <= 1)
                {
                    ModelState.AddModelError(string.Empty, "The last active Admin cannot be demoted or deactivated.");
                    return View(model);
                }
            }

            user.FirstName = model.FirstName.Trim();
            user.LastName = model.LastName.Trim();
            user.Email = normalizedEmail;
            user.UserName = normalizedEmail;
            user.IsActive = model.IsActive;
            user.UpdatedAt = DateTime.UtcNow;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            if (currentRole != model.Role)
            {
                if (currentRoles.Any())
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeResult.Succeeded)
                    {
                        foreach (var error in removeResult.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);

                        return View(model);
                    }
                }

                var addResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!addResult.Succeeded)
                {
                    foreach (var error in addResult.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                    return View(model);
                }
            }

            await _userManager.UpdateSecurityStampAsync(user);
            TempData["SuccessMessage"] = "User updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadRolesAsync()
        {
            var roles = await _roleManager.Roles.Where(role => role.Name != null)
                .Select(role => role.Name!).ToListAsync();
            ViewBag.Roles = roles;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            if (user.Id == _userManager.GetUserId(User))
            {
                TempData["ErrorMessage"] = "You cannot deactivate your own account.";
                return RedirectToAction(nameof(Index));
            }

            if (user.IsActive)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Admin"))
                {
                    var admins = await _userManager.GetUsersInRoleAsync("Admin");
                    var activeAdminCount = admins.Count(admin => admin.IsActive);
                    if (activeAdminCount <= 1)
                    {
                        TempData["ErrorMessage"] = "The last active Admin cannot be deactivated.";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            user.IsActive = !user.IsActive;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = "Failed to update user status.";
                return RedirectToAction(nameof(Index));
            }

            await _userManager.UpdateSecurityStampAsync(user);

            TempData["SuccessMessage"] = user.IsActive ? "User activated successfully." : "User deactivated successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}