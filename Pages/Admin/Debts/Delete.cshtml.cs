using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Debts
{
    [Authorize] public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Debt Debt { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var debt = await _context.Debt
                //.Include(d=>d.Reason)
                //.Include(d=>d.Counterparty)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (debt == null)
            {
                return NotFound();
            }
            else
            {
                Debt = debt;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var debt = await _context.Debt.FindAsync(id);
            if (debt != null)
            {

                Debt = debt;
                _context.Debt.Remove(Debt);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
