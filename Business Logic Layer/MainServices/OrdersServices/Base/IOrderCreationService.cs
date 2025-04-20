using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.OrdersServices.Base
{
    public interface IOrderCreationService
    {
        (OrdersDTO order, List<OrderItemDTO> orderItems) CreateOrder(List<CartItemDTO> items, int guestId, int paymentId);
    }

}
