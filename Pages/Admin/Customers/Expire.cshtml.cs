using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
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
                Customer = await _context.Customer.Where(c => (c.ReplaceDate - DateTime.Today).TotalDays < 60).ToListAsync();
            }
        }
    }
}
