using BatteryPeykCustomers.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BatteryPeykCustomers.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        //private readonly BatteryPeykCustomers.Data.ApplicationDbContext _context;

        public IndexModel()
        {

        }



        //public Customer Customer { get; set; }

        //public async Task<IActionResult> OnGetAsync(string m)
        //{
        //    if (m == null)
        //    {
        //        return RedirectToPage("/NotFound");
        //    }

        //    var customer = await _context.Customer.FirstOrDefaultAsync(c => c.Phone == m);
        //    if (customer == null)
        //    {
        //        return RedirectToPage("/NotFound");
        //    }
        //    else
        //    {
        //        Customer = customer;
        //    }
        //    return Page();

        //}

        //public string ToPersianDate(DateTime? date)
        //{
        //    return DateHelper.ToPersianDate(date);
        //}
        //public int CalcLife()
        //{
        //    return DateHelper.CalcLife(Customer.PurchaseDate);
        //}
        //public string CalcGuarantyExpireDate()
        //{
        //    return DateHelper.CalcGuarantyExpireDate(Customer.PurchaseDate, Customer.Guaranty);


        //}

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                //SmsHelper smsHelper = new SmsHelper("منگوله گوش", "09303003843");

                //var respone = await smsHelper.SendSms(MessageType.Welcome);
                //if (!respone.IsSuccess)
                //{
                //    TempData["error"] = respone.Message;
                //}

                //var result3 = smsHelper.GetCredit();

                return Page();


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
