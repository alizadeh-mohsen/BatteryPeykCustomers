using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Report
{
    public class UsedModel : PageModel
    {
        public IList<UsedHistory> UsedHistories { get; set; } = default!;
        public long TotalAmper { get; set; }
        public int Count { get; set; }

        private readonly ApplicationDbContext _context;

        public UsedModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            UsedHistories = await _context.UsedHistory.Include(c => c.Customer)
                .ToListAsync();

            Count = UsedHistories.Count;
            TotalAmper = UsedHistories.Sum(b => b.Amper);

            return Page();

        }
    }
}
