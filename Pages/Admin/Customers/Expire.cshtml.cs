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
        private readonly IConfiguration _configuration;

        public ExpireModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public ExpireViewModel vm { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Customer != null)
            {
                var result = await _context.Car.Include(c => c.Customer).
                    Where(c => c.ReplaceDate.AddDays(-30) <= DateTime.Today && c.Sms == 0).OrderBy(c => c.PurchaseDate).Select(
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
                    Expires = result,
                    count = result.Count
                };

            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var name = Request.Form["name"];
                var phone = Request.Form["phone"];
                var Id = int.Parse(Request.Form["Id"]);
                SmsHelper smsHelper = new SmsHelper(name, phone, _configuration);

                var respone = await smsHelper.SendSms(MessageType.Update);

                if (!respone.IsSuccess)
                {
                    TempData["error"] = "خطا در ارسال پیامک: " + respone.Message;
                }
                else
                {
                    var car = await _context.Car.FindAsync(Id);
                    car.Sms = 1;
                    _context.Update(car);
                    var result = await _context.SaveChangesAsync();

                }
                return RedirectToPage("/Admin/Customers/Expire");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
