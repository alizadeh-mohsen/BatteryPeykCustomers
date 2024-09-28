using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Helpers;
using BatteryPeykCustomers.Model.ViewModel;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace BatteryPeykCustomers.Pages.Admin.Report
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public LossAndProfitViewModel vm { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public DateTime? From { get; set; } = DateTime.Today;

        [BindProperty(SupportsGet = true)]
        public DateTime? To { get; set; } = DateTime.Today;

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            vm = new LossAndProfitViewModel();

            _context = context;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (From == null || To == null)
                return Page();

            //var from = GetGregorianDate(From.Value);
            //var to = GetGregorianDate(To.Value);
            TimeSpan ts = new TimeSpan(11, 59, 59);
            To = To.Value.Date + ts;

            var batteriesSold = await _context.Car
                .Where(c => c.PurchaseDate >= From && c.PurchaseDate <= To)
                .CountAsync();

            var profit = await _context.Profit
                .Where(c => c.Date >= From && c.Date <= To).ToListAsync();

            var profitSum = 0;
            foreach (var itemP in profit)
                profitSum += itemP.Amount;

            var loss = await _context.Expense
                .Where(c => c.Date >= From && c.Date <= To).ToListAsync();
            var lossSum = 0;
            foreach (var itemL in loss)
                lossSum += itemL.Amount;
            vm = new LossAndProfitViewModel
            {

                TotalLoss = lossSum,
                TotalProfit = profitSum,
                TotalBatterySold = batteriesSold,
                Profit = profitSum - lossSum
            };

            return Page();

        }
        private DateTime GetGregorianDate(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return new DateTime(date.Year, date.Month, date.Day, pc);
        }

        //public async Task<IActionResult> OnGetReportAsync(string from, string to)
        //{

        //    return await OnGet();
        //    //var battery = await _context.Battery
        //    //    .FirstOrDefaultAsync(c => c.CompanyId == companyId && c.AmperId == amperId);
        //    //return new JsonResult(battery);
        //}
    }
}
