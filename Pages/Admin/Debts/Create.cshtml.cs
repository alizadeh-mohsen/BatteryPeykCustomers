using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Debts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var counterparties = await _context.Counterparty.OrderBy(c => c.Title).ToListAsync();
            var reasons = await _context.Reason.OrderBy(c => c.Title).ToListAsync();

            ViewData["CounterpartyId"] = new SelectList(counterparties.OrderBy(c => c.Title), "Id", "Title");
            ViewData["ReasonId"] = new SelectList(reasons, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Debt Debt { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _context.Debt.Add(Debt);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                return Page();
            }
        }
    }
}
