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
        public IList<UsedReportGrouped> UsedReportGrouped { get; set; } = default!;
        public long TotalAmper { get; set; }
        public int Count { get; set; }

        private readonly ApplicationDbContext _context;

        public UsedModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string sortOrder = "date_asc", string groupOrder = "g_amper_asc")
        {

            var query = _context.UsedHistory.Include(c => c.Customer) as IQueryable<UsedHistory>;

            ViewData["AmperSort"] = sortOrder == "amper_asc" ? "amper_desc" : "amper_asc";
            ViewData["BrandSort"] = sortOrder == "barnd_asc" ? "brand_desc" : "brand_asc";

            switch (sortOrder)
            {
                case "amper_desc":
                    query = query.OrderByDescending(c => c.Amper);
                    break;
                case "amper_asc":
                    query = query.OrderBy(c => c.Amper);
                    break;
                case "brand_desc":
                    query = query.OrderByDescending(c => c.Brand);
                    break;
                case "brand_asc":
                    query = query.OrderBy(c => c.Brand);
                    break;
                default:
                    query = query.OrderByDescending(c => c.Date);
                    break;
            }
            UsedHistories = await query.ToListAsync();

            Count = UsedHistories.Count;
            TotalAmper = UsedHistories.Sum(b => b.Amper);

            IQueryable<UsedReportGrouped> queryGroup = _context.UsedHistory.GroupBy(u => new { u.Amper, u.Brand })
        .Select(g => new UsedReportGrouped
        {
            Amper = g.Key.Amper,
            Brand = g.Key.Brand,
            Count = g.Count()
        });

            ViewData["BrandGroupSort"] = groupOrder == "g_amper_asc" ? "g_amper_desc" : "g_amper_asc";
            ViewData["AmperGroupSort"] = groupOrder == "g_barnd_asc" ? "g_barnd_desc" : "g_barnd_asc";


            switch (groupOrder)
            {
                case "g_amper_desc":
                    queryGroup = queryGroup.OrderByDescending(c => c.Amper);
                    break;
                case "g_amper_asc":
                    queryGroup = queryGroup.OrderBy(c => c.Amper);
                    break;
                case "g_barnd_desc":
                    queryGroup = queryGroup.OrderByDescending(c => c.Brand);
                    break;
                case "g_barnd_asc":
                    queryGroup = queryGroup.OrderBy(c => c.Brand);
                    break;
                default:
                    queryGroup = queryGroup.OrderByDescending(c => c.Amper);
                    break;
            }


            UsedReportGrouped = await queryGroup.ToListAsync();

            return Page();

        }
    }

    public class UsedReportGrouped
    {
        public int Amper { get; set; }
        public string Brand { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
