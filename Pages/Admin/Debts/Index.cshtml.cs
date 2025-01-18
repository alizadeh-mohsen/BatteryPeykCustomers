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
        public string? SearchCommentString { get; set; }

        [BindProperty(SupportsGet = true)]
        public long TotalCredit { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Debt> Debt { get; set; } = default!;

        public async Task OnGetAsync()
        {
            IQueryable<Debt> query = _context.Debt;
            if (!string.IsNullOrEmpty(SearchCommentString))
                query = query.Where(s => s.Description.Contains(SearchCommentString));

                
            Debt = await query.OrderBy(C => C.Id)
                  //.Include(d => d.Counterparty)
                  //.Include(d => d.Reason).OrderBy(c => c.Counterparty.Title)
                .ToListAsync();

            TotalCredit = Debt.Sum(s=>s.Amount);
        }
    }
}
