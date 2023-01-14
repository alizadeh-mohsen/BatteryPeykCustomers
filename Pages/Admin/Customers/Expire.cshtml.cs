using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using BatteryPeykCustomers.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    [Authorize]
    public class ExpireModel : PageModel
    {
        private readonly BatteryPeykCustomers.Data.ApplicationDbContext _context;

        public ExpireModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Customer != null)
            {
                Customer = await _context.Customer.Where(c => c.ReplaceDate.AddDays(-60) <= DateTime.Today && !c.StopNotify).ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null) { return NotFound(); }
            customer.StopNotify = true;

            await _context.SaveChangesAsync();

            TempData["success"] = "Updated Successfully";
            return RedirectToPage("./Expire");
        }

        public string ToPersianDate(DateTime? date)
        {
            return DateHelper.ToPersianDate(date);
        }
    }
}
