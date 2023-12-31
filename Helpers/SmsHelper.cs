using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Text;

namespace BatteryPeykCustomers.Helpers
{
    public class SmsHelper
    {

        private const string APIKEY = "1wvNTyffrYh0A51Vno4BGatY0xfCWhZbrFiFrT6ZTmZsVQurKmiyrYxX6E6bU9YEO";
        private const string lineNumber = "30007732907663";
        private string _name;
        private string _phone;
        private string _link;
        private const string defaultPath = "https://BatteryPeyk.com/vip.html?m=";
        HttpClient httpClient;


        public SmsHelper(string name, string phone)
        {
            _name = name;
            _link = defaultPath + phone;
            _phone = phone;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-api-key", APIKEY);
        }

        public async Task<Response> SendSms(MessageType messageType)
        {
            if (_phone == null || !_phone.StartsWith("0") || _phone.Length != 11)
                return new Response
                {
                    Message="شماره موبایل صحیح نیست"
                };

            string message = GetMessage(messageType);
            return await Send(message);
        }

        private string GetMessage(MessageType messageType)
        {
            var message = string.Empty;

            switch (messageType)
            {
                case MessageType.Welcome:
                    message =
                        "عضویت شما در باشگاه مشتریان باتری پیک با موفقیت انجام شد. برای مشاهده مشخصات باتری خریداری شده به لینک زیر مراجعه کنید";
                    break;
                case MessageType.Update:
                    message =
                    "عضو باشگاه مشتریان باتری پیک. اطلاعات شما بروزرسانی شد. برای مشاهده این اطلاعات به لینک زیر مراجعه کنید" ;
                    break;
                case MessageType.Expire:
                    message =
                "عضو باشگاه مشتریان باتری پیک. عمر مفید باتری شما به پایان رسیده. ظرف یک ماه آینده نسبت به تعویض آن اقدام کنید." ;
                    break;
                case MessageType.NewCar:
                    message =
                 "عضو باشگاه مشتریان باتری پیک. خودرو جدیدی به پنل شما اشافه شد. برای مشاهده اطلاعات جدید به آدرس زیر مراجعه کنید.";
                    break;
                default:
                    return string.Empty;
            }

            return $" {_name} عزیز" + "\n" +
                message + "\n" +
                $"{_link}";
        }

        private string GetPayload(string message)
        {
            Request request = new Request
            {

                LineNumber = lineNumber,
                MessageText = message,
                Mobiles = new string[] { _phone }

            };

            var payload = JsonConvert.SerializeObject(request);

            return payload;
        }

        private async Task<Response> Send(string message)
        {
            HttpContent content = new StringContent(GetPayload(message), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://api.sms.ir/v1/send/bulk", content);
            var result = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<Response>(result);
            return resp;
        }
    }
}


