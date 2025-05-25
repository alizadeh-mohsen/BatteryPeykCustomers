using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;

namespace BatteryPeykCustomers.Pages.Admin.Debts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty(SupportsGet = true)]
        public long TotalDebit { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Debt> Debt { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Debt = await _context.Debt.OrderBy(C => C.Id)
                //.Include(d => d.Counterparty)
                //.Include(d => d.Reason).OrderBy(c => c.Counterparty.Title)
                .ToListAsync();

            TotalDebit = Debt.Sum(x => x.Amount);
        }
    }
}
