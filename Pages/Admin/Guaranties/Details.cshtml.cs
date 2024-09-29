using Microsoft.AspNetCore.Authorization;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Guaranties
{
    [Authorize] public class DetailsModel : PageModel
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
