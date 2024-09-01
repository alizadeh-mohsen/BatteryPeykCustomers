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
        private readonly IConfiguration _configuration;

        [BindProperty]
        public CustomerViewModel vm { get; set; } = default!;


        public CreateModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet()
        {
            var companies = await _context.Company.OrderBy(c => c.Title).ToListAsync();
            var vehicles = await _context.Vehicle.OrderBy(c => c.Make).ToListAsync();
            var ampers = await _context.Amper.OrderBy(c => c.Amperage).ToListAsync();
            ViewData["Companies"] = new SelectList(companies, "Id", "Title");
            ViewData["Vehicles"] = new SelectList(vehicles, "Id", "Make");
            ViewData["Ampers"] = new SelectList(ampers, "Id", "Title");
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
                    ModelState.AddModelError("Customer.Phone", "شماره موبایل باید با 0 شروع شود");
                    return Page();
                }

                var existingCustomer = _context.Customer.FirstOrDefault(x => x.Phone == vm.Phone);
                if (existingCustomer != null)
                {
                    ModelState.AddModelError("Customer.Phone", "شماره موبایل قبلا ثبت شده. مشتری موجود است. برای این مشتری ماشین جدید ثبت کنید");
                    return Page();
                }

                var selectedCompnay = await _context.Company.FindAsync(vm.CompanyId);
                var selectedVehicle = await _context.Vehicle.FindAsync(vm.VehicleId);
                var selectedAmper = await _context.Amper.FindAsync(vm.AmperId);

                var Cars = new List<Car> { new()
                {
                    Battery = selectedCompnay?.Title + " " + selectedAmper?.Title,
                    Guaranty = vm.Guaranty,
                    Make = selectedVehicle?.Make,
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
                string stockEnabled = _configuration["StockEnabled"];
                if (!string.IsNullOrWhiteSpace(stockEnabled) && bool.Parse(stockEnabled) == true)
                {
                    var battery = await _context.Battery.FirstOrDefaultAsync(x => x.AmperId == vm.AmperId && x.CompanyId == vm.CompanyId);
                    if (battery != null)
                        battery.Quantity -= 1;

                    var message = $"موجودی انبار " +
                           $"{battery.Company.Title} {battery.Amper.Title} : {battery.Quantity}" +
                           $" عدد";

                    if (battery?.Quantity <= battery?.AlertQuantity)
                        TempData["error"] = message;
                    else
                        TempData["success"] = message;
                }

                _context.Customer.Add(customer);
                await _context.SaveChangesAsync();

                var count = _context.Customer.Count();
                if (count % 100 == 0)
                    TempData["success"] = $" ای ول به ولت وولی به وولت زنبور نزنه یه وری به دولت. سلطان مشتری هات شد " +
                        $"{count} " +
                        $"تا.";
                else
                    TempData["success"] = "با موفقیت ثبت شد";



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

        public async Task<JsonResult> OnGetCompanyAsync(int companyId)
        {
            var battery = await _context.Company.FirstOrDefaultAsync(c => c.Id == companyId);
            return new JsonResult(battery);
        }
    }
}
