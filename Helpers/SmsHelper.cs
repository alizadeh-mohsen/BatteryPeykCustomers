using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace BatteryPeykCustomers.Helpers
{
    public class SmsHelper
    {


        public MessageResource SendSms()
        {
            var accountSid = "AC33cc375cb39231801a1e61ba70114c33";
            var authToken = "8b41e11d017fcfe7b8e551c860a50ed2";
            TwilioClient.Init(accountSid, authToken);

            MessageResource message = MessageResource.Create(
            body: "This is the ship that made the Kessel Run in fourteen parsecs?",
            from: new PhoneNumber("+447700142892"),
            to: new PhoneNumber("+989125031094"));

            return message;

        }
    }
}


