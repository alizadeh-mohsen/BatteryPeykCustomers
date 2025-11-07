using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Report
{
    public class SalesModel : PageModel
    {
        public IList<Car> Cars { get; set; } = default!;
        public long TotalBatteries { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? From { get; set; } = DateTime.Today;

        [BindProperty(SupportsGet = true)]
        public DateTime? To { get; set; } = DateTime.Today;

        private readonly ApplicationDbContext _context;

        public SalesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            if (From == null || To == null || From == DateTime.MinValue || To == DateTime.MinValue)
                return Page();

            TimeSpan ts = new TimeSpan(11, 59, 59);
            To = To.Value.Date + ts;

            var batteriesSold = await _context.Car.Include(c => c.Customer)
                .Where(c => c.PurchaseDate >= From && c.PurchaseDate <= To)
                .ToListAsync();

            Cars = batteriesSold;
            TotalBatteries = batteriesSold.Count;

            return Page();

        }
    }
}
