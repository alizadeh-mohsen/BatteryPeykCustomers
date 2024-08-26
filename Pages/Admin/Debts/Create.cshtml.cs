using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Debts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CounterpartyId"] = new SelectList(_context.Counterparty.OrderBy(c => c.Title), "Id", "Title");
            ViewData["ReasonId"] = new SelectList(_context.Reason.OrderBy(c => c.Title), "Id", "Title");
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

            _context.Debt.Add(Debt);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
