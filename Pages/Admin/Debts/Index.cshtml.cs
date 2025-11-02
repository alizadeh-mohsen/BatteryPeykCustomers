using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Debts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty(SupportsGet = true)]
        public long TotalDebit { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchCommentString { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Debt> Debts { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var query = _context.Debt.OrderBy(C => C.Id).AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchCommentString))
                query = query.Where(c => c.Description.Contains(SearchCommentString));

            Debts = await query.ToListAsync();

            TotalDebit = Debts.Sum(x => x.Amount);
        }
    }
}
