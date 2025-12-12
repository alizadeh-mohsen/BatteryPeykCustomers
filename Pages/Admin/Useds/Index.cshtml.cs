using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using BatteryPeykCustomers.Pages.Admin.Report;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Useds
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Used? Used { get; set; } = default!;
        public List<UsedReportGrouped> Report { get; set; } = default;

        public async Task OnGetAsync()
        {
            Used = await _context.Used.FirstOrDefaultAsync();
            IQueryable<UsedReportGrouped> historyQuery = _context.UsedHistory.GroupBy(u => new { u.Amper })
            .Select(g => new UsedReportGrouped
            {
                Amper = g.Key.Amper,
                //Brand = g.Key.Brand,
                Count = g.Count()
            }).OrderBy(c => c.Amper);

            Report = await historyQuery.ToListAsync();
        }

        // Handler invoked when the reset button is clicked.
        //public async Task<IActionResult> OnPostResetAsync(int id)
        //{
        //    var used = await _context.Used.FindAsync(id);
        //    if (used == null)
        //    {
        //        return NotFound();
        //    }

        //    used.Quantity = 0;
        //    used.Amperage = 0;

        //    _context.Update(used);
        //    var usedHistories = await _context.UsedHistory.ToListAsync();
        //    _context.UsedHistory.RemoveRange(usedHistories);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage();
        //}
    }
}
