
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Batteries
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
            var amperQuery = _context.Amper.OrderBy(c => c.Amperage) as IQueryable<Amper>;
            var titleQuery = _context.Company.OrderBy(c => c.Title) as IQueryable<Company>;


            ViewData["AmperId"] = new SelectList(await amperQuery.ToListAsync(), "Id", "Title");
            ViewData["CompanyId"] = new SelectList(await titleQuery.ToListAsync(), "Id", "Title");
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
