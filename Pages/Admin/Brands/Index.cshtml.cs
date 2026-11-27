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

        public IList<Brand> Brand { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var BrandQuery = _context.Brand.OrderBy(c => c.Title) as IQueryable<Brand>;
            Brand = await BrandQuery.ToListAsync();
        }
    }
}
