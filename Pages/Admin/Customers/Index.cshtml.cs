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

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    public class IndexModel : PageModel
    {
        private readonly BatteryPeykCustomers.Data.ApplicationDbContext _context;

        public IndexModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchNameString { get; set; } 
        
        [BindProperty(SupportsGet = true)]
        public string? SearchPhoneString { get; set; }

        public IList<Customer> Customers { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Customer != null)
            {
                var result = from c in _context.Customer select c;
                if (!string.IsNullOrEmpty(SearchNameString))
                {
                    result = result.Where(c => c.Name.ToLower().Contains(SearchNameString.ToLower().Trim()));
                }
                if (!string.IsNullOrEmpty(SearchPhoneString))
                {
                    result = result.Where(c => c.Phone.Contains(SearchPhoneString.Trim()));
                }
                Customers = await result.OrderByDescending(c=>c.PurchaseDate).ToListAsync();
            }
        }

        public string ToPersianDate(DateTime? date)
        {
            return DateHelper.ToPersianDate(date);
        }
    }
}
