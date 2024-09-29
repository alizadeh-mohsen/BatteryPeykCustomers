using Microsoft.AspNetCore.Authorization;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Guaranties
{
    [Authorize] public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Guarranty> Guarranty { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Guarranty = await _context.Guarranty
                .Include(g => g.Amper)
                .Include(g => g.Company).OrderBy(c => c.Id).ToListAsync();
        }
    }
}
