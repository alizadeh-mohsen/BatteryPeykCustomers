using Microsoft.AspNetCore.Mvc.RazorPages;
using BatteryPeykCustomers.Data;
using Microsoft.AspNetCore.Authorization;
using BatteryPeykCustomers.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Model;
using BatteryPeykCustomers.Helpers;
using Microsoft.AspNetCore.Mvc;

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
                    Where(c => c.ReplaceDate.AddDays(-30) <= DateTime.Today).OrderBy(c => c.PurchaseDate).Select(
                    c => new Expire
                    {
                        Id = c.Id,
                        CustomerId = c.Customer.Id,
                        Name = c.Customer.Name,
                        Phone = c.Customer.Phone,
                        Make = c.Make,
                        Battery = c.Battery,
                        PurchaseDate = c.PurchaseDate,
                        ReplaceDate = c.ReplaceDate,
                        LifeExpectancy = c.LifeExpectancy,

                    })
                    .ToListAsync();


                vm = new ExpireViewModel
                {
                    Expires = result
                };

            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var name = Request.Form["name"];
                var phone = Request.Form["phone"];
                SmsHelper smsHelper = new SmsHelper(name, phone);
                var result = await smsHelper.SendSms(MessageType.Expire);

                return Page();


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
