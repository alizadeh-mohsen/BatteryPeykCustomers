using Microsoft.AspNetCore.Authorization;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Profits
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
            return Page();
        }

        [BindProperty]
        public Profit Profit { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Profit.Add(Profit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
