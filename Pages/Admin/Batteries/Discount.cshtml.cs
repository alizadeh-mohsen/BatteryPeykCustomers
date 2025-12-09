
using BatteryPeykCustomers.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Batteries
{
    public class DiscountModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DiscountModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        [BindProperty]
        public double? Discount { get; set; }

        public async Task<IActionResult> OnPostRemoveDiscount()
        {
            var batteries = await _context.Battery.ToListAsync();

            foreach (var battery in batteries)
            {
                battery.Profit = Convert.ToInt32(battery.Profit / (1 - Discount / 100));
            }

            await _context.SaveChangesAsync();
            TempData["success"] = "تخفیف اعمال شد";

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostApplyDiscount()
        {

            var batteries = await _context.Battery.ToListAsync();

            foreach (var battery in batteries)
            {
                battery.Profit = Convert.ToInt32(battery.Profit * (1 - Discount / 100));
            }

            await _context.SaveChangesAsync();
            TempData["success"] = "تخفیف اعمال شد";

            return RedirectToPage("./Index");

        }
    }
}
