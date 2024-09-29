using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Credits
{
    [Authorize] public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int TotalCredit { get; set; }
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Credit> Credit { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Credit = await _context.Credit.ToListAsync();
            var sum = 0;
            foreach (var credit in Credit)
            {
                sum += credit.Amount;
            }
            TotalCredit = sum;
        }
    }
}
