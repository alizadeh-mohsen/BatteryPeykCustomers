using Microsoft.AspNetCore.Authorization;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Profits
{
    [Authorize] public class DeleteModel : PageModel
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
