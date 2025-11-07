using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Batteries
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Battery Battery { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToPage("./Index");

            if (id == null)
            {
                return NotFound();
            }

            var battery = await _context.Battery.Include(b => b.Company)
                .Include(b => b.Amper).FirstOrDefaultAsync(m => m.Id == id);
            if (battery == null)
            {
                return NotFound();
            }
            else
            {
                Battery = battery;
            }
            return Page();
        }
    }
}
