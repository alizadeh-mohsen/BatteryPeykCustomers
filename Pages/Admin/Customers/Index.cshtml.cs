using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerService _customerService;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public IndexModel(ApplicationDbContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchNameString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchPhoneString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchCommentString { get; set; }

        public IList<Customer> Customers { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var query = _context.Customer.OrderByDescending(c => c.Id).AsQueryable();
            if (!string.IsNullOrEmpty(SearchNameString))
                query = query.Where(c => c.Name.Contains(SearchNameString));
            if (!string.IsNullOrEmpty(SearchPhoneString))
                query = query.Where(c => c.Phone.Contains(SearchPhoneString));
            if (!string.IsNullOrEmpty(SearchCommentString))
                query = query.Include(c => c.Cars).Where(c => c.Cars.Any(c => c.Comments.Contains(SearchCommentString)));

            Customers = await _customerService.GetPaginatedResult(query, CurrentPage, PageSize);
            Count = await _customerService.GetCount(query);
        }

    }
}
