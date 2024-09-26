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
            vm = new CarAndCustomerViewModel
            {

                Cars = await _context.Car.Where(c => c.CustomerId == customerId).ToListAsync(),
                Customer = await _context.Customer.FirstAsync(c => c.Id == customerId)
            };
            return Page();
        }

        public IActionResult OnPost()
        {

            return RedirectToPage("/Admin/Customers/Create", new { CustomerId });


        }

    }
}
