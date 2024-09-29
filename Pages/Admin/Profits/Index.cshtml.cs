using Microsoft.AspNetCore.Authorization;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Profits
{
    [Authorize] public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public DateTime? From { get; set; } = DateTime.Today;

        [BindProperty(SupportsGet = true)]
        public DateTime? To { get; set; }= DateTime.Today;


        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Profit> Profit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()

        {
            TimeSpan ts = new TimeSpan(11, 59, 59);

            IQueryable<Profit> query = _context.Profit.OrderByDescending(c => c.Id);

            if (From != null && To != null)
            {
                To = To.Value.Date + ts;
                query = query.Where(c => c.Date >= From && c.Date <= To);
            }

            Profit = await query.ToListAsync();
            return Page();
        }
    }
}
