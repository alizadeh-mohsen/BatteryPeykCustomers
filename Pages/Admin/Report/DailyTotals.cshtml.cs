using BatteryPeykCustomers.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Report
{
    public class DailyTotalsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DailyTotalsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<DailyTotalDto> DailyTotals { get; set; } = new();
        public int SumTotals { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? From { get; set; } = DateTime.Today;

        [BindProperty(SupportsGet = true)]
        public DateTime? To { get; set; } = DateTime.Today;

        public async Task<IActionResult> OnGetAsync()
        {
            if (From == null || To == null)
            {
                DailyTotals = new List<DailyTotalDto>();
                return Page();
            }

            // ensure To includes the whole day
            var toInclusive = To.Value.Date.AddDays(1).AddTicks(-1);

            // Group by date portion of PurchaseDate and count
            DailyTotals = await _context.Car
                .Where(c => c.PurchaseDate >= From.Value.Date && c.PurchaseDate <= toInclusive)
                .GroupBy(c => c.PurchaseDate.Date)
                .Select(g => new DailyTotalDto
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(d => d.Date)
                .ToListAsync();
            SumTotals = DailyTotals.Sum(d => d.Count);
            return Page();
        }
    }

    public class DailyTotalDto
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}