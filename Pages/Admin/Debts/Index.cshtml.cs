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

        public async Task OnGetAsync(string sortOrder = "date_asc")
        {
            var query = _context.Debt.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchCommentString))
                query = query.Where(c => c.Description.Contains(SearchCommentString));

            ViewData["DateSort"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["AmountSort"] = sortOrder == "amount_asc" ? "amount_desc" : "amount_asc";

            switch (sortOrder)
            {
                case "date_desc":
                    query = query.OrderByDescending(c => c.Date);
                    break;
                case "date_asc":
                    query = query.OrderBy(c => c.Date);
                    break;
                case "amount_desc":
                    query = query.OrderByDescending(c => c.Amount);
                    break;
                case "amount_asc":
                    query = query.OrderBy(c => c.Amount);
                    break;
                default:
                    query = query.OrderBy(c => c.Date);
                    break;
            }


            Debts = await query.ToListAsync();

            TotalDebit = Debts.Sum(x => x.Amount);
        }
    }
}
