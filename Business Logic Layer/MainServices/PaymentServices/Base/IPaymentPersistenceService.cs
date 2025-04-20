using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.PaymentServices.Base
{
    public interface IPaymentPersistenceService
    {
        Task<PaymentDTO> GetByIdAsync(int paymentId);
        Task<List<PaymentDTO>> GetAllAsync();
    }

}
