using BatteryPeykCustomers.Model;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
