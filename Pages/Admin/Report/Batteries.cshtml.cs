using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Report
{
    public class BatteriesModel : PageModel
    {
        public IList<Battery> Batteries { get; set; } = default!;
        public int Count { get; set; }

        private readonly ApplicationDbContext _context;

        public BatteriesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var batteriesQuery = _context.Battery
                 .Include(b => b.Company)
                 .Include(b => b.Amper)
                 .Where(b => b.Quantity > 0)
                 .Select(g => new Battery
                 {
                     Company = g.Company,
                     Amper = g.Amper,
                     Quantity = g.Quantity
                 })
                 .OrderBy(b => b.Company!.Title)
                 .ThenBy(b => b.Amper.Title) as IQueryable<Battery>;
            Batteries = await batteriesQuery.ToListAsync();
            Count = Batteries.Sum(b => b.Quantity);

            return Page();
        }
    }
}
