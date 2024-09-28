using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Credits
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Credit Credit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credit = await _context.Credit.FirstOrDefaultAsync(m => m.Id == id);

            if (credit == null)
            {
                return NotFound();
            }
            else
            {
                Credit = credit;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credit = await _context.Credit.FindAsync(id);
            if (credit != null)
            {
                Credit = credit;
                _context.Credit.Remove(Credit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
