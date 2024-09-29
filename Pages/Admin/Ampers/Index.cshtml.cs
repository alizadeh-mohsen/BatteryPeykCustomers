using Microsoft.AspNetCore.Mvc.RazorPages;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Ampers
{
        [Authorize] public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Amper> Amper { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Amper = await _context.Amper.OrderBy(c=>c.Amperage).ToListAsync();
        }
    }
}
