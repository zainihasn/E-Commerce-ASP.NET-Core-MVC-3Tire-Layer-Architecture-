using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.Base;
using BusinessLogicLayer.Service.OrdersServices.Base;
using BusinessLogicLayer.Service.PaymentServices.Base;

namespace BusinessLogicLayer.Service.OrdersServices
{
    public class OrderProcessorService : IOrderProcessorService
    {
        private readonly IGuestService _guestService;
        private readonly IPaymentHandler _paymentHandler;

        public OrderProcessorService(IGuestService guestService, IPaymentHandler paymentHandler)
        {
            _guestService = guestService;
            _paymentHandler = paymentHandler;
        }

        public async Task<bool> ConfirmOrderAsync(List<CartItemDTO> cartItems, GuestInfoDTO guestInfoDTO, int? guestId, PaymentDTO paymentDTO, string cardToken)
        {
            if (!guestId.HasValue || cartItems == null || !cartItems.Any()) return false;
            await _guestService.UpdateGuestInfoAsync(guestId.Value, guestInfoDTO);
            decimal total = cartItems.Sum(c => c.Amount);
            return await _paymentHandler.HandlePaymentAsync(paymentDTO, total, cardToken);
        }
    }

}
