using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Data
{
    public interface IPaginationService<T>
    {
        Task<IList<T>> GetPaginatedResult(IQueryable<T> query, int currentPage, int pageSize);
        Task<int> GetCount(IQueryable<T> query);

    }
}
