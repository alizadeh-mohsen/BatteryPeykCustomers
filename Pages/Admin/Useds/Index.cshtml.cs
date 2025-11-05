using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Useds
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Used Used { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Used = await _context.Used.FirstOrDefaultAsync();
        }

        // Handler invoked when the reset button is clicked.
        public async Task<IActionResult> OnPostResetAsync(int id)
        {
            var used = await _context.Used.FindAsync(id);
            if (used == null)
            {
                return NotFound();
            }

            used.Quantity = 0;
            used.Amperage = 0;

            _context.Update(used);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
