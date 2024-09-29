using Microsoft.AspNetCore.Authorization;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Useds
{
    [Authorize] public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Used Used { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Used = await _context.Used.FirstOrDefaultAsync();
        }
    }
}
