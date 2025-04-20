using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.PaymentServices.Base;
using DataAccessLayer.Repository.Base;

namespace BusinessLogicLayer.MainServices.PaymentServices
{
    public class PaymentPersistenceService : IPaymentPersistenceService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public PaymentPersistenceService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<PaymentDTO> GetByIdAsync(int paymentId)
        {
            var entity = await _unit.Payment.FindByIdAsync(paymentId);
            return _mapper.Map<PaymentDTO>(entity);
        }

        public async Task<List<PaymentDTO>> GetAllAsync()
        {
            var payments = await _unit.Payment.FindAllAsync();
            return _mapper.Map<List<PaymentDTO>>(payments);
        }
    }

}
