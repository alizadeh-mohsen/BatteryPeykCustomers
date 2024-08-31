using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using BatteryPeykCustomers.Model.ViewModel;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    [Authorize]
    public class CreateModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        [BindProperty]
        public CustomerViewModel vm { get; set; } = default!;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var companies = await _context.Company.OrderBy(c => c.Title).ToListAsync();
            var vehicles = await _context.Vehicle.OrderBy(c => c.Make).ToListAsync();
            var ampers = await _context.Amper.OrderBy(c => c.Title).ToListAsync();
            ViewData["CompanyId"] = new SelectList(companies, "Id", "Title");
            ViewData["VehicleId"] = new SelectList(vehicles, "Id", "Make");
            ViewData["AmperId"] = new SelectList(ampers, "Id", "Title");
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

                if (!vm.Phone.StartsWith("0"))
                {
                    ModelState.AddModelError("Customer.Phone", "Phone number should start with 0");
                    return Page();
                }

                var existingCustomer = _context.Customer.FirstOrDefault(x => x.Phone == vm.Phone);
                if (existingCustomer != null)
                {
                    ModelState.AddModelError("Customer.Phone", "Mobile phone exists");
                    return Page();
                }


                var Cars = new List<Car> { new()
                {
                    Battery = vm.Battery,
                    Guaranty = vm.Guaranty,
                    Make = vm.Make,
                    LifeExpectancy = vm.LifeExpectancy,
                    PurchaseDate = DateTime.Today,
                    ReplaceDate = DateTime.Today.AddMonths(vm.LifeExpectancy),
                    Comments = vm.Comments,
                } };
                Customer customer = new()
                {
                    Address = vm.Address,
                    Name = vm.Name,
                    Phone = vm.Phone,
                    Cars = Cars,
                };

                _context.Customer.Add(customer);
                await _context.SaveChangesAsync();
                var count = _context.Customer.Count();
                if (count % 100 == 0)
                {
                    TempData["success"] = " ای ول به ولت وولی به وولت زنبور نزنه یه وری به دولت. مشتریها شد" + count + " تا.";
                }
                else
                    TempData["success"] = "Created Successfully";



                SmsHelper smsHelper = new SmsHelper(vm.Name, vm.Phone);
                var respone = await smsHelper.SendSms(MessageType.Welcome);
                if (!respone.IsSuccess)
                {
                    TempData["error"] = respone.Message;
                }

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message + ex.InnerException?.Message;
                return Page();
            }
        }
    }
}
