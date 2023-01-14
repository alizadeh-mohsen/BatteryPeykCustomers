using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Model;
using BatteryPeykCustomers.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly BatteryPeykCustomers.Data.ApplicationDbContext _context;

        public DetailsModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Customer Customer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;
            }
            return Page();
        }

        public string ToPersianDate(DateTime? date)
        {
            return DateHelper.ToPersianDate(date);
        }
        public string BatteryAge(DateTime purchaseDate)
        {
            return DateTime.Today.Subtract(purchaseDate).TotalDays + " Days" ;
        }
    }
}
