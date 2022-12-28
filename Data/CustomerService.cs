using BatteryPeykCustomers.Model;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Data
{
    public class CustomerService : ICustomerService
    {
        public async Task<IList<Customer>> GetPaginatedResult(IQueryable<Customer> query, int currentPage, int pageSize = 10)
        {
            return await query.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderByDescending(c => c.PurchaseDate).ToListAsync();
        }
        public async Task<int> GetCount(IQueryable<Customer> query)
        {
            var customers = await query.ToListAsync();
            return customers.Count();

        }
    }
}
