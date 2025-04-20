using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Service.Base
{
    public interface IOrdersService
    {
        Task<OrdersDTO> GetById(int id);
        Task<OrdersDTO> Add(OrdersDTO OrdersDTO);
        Task<OrderItemDTO> AddOrderItem(OrderItemDTO OrderItemDTO);
        Task Update(OrdersDTO productDto);
        Task<List<OrdersDTO>> GetAllOrdersById(int OrderId);  
        (OrdersDTO order, List<OrderItemDTO> orderItems) CreateOrder(List<CartItemDTO> items, int guestId, int paymentId);
        Task<(OrdersDTO, List<OrderItemDTO>)> SaveOrderAsync(int guestId, List<CartItemDTO> cartItems, PaymentDTO paymentDTO);
        Task<List<OrderItemDTO>> GetAllOrderItemByIdAsync(int OrderId);
        Task Delete(int id);
    }
}
