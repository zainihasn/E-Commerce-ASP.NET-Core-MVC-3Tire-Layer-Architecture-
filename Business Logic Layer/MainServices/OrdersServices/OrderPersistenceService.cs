using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.OrdersServices.Base;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.Base;

namespace BusinessLogicLayer.MainServices.OrdersServices
{
    public class OrderPersistenceService : IOrderPersistenceService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public OrderPersistenceService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }


        public async Task<OrdersDTO> AddAsync(OrdersDTO OrdersDTO)
        {
            var Order = _mapper.Map<Orders>(OrdersDTO);
            await _unit.Orders.AddOneAsync(Order);
            return _mapper.Map<OrdersDTO>(Order);
        }

        public async Task<OrdersDTO> GetById(int OrderId)
        {
            var Order = await _unit.Orders.FindByIdAsync(OrderId);
            return _mapper.Map<OrdersDTO>(Order);
        }


        public async Task UpdateAsync(OrdersDTO orderDto)
        {
            var existingOrder = await _unit.Orders.FindByIdAsync(orderDto.OrderId);
            if (existingOrder == null) return;

            _mapper.Map(orderDto, existingOrder);
            await _unit.Orders.EditOneAsync(existingOrder);
        }

        public async Task DeleteAsync(int orderId)
        {
            var order = await _unit.Orders.FindByIdAsync(orderId);
            if (order == null) return;

            await _unit.Orders.DeleteOneAsync(order);
        }

        public async Task<OrdersDTO> SaveOrderAsync(OrdersDTO order)
        {
            var entity = _mapper.Map<Orders>(order);
            await _unit.Orders.AddOneAsync(entity);
            return _mapper.Map<OrdersDTO>(entity);
        }

        public async Task<OrderItemDTO> AddOrderItemSync(OrderItemDTO item)
        {
            var entity = _mapper.Map<OrderItem>(item);
            await _unit.OrderItem.AddOneAsync(entity);
            return _mapper.Map<OrderItemDTO>(entity);
        }
        public async Task<List<OrderItemDTO>> GetAllOrderItemByIdAsync(int OrderId)
        {
            var OrderItem = await _unit.OrderItem.FindAllAsync(i => i.OrderId == OrderId);
            return _mapper.Map<List<OrderItemDTO>>(OrderItem);
        }

    }

}
