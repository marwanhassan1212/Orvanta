
using Orvanta.Models;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Customers;

namespace Orvanta.Repositories
{
    public interface ICustomerRepository
    {
        Task<PagedResult<Models.Customer>> GetFilteredAsync(CustomerFilterViewModel filter);
        Task<Models.Customer?> GetByIdAsync(int id);
        Task AddAsync(Models.Customer customer);
        void Update(Models.Customer customer);
    }
}
