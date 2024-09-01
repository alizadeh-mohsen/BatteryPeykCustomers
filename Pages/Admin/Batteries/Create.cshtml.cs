
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Batteries
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
        ViewData["AmperId"] = new SelectList(_context.Amper.OrderBy(c => c.Amperage), "Id", "Title");
        ViewData["CompanyId"] = new SelectList(_context.Company.OrderBy(c => c.Title), "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Battery Battery { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Battery.Add(Battery);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
