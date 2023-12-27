using Microsoft.AspNetCore.Mvc.RazorPages;
using BatteryPeykCustomers.Data;
using Microsoft.AspNetCore.Authorization;
using BatteryPeykCustomers.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    [Authorize]
    public class ExpireModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExpireModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ExpireViewModel vm { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Customer != null)
            {
                var result = await _context.Car.Include(c => c.Customer).
                    Where(c => c.ReplaceDate.AddDays(-30) <= DateTime.Today).Select(
                    c => new Expire
                    {
                        Id = c.Id,
                        Name = c.Customer.Name,
                        Phone = c.Customer.Phone,
                        Make = c.Make,
                        Battery = c.Battery,
                        ReplaceDate = c.ReplaceDate

                    })
                    .ToListAsync();


                vm = new ExpireViewModel
                {
                    Expires = result
                };

            }
        }
    }
}
