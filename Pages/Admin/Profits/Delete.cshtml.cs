using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Profits
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Profit Profit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profit = await _context.Profit.FirstOrDefaultAsync(m => m.Id == id);

            if (profit == null)
            {
                return NotFound();
            }
            else
            {
                Profit = profit;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profit = await _context.Profit.FindAsync(id);
            if (profit != null)
            {
                Profit = profit;
                _context.Profit.Remove(Profit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
