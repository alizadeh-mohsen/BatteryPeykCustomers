using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Batteries
{
    [Authorize] public class IndexModel : PageModel
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


        public async Task OnGetAsync()
        {

            AmperList = new SelectList(await _context.Amper.OrderBy(c => c.Amperage).ToListAsync(), "Id", "Title");
            CompanyList = new SelectList(await _context.Company.OrderBy(c => c.Title).ToListAsync(), "Id", "Title");
            Total = (await _context.Battery.SumAsync(b => b.Quantity)).ToString();

            IQueryable<Battery> result = _context.Battery
                            .Include(b => b.Amper)
                            .Include(b => b.Company)
                            .OrderByDescending(c => c.Quantity<= c.AlertQuantity ).ThenBy(c=>c.Quantity);

            if (!string.IsNullOrEmpty(SelectedCompany))
                result = result.Where(t => t.CompanyId == int.Parse(SelectedCompany));

            if (!string.IsNullOrEmpty(SelectedAmper))
                result = result.Where(t => t.AmperId == int.Parse(SelectedAmper));

            Battery = await result.ToListAsync();
        }
    }
}
