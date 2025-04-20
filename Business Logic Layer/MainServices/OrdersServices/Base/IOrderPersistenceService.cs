using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.OrdersServices.Base
{
    public interface IOrderPersistenceService
    {
        Task<OrdersDTO> GetById(int OrderId);
        Task<OrdersDTO> AddAsync(OrdersDTO OrdersDTO);
        Task<OrdersDTO> SaveOrderAsync(OrdersDTO order);
        Task<OrderItemDTO> AddOrderItemSync(OrderItemDTO item);
        Task<List<OrderItemDTO>> GetAllOrderItemByIdAsync(int OrderId);
        Task UpdateAsync(OrdersDTO orderDto);
        Task DeleteAsync(int orderId);
        
    }

}
