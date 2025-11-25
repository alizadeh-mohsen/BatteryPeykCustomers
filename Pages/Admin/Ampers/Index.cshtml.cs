using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Ampers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Amper> Amper { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var query = _context.Amper.OrderBy(c => c.Amperage) as IQueryable<Amper>;
            Amper = await query.ToListAsync();
        }
    }
}
