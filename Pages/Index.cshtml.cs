using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BatteryPeykCustomers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BatteryPeykCustomers.Data.ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger,BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;

        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(string m)
        {
            try {
                if (m == null)
                {
                    return RedirectToPage("/NotFound");
                }

                var customer = await _context.Customer.FirstOrDefaultAsync(c => c.Phone == m);
                if (customer == null)
                {
                    return RedirectToPage("/NotFound");
                }
                else
                {
                    Customer = customer;
                }
                return Page();
            }
            catch(Exception ex)
            {
                throw;
            }
            
            }

        public string ToPersianDate(DateTime? date)
        {
            if (date == null)
                return string.Empty;
            DateTime dateResult;

            var culture = CultureInfo.CreateSpecificCulture("en-US");
            var styles = DateTimeStyles.None;
            if (DateTime.TryParse(date.ToString(), culture, styles, out dateResult))
            {
                PersianCalendar pc = new PersianCalendar();
                return string.Format("{0}/{1}/{2}", pc.GetYear(dateResult), pc.GetMonth(dateResult), pc.GetDayOfMonth(dateResult));
            }
            return string.Empty;
        }
        public int CalcLife()
        {
            return Customer.PurchaseDate == null
                ? 0
                : ((DateTime.Today.Year - Customer.PurchaseDate.Year) * 12) + DateTime.Today.Month - Customer.PurchaseDate.Month;
        }
    }
}
