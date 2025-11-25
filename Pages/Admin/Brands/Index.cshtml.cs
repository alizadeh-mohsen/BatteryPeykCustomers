using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Brands
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Company> Company { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var CompanyQuery = _context.Company.OrderBy(c => c.Title) as IQueryable<Company>;
            Company = await CompanyQuery.ToListAsync();
        }
    }
}
