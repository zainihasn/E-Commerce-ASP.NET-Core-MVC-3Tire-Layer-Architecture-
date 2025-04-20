using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.OrdersServices.Base;

namespace BusinessLogicLayer.MainServices.OrdersServices
{
    public class OrderCreationService : IOrderCreationService
    {
        public (OrdersDTO order, List<OrderItemDTO> orderItems) CreateOrder(List<CartItemDTO> items, int guestId, int paymentId)
        {
            var order = new OrdersDTO
            {
                OrderDate = DateTime.Today,
                GuestId = guestId,
                Status = 1,
                IsCancel = false,
                PaymentId = paymentId
            };

            var orderItems = items.Select(item => new OrderItemDTO
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList();

            return (order, orderItems);
        }
    }

}
