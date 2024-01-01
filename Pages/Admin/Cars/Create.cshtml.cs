using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using BatteryPeykCustomers.Helpers;

namespace BatteryPeykCars.Pages.Admin.Cars
{
    [Authorize]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Car Car { get; set; }
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int customerId)
        {
            Car = new Car { CustomerId = customerId };
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                Car.PurchaseDate = DateTime.Today;
                Car.ReplaceDate = DateTime.Today.AddMonths(Car.LifeExpectancy);

                _context.Car.Add(Car);
                await _context.SaveChangesAsync();

                TempData["success"] = "Created Successfully";


                var customer = await _context.Customer.FindAsync(Car.CustomerId);
                if (customer != null)
                {
                    SmsHelper smsHelper = new SmsHelper(customer.Name, customer.Phone);
                    var respone = await smsHelper.SendSms(MessageType.Update);
                    if (!respone.IsSuccess)
                    {
                        TempData["error"] = respone.Message;
                    }
                }

                return RedirectToPage("Index", new { customerId = Car.CustomerId });
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return Page();
            }
        }
    }
}
