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

                var user = _userManager.FindByNameAsync("Qwerty").GetAwaiter().GetResult();
                if (user == null)
                {
                    var result = _userManager.CreateAsync(new IdentityUser
                    {
                        UserName = "Qwerty",
                        Email = "Soltan@batterypeyk.com",
                        EmailConfirmed = true,
                    }, "Qwerty@1").GetAwaiter().GetResult();
                    if (result != null)
                    {
                        _db.Database.Migrate();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
