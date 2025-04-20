using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.Base;
using BusinessLogicLayer.Service.PaymentServices.Base;

namespace BusinessLogicLayer.Service.PaymentServices
{
    public class PaymentHandler : IPaymentHandler
    {
        private readonly IPaymentService _paymentService;

        public PaymentHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public Task<bool> HandlePaymentAsync(PaymentDTO paymentDTO, decimal total, string cardToken)
        {
            return Task.FromResult(
                paymentDTO.CardName.ToLower() == "cache" ||
                _paymentService.ProcessPayment(total, paymentDTO.CardName, cardToken)
            );
        }
    }

}
