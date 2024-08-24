using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BatteryPeykCustomers.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Battery> Battery { get; set; }
        public DbSet<Amper> Amper { get; set; }
        public DbSet<Company> Company { get; set; }

        public DbSet<Counterparty> Counterparty { get; set; }
        public DbSet<Reason> Reason { get; set; }
        public DbSet<Debt> Debt { get; set; }
        public DbSet<Expense> Expense { get; set; }

    }
}
