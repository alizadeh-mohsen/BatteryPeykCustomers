using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Data
{
    public interface ICustomerService
    {
        Task<IList<Customer>> GetPaginatedResult(IQueryable<Customer> query, int currentPage, int pageSize);
        Task<int> GetCount(IQueryable<Customer> query);

    }
}
