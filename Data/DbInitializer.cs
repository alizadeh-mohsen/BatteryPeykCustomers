using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public DbInitializer(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            this._db = applicationDbContext;
            this._userManager = userManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw;
            }
            var user = _userManager.FindByNameAsync("alis").GetAwaiter().GetResult();
            if (user == null)
            {
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "alis",
                    Email= "alis@b.com",
                    EmailConfirmed= true,
                }, "Admin@123").GetAwaiter().GetResult();
            }
        }
    }
}
