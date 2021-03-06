// Download the twilio-csharp library from twilio.com/docs/libraries/csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Twilio;
using Twilio.Rest.Lookups.V1;
using Twilio.Rest.Pricing.V1.Messaging;
using Twilio.Types;

public class Example
{
    public static void Main(string[] args)
    {
        // Find your Account SID and Auth Token at twilio.com/console
        const string accountSid = "ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        const string authToken = "your_auth_token";

        TwilioClient.Init(accountSid, authToken);

        var phoneNumber = PhoneNumberResource.Fetch(
                new PhoneNumber("+15108675309"),
                type: new List<string> { "carrier" });

        var mcc = phoneNumber.Carrier["mobile_country_code"];
        var mnc = phoneNumber.Carrier["mobile_network_code"];

        var countryCode = phoneNumber.CountryCode;
        var countries = CountryResource.Fetch(countryCode);

        var prices = countries
            .OutboundSmsPrices
            .Where(price => price.Mcc.Equals(mcc) && price.Mnc.Equals(mnc))
            .SelectMany(price => price.Prices);


        foreach (var price in prices)
        {
            Console.WriteLine($"Country {countryCode}");
            Console.WriteLine($"Current price {price.BasePrice}");
            Console.WriteLine($"Current price {price.CurrentPrice}");
        }
    }
}
