using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.OrdersServices.Base;
using BusinessLogicLayer.Service.Base;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.Base;
using Stripe.Climate;

namespace BusinessLogicLayer.Service
{


    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _MyUnit;
        private readonly IMapper _mapper;
        private readonly IOrderCreationService _orderCreationService;
        private readonly IOrderPersistenceService _orderPersistenceService;

        public OrdersService(
            IUnitOfWork MyUnit,
            IMapper mapper,
            IOrderCreationService orderCreationService,
            IOrderPersistenceService orderPersistenceService)
        {
            _MyUnit = MyUnit;
            _mapper = mapper;
            _orderCreationService = orderCreationService;
            _orderPersistenceService = orderPersistenceService;
        }

        public async Task<OrdersDTO> GetById(int OrderId) => await _orderPersistenceService.GetById(OrderId);
        public async Task<OrdersDTO> Add(OrdersDTO OrdersDTO) => await _orderPersistenceService.AddAsync(OrdersDTO);
        public async Task Update(OrdersDTO OrdersDTO) => await _orderPersistenceService.UpdateAsync(OrdersDTO);
        public async Task Delete(int OrdersId) => await _orderPersistenceService.DeleteAsync(OrdersId);
       
        public async Task<List<OrdersDTO>> GetAllOrdersById(int OrderId)
        {
            var Orders = await _MyUnit.Orders.FindAllAsync(p => p.GuestId == OrderId);
            return _mapper.Map<List<OrdersDTO>>(Orders);
        }


        public (OrdersDTO order, List<OrderItemDTO> orderItems) CreateOrder(List<CartItemDTO> items, int guestId, int paymentId)
            => _orderCreationService.CreateOrder(items, guestId, paymentId);
        public async Task<OrderItemDTO> AddOrderItem(OrderItemDTO OrderItemDTO)
            => await _orderPersistenceService.AddOrderItemSync(OrderItemDTO);



        public async Task<(OrdersDTO, List<OrderItemDTO>)> SaveOrderAsync(int guestId, List<CartItemDTO> cartItems, PaymentDTO paymentDTO)
        {
            var (order, orderItems) = CreateOrder(cartItems, guestId, paymentDTO.PaymentId);
            order = await _orderPersistenceService.SaveOrderAsync(order);

            foreach (var item in orderItems)
            {
                item.OrderId = order.OrderId;
                await AddOrderItem(item);
            }
            return (order, orderItems);
        }



        public async Task<List<OrderItemDTO>> GetAllOrderItemByIdAsync(int OrderId)
            => await _orderPersistenceService.GetAllOrderItemByIdAsync(OrderId); 

    }
}

