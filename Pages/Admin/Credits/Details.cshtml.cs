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
    public class DetailsModel : PageModel
    {
        private readonly BatteryPeykCustomers.Data.ApplicationDbContext _context;

        public DetailsModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
