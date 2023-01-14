using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    public class CreateModel : PageModel
    {
        private readonly BatteryPeykCustomers.Data.ApplicationDbContext _context;

        public CreateModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Customer = new Customer
            {
                PurchaseDate = DateTime.Today

            };
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid || _context.Customer == null || Customer == null)
                {
                    return Page();
                }

                if (!Customer.Phone.StartsWith("0"))
                {
                    ModelState.AddModelError("Customer.Phone", "Phone number should start with 0");
                    return Page();
                }

                var existingCustomer = _context.Customer.FirstOrDefault(x => x.Phone == Customer.Phone);
                if (existingCustomer != null)
                {
                    ModelState.AddModelError("Customer.Phone", "Mobile phone exists");
                    return Page();
                }

                Customer.PurchaseDate = DateTime.Today;
                Customer.ReplaceDate = DateTime.Today.AddMonths(Customer.LifeExpectancy);
                _context.Customer.Add(Customer);
                await _context.SaveChangesAsync();
                var count = _context.Customer.Count();
                if (count % 100 == 0)
                {
                    TempData["success"] = " دمت گرم سلطان مشتریات شد" + count + " تا.";
                }
                else
                    TempData["success"] = "Created Successfully";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
