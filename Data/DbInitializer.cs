using BatteryPeykCustomers.Helpers.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

                // Ensure required roles exist (use enum to centralize role names)
                var roles = Enum.GetNames(typeof(Roles));
                foreach (var roleName in roles)
                {
                    var roleExists = _roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult();
                    if (!roleExists)
                    {
                        var roleResult = _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                    }
                }

                // Ensure admin user exists and is in Admin role
                var admin = _userManager.FindByNameAsync("Qwerty").GetAwaiter().GetResult();
                if (admin == null)
                {
                    var result = _userManager.CreateAsync(new IdentityUser
                    {
                        UserName = "Qwerty",
                        Email = "Soltan@batterypeyk.com",
                        EmailConfirmed = true,
                    }, "Qwerty@1").GetAwaiter().GetResult();

                    if (result.Succeeded)
                    {
                        admin = _userManager.FindByNameAsync("Qwerty").GetAwaiter().GetResult();
                    }
                }

                if (admin != null)
                {
                    var inRole = _userManager.IsInRoleAsync(admin, Roles.Admin.ToString()).GetAwaiter().GetResult();
                    if (!inRole)
                    {
                        _userManager.AddToRoleAsync(admin, Roles.Admin.ToString()).GetAwaiter().GetResult();
                    }
                }

                var user = _userManager.FindByNameAsync("operator").GetAwaiter().GetResult();
                if (user == null)
                {
                    var result = _userManager.CreateAsync(new IdentityUser
                    {
                        UserName = "operator",
                        Email = "operator@batterypeyk.com",
                        EmailConfirmed = true,
                    }, "Operator@1").GetAwaiter().GetResult();

                    if (result.Succeeded)
                    {
                        user = _userManager.FindByNameAsync("operator").GetAwaiter().GetResult();
                    }
                }

                if (user != null)
                {
                    var inRole = _userManager.IsInRoleAsync(user, Roles.User.ToString()).GetAwaiter().GetResult();
                    if (!inRole)
                    {
                        _userManager.AddToRoleAsync(user, Roles.User.ToString()).GetAwaiter().GetResult();
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
