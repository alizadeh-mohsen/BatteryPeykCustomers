using Newtonsoft.Json;
using System.Text;

namespace BatteryPeykCustomers.Helpers
{
    public class SmsHelper
    {

        private const string APIKEY = "wvNTyffrYh0A51Vno4BGatY0xfCWhZbrFiFrT6ZTmZsVQurKmiyrYxX6E6bU9YEO";
        private const string lineNumber = "30007732907663";
        private const string vipPanel = "https://BatteryPeyk.com/vip.html?m=";
        private const string SendUrl = "https://api.sms.ir/v1/send/verify";
        private const string ReportUrl = "https://api.sms.ir/v1/send/";
        private const int WelcomeMessageTemplateId = 538705;
        private const int UpdateMessageTemplateId = 620937;
        private Dictionary<int, string> DeliveryStatusCode = new Dictionary<int, string>
        {
            {0  , "مشکلی در سامانه رخ داده است، لطفا با پشتیبانی در تماس باشید" },
            {2  ,"نرسیده به گوشی" },
            {3  ,"پردازش در مخابرات" },
            {4  ,"نرسیده به مخابرات" },
            {5  ,"رسیده به اپراتور" },
            {6  ,"ناموفق" },
            {7  ,"لیست سیاه" },
            {10 , "کلید وب سرویس نامعتبر است" },
            {11 , "کلید وب سرویس غیرفعال است" },
            {12 , "کلید وب‌ سرویس محدود به IP‌های تعریف شده می‌باشد" },
            {13 , "حساب کاربری غیر فعال است" },
            {14 , "حساب کاربری در حالت تعلیق قرار دارد" },
            {20 , "تعداد درخواست بیشتر از حد مجاز است" },
            {101, "شماره خط نامعتبر می‌باشد" },
            {102, "اعتبار کافی نمی‌باشد" },
            {103, "درخواست شما دارای متن(های) خالی است" },
            {104, "درخواست شما دارای موبایل(های) نادرست است" },
            {105, "تعداد موبایل ها بیشتر از حد مجاز(100 عدد) می‌باشد" },
            {106, "تعداد متن ها بیشتر از حد مجاز(100 عدد) می‌باشد" },
            {107, "لیست موبایل ها خالی می‌باشد" },
            {108, "لیست متن ها خالی می‌باشد" },
            {109, "زمان ارسال نامعتبر می‌باشد" },
            {110, "تعداد شماره موبایل‌ها و تعداد متن ها برابر نیستند" },
            {111, "با این شناسه ارسالی ثبت نشده است" },
            {112, "رکوردی برای حذف یافت نشد" },
            {113, "قالب یافت نشد" },
            {114, "طول رشته مقدار پارامتر، بیش از حد مجاز(25 کاراکتر) می‌باشد" },
            {115, "شماره موبایل(ها) در لیست سیاه سامانه می‌باشند" },
            {116, "نام پارامتر نمی‌تواند خالی باشد" },
            {117, "متن ارسال شده مورد تایید نمی‌باشد" },
            {118, "تعداد پیام ها بیش از حد مجاز می باشد." },

            };

        private string _name;
        private string _phone;
        private readonly IConfiguration _configuration;
        private string _link;
        HttpClient httpClient;

        public SmsHelper(string name, string phone, IConfiguration configuration)
        {
            _name = name;
            _link = vipPanel + phone;
            _phone = phone;
            _configuration = configuration;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-api-key", APIKEY);
        }

        public async Task<Response> SendSms(MessageType messageType)
        {
            try
            {
                //string smsEnabled = _configuration["SmsEnabled"];
                //if (!string.IsNullOrWhiteSpace(smsEnabled) && bool.Parse(smsEnabled) == true)
                //{

                int templateId = GetTemplateId(messageType);
                var sendresponse = await Send(templateId);
                if (sendresponse.Status != 1)
                    return new Response
                    {
                        IsSuccess = false,
                        Message = sendresponse.Message
                    };
                //return GetErrorMessage(sendresponse, true);

                //var reportresponse = await GetReport(sendresponse.Data.messageId);
                //if (reportresponse.Data.DeliveryState != 1)
                //    //return GetErrorMessage(reportresponse, false);
                //    return new Response
                //    {
                //        IsSuccess = false,
                //        Message = "اختلال در ارسال پیامک",
                //    };

                //}

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = "خطا در ارسال پیامک",
                };
            }
        }

        private int GetTemplateId(MessageType messageType)
        {

            switch (messageType)
            {
                case MessageType.Welcome:
                    return WelcomeMessageTemplateId;
                case MessageType.Update:
                    return UpdateMessageTemplateId;
                default:
                    return -1;
            }

        }

        private async Task<Response> Send(int templateId)
        {

            HttpContent content = new StringContent(GetPayload(templateId), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(SendUrl, content);
            return await GetResponse(response);

        }

        private async Task<Response> GetReport(int messageId)
        {
            var response = await httpClient.GetAsync(ReportUrl + messageId);
            var result = await GetResponse(response);
            return result;
        }

        private string GetPayload(int templateId)
        {
            Request request = new Request
            {

                Mobile = _phone,
                TemplateId = templateId,
                Parameters = new Parameter[] {
                    new Parameter { Name = "CUSTOMER", Value = _name },
                    new Parameter { Name = "DATE", Value = _link.Substring(0,25) },
                    new Parameter { Name = "PHONE", Value = _link.Substring(25) },
                }
            };

            var payload = JsonConvert.SerializeObject(request);

            return payload;
        }

        private async Task<Response> GetResponse(HttpResponseMessage httpResponseMessage)
        {
            var result = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Response>(result);
            return response;
        }

        private Response GetErrorMessage(Response response, bool isSend)
        {

            response.IsSuccess = false;
            response.Message = isSend ? DeliveryStatusCode[response.Status] :
            response.Message = DeliveryStatusCode[response.Data.DeliveryState];

            return response;
        }
    }
}


