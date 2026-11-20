
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Customers;

namespace Orvanta.Services
{
    public interface ICustomerService
    {
        Task<PagedResult<CustomerListViewModel>> GetFilteredAsync(CustomerFilterViewModel filter);
        Task<CustomerDetailsViewModel?> GetByIdAsync(int id);
        Task<(bool Success, string? Error)> CreateAsync(CustomerCreateViewModel model);
        Task<(bool Success, string? Error)> UpdateAsync(CustomerUpdateViewModel model);
        Task<(bool Success, string? Error)> ToggleStatusAsync(int id);
    }
}
