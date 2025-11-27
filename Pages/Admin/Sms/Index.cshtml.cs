using BatteryPeykCustomers.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BatteryPeykCustomers.Pages.Admin.Sms
{
    public class IndexModel : PageModel
    {

        private readonly IConfiguration _configuration;
        [BindProperty]
        public string Message { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSendWelcomeMessage()
        {

            Message = await SendMessage(MessageType.Welcome);
            return Page();

        }

        public async Task<IActionResult> OnPostSendUpdateMessage()
        {
            Message = await SendMessage(MessageType.Update);
            return Page();
        }


        private async Task<string> SendMessage(MessageType messageType)
        {
            try
            {
                SmsHelper smsHelper = new SmsHelper("علیرضا رنجبر", "09102016396", _configuration);

                var respone = await smsHelper.SendSms(messageType);

                return respone.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }

}
