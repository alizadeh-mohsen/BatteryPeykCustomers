using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Credits
{
    public class IndexModel : PageModel
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
            TotalCredit = Credit.Sum(credit => credit.Amount);
        }
    }
}
