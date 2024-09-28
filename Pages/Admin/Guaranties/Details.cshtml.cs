using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Guaranties
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Guarranty Guarranty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guarranty = await _context.Guarranty.FirstOrDefaultAsync(m => m.Id == id);
            if (guarranty == null)
            {
                return NotFound();
            }
            else
            {
                Guarranty = guarranty;
            }
            return Page();
        }
    }
}
