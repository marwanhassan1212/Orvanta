using Orvanta.Data;
using Orvanta.Repositories;
using Orvanta.Services;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Customers;

namespace Orvanta.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(IUnitOfWork unitOfWork , ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        public async Task<(bool Success, string? Error)> CreateAsync(CustomerCreateViewModel model)
        {
            if (model == null)
                return (false, "Invalid customer data.");

            var name = model.Name?.Trim();
            var phone = model.Phone?.Trim();
            var email = model.Email?.Trim();
            var address = model.Address?.Trim();
            var notes = model.Notes?.Trim();

            if (string.IsNullOrWhiteSpace(name))
                return (false, "Customer name is required.");

            var customer = new Models.Customer
            {
                Name = name,
                Phone = string.IsNullOrWhiteSpace(phone) ? null : phone,
                Email = string.IsNullOrWhiteSpace(email) ? null : email,
                Address = string.IsNullOrWhiteSpace(address) ? null : address,
                Notes = string.IsNullOrWhiteSpace(notes) ? null : notes,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _customerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return (true, null);
        }

        public async Task<CustomerDetailsViewModel?> GetByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return null;

            return new CustomerDetailsViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                Notes = customer.Notes,
                IsActive = customer.IsActive,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }

        public async Task<PagedResult<CustomerListViewModel>> GetFilteredAsync(CustomerFilterViewModel filter)
        {
            filter ??= new CustomerFilterViewModel();
            filter.PageNumber = Math.Max(1, filter.PageNumber);
            filter.PageSize = filter.PageSize switch
            {
                10 => 10,
                20 => 20,
                50 => 50,
                _ => 10
            };

            var allowedSortColumns = new[] { "name", "createdat" };

            if (!allowedSortColumns.Contains(filter.SortBy?.ToLowerInvariant()))
            {
                filter.SortBy = "name";
            }

            filter.SortBy = filter.SortBy?.ToLowerInvariant() ?? "name";

            filter.SortDirection = filter.SortDirection?.ToLowerInvariant() == "desc" ? "desc" : "asc";
            var result = await _customerRepository.GetFilteredAsync(filter);

            return new PagedResult<CustomerListViewModel>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                Items = result.Items.Select(c => new CustomerListViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone,
                    Email = c.Email,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt
                })
            };
        }

        public async Task<(bool Success, string? Error)> ToggleStatusAsync(int id)
        {
            if (id <= 0)
                return (false, "Invalid customer ID.");

            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return (false, "Customer not found.");

            customer.IsActive = !customer.IsActive;
            customer.UpdatedAt = DateTime.UtcNow;

            _customerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync();

            return (true, null);
        }

        public async Task<(bool Success, string? Error)> UpdateAsync(CustomerUpdateViewModel model)
        {
            if (model == null || model.Id <= 0)
                return (false, "Invalid customer data.");

            var customer = await _customerRepository.GetByIdAsync(model.Id);

            if (customer == null)
                return (false, "Customer not found.");

            var name = model.Name?.Trim();
            var phone = model.Phone?.Trim();
            var email = model.Email?.Trim();
            var address = model.Address?.Trim();
            var notes = model.Notes?.Trim();

            if (string.IsNullOrWhiteSpace(name))
                return (false, "Customer name is required.");

            customer.Name = name;
            customer.Phone = string.IsNullOrWhiteSpace(phone) ? null : phone;
            customer.Email = string.IsNullOrWhiteSpace(email) ? null : email;
            customer.Address = string.IsNullOrWhiteSpace(address) ? null : address;
            customer.Notes = string.IsNullOrWhiteSpace(notes) ? null : notes;
            customer.UpdatedAt = DateTime.UtcNow;

            _customerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }
    }
}
