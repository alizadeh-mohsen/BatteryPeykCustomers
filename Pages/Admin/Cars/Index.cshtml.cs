using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Cars
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public CarAndCustomerViewModel vm { get; set; } = default!;

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;

        }

        [BindProperty]
        public int CustomerId { get; set; }

        public async Task<IActionResult> OnGetAsync(int customerId)
        {
            CustomerId = customerId;
            var carsQuery = _context.Car.Where(c => c.CustomerId == customerId).OrderByDescending(c => c.PurchaseDate) as IQueryable<Model.Car>;

            vm = new CarAndCustomerViewModel
            {
                Cars = await carsQuery.ToListAsync(),
                Customer = await _context.Customer.FindAsync(customerId)
            };
            return Page();
        }

        public IActionResult OnPost()
        {

            return RedirectToPage("/Admin/Customers/Create", new { CustomerId });


        }

    }
}
