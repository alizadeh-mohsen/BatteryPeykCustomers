using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Helpers;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            if (_context.Customer != null)
            {
                var query = from customer in _context.Customer
                            join car in _context.Car on customer.Id equals car.CustomerId
                            where
                            (string.IsNullOrEmpty(SearchNameString) || customer.Name.ToLower().Contains(SearchNameString.ToLower().Trim()))
                            && (string.IsNullOrEmpty(SearchPhoneString) || customer.Phone.Contains(SearchPhoneString.Trim()))
                           && (string.IsNullOrEmpty(SearchCommentString) || car.Comments.Contains(SearchCommentString))
                            select customer;

                query = query.OrderByDescending(c => c.Id);

                Customers = await _customerService.GetPaginatedResult(query, CurrentPage, PageSize);
                Count = await _customerService.GetCount(query);
            }
        }

    }
}
