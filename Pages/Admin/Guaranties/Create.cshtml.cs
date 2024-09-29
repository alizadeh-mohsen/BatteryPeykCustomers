using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Guaranties
{
    [Authorize] public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AmperId"] = new SelectList(_context.Amper, "Id", "Title");
        ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Guarranty Guarranty { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Guarranty.Add(Guarranty);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
