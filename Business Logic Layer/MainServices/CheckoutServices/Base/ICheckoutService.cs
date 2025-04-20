using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Service.CheckoutService.Base
{
    public interface ICheckoutService
    {
        Task<bool> ConfirmOrderAsync(List<CartItemDTO> cartItems, GuestInfoDTO guestInfoDTO,
            int? guestId, PaymentDTO paymentDTO, string cardToken);
    }
}
