using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.MainServices.PaymentServices.Base
{
    public interface IPaymentProcessorService
    {
        bool ProcessPayment(decimal amount, string method, string cardToken);

        //string CreateApplePaySession(decimal amount)
    }

}
