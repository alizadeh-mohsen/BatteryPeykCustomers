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
        public DbSet<Amper> Amper { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<Battery> Battery { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Counterparty> Counterparty { get; set; }
        public DbSet<Credit> Credit { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Debt> Debt { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<Warranty> Warranty { get; set; }
        public DbSet<WarrantyBattery> WarrantyBattery { get; set; }
        public DbSet<NewBattery> NewBattery { get; set; }
        public DbSet<OldBattery> OldBattery { get; set; }
        public DbSet<Profit> Profit { get; set; }
        public DbSet<Reason> Reason { get; set; }
        public DbSet<Order> Sale { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Used> Used { get; set; }
        public DbSet<UsedBattery> UsedBattery { get; set; }
        public DbSet<UsedHistory> UsedHistory { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Battery>()
                    .HasIndex(x => new
                    {
                        x.AmperId,
                        x.BatteryBrandId
                    })
                    .IsUnique();
            builder.Entity<NewBattery>()
                    .HasIndex(x => new
                    {
                        x.BranchId,
                        x.BatteryId
                    })
                    .IsUnique();
            builder.Entity<OldBattery>()
                    .HasIndex(x => new
                    {
                        x.BranchId,
                        x.BatteryId
                    })
                    .IsUnique();

            builder.Entity<UsedBattery>()
                    .HasIndex(x => new
                    {
                        x.BranchId,
                        x.BatteryId
                    })
                    .IsUnique();

            builder.Entity<WarrantyBattery>()
                    .HasIndex(x => new
                    {
                        x.BranchId,
                        x.BatteryId
                    })
                    .IsUnique();

            builder.Entity<BankAccount>()
                    .HasIndex(x => x.AccountNumber)
                    .IsUnique();

            builder.Entity<Branch>()
                    .HasIndex(x => x.Code)
                    .IsUnique();

            builder.Entity<Counterparty>()
                    .HasIndex(x => x.Code)
                    .IsUnique();
            builder.Entity<Customer>()
                    .HasIndex(x => x.Phone)
                    .IsUnique();
            builder.Entity<Customer>()
                    .HasIndex(x => x.CustomerNumber)
                    .IsUnique();

        }

    }


}
