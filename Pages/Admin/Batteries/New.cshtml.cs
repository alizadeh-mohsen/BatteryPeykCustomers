using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Batteries
{
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NewModel(ApplicationDbContext context)
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }


                var dbBattery = _context.Battery.FirstOrDefault(b => b.AmperId == Battery.AmperId && b.CompanyId == Battery.CompanyId);

                dbBattery.Quantity += Battery.Quantity;
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
