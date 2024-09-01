using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Data
{
    public class PaginationService<T> : IPaginationService<T>
    {
        public async Task<IList<T>> GetPaginatedResult(IQueryable<T> query, int currentPage, int pageSize = 10)
        {
            return await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<int> GetCount(IQueryable<T> query)
        {
            var customers = await query.ToListAsync();
            return customers.Count();

        }
    }
}
