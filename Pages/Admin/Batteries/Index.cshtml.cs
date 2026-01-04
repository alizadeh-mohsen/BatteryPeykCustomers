using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Batteries
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Battery> Battery { get; set; } = default;


        public SelectList CompanyList { get; set; }
        public SelectList AmperList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedCompany { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedAmper { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Total { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool HideZeroQuantity { get; set; } = true;


        public async Task OnGetAsync()
        {
            var amperQuery = _context.Amper.OrderBy(c => c.Amperage) as IQueryable<Amper>;
            var titleQuery = _context.Company.OrderBy(c => c.Title) as IQueryable<Company>;

            AmperList = new SelectList(await amperQuery.ToListAsync(), "Id", "Title");
            CompanyList = new SelectList(await titleQuery.ToListAsync(), "Id", "Title");
            Total = (await _context.Battery.SumAsync(b => b.Quantity)).ToString();

            IQueryable<Battery> result = _context.Battery
                            .Include(b => b.Amper)
                            .Include(b => b.Company)
                            .OrderBy(c => c.Quantity).ThenBy(c => c.Company.Title);

            if (!string.IsNullOrEmpty(SelectedCompany))
                result = result.Where(t => t.CompanyId == int.Parse(SelectedCompany));

            if (!string.IsNullOrEmpty(SelectedAmper))
                result = result.Where(t => t.AmperId == int.Parse(SelectedAmper));

            if (HideZeroQuantity)
                result = result.Where(t => t.Quantity > 0);

            Battery = await result.ToListAsync();
        }
    }
}
