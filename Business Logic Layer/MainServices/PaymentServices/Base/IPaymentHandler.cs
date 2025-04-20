using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Service.PaymentServices.Base
{
    public interface IPaymentHandler
    {
        Task<bool> HandlePaymentAsync(PaymentDTO paymentDTO, decimal total, string cardToken);
    }
}
