using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Useds
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public CreateModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Used Used { get; set; } = default!;

        [BindProperty]
        public string UsedBrand { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var existingUsed = await _context.Used.FirstOrDefaultAsync();
            if (existingUsed == null)
            {
                return NotFound();
            }

            existingUsed.Amperage += Used.Amperage;
            existingUsed.Quantity += Used.Quantity;

            var usedHistory = new UsedHistory
            {
                Amper = Used.Amperage,
                Brand = UsedBrand + " - دستی",
                CustomerId = 435,
                Date = DateTime.UtcNow
            };

            _context.UsedHistory.Add(usedHistory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
