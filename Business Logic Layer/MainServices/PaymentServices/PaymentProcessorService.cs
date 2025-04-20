using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.MainServices.PaymentServices.Base;
using Stripe;

namespace BusinessLogicLayer.MainServices.PaymentServices
{
    public class PaymentProcessorService : IPaymentProcessorService
    {
        public bool ProcessPayment(decimal amount, string method, string cardToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51RBMRYCKY6kagkXqxlW0SnBH4NbsO2uSotqZRyOOwRnqDf974KNO5ApspKHsDAkfjeTI3LA9LKsVUSID49OwurWM002scQ90Ly"; // ضعه من ملف الإعدادات الأفضل

            var lowerMethod = method.ToLower();

            if ((lowerMethod == "visa" || lowerMethod == "mastercard") && !string.IsNullOrEmpty(cardToken))
            {
                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = (long)(amount * 100),
                    Currency = "usd",
                    Description = "دفع بواسطة بطاقة",
                    Source = cardToken
                };

                var chargeService = new ChargeService();
                try
                {
                    var charge = chargeService.Create(chargeOptions);
                    return charge.Status == "succeeded";
                }
                catch
                {
                    return false;
                }
            }

            return true; // في حالة طرق أخرى غير بطاقات
        }

        //public string CreateApplePaySession(decimal amount)
        //{
        //    StripeConfiguration.ApiKey = "";

        //    var sessionOptions = new SessionCreateOptions
        //    {
        //        PaymentMethodTypes = new List<string> { "card" },
        //        LineItems = new List<SessionLineItemOptions>
        //        {
        //            new SessionLineItemOptions
        //            {
        //                PriceData = new SessionLineItemPriceDataOptions
        //                {
        //                    UnitAmount = (long)(amount * 100),
        //                    Currency = "usd",
        //                    ProductData = new SessionLineItemPriceDataProductDataOptions
        //                    {
        //                        Name = "طلب من المتجر"
        //                    },
        //                },
        //                Quantity = 1,
        //            }
        //        },
        //        Mode = "payment",
        //        SuccessUrl = "https://example.com/success",
        //        CancelUrl = "https://example.com/cancel",
        //    };

        //    var sessionService = new SessionService();
        //    var session = sessionService.Create(sessionOptions);

        //    return session.Id; ; 
        //}
    }


}
