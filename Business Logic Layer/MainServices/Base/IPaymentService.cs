using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Service.Base
{
    public interface IPaymentService
    {
        public bool ProcessPayment(decimal amount, string method, string cardToken );
        //public  Task<string> Bill(OrdersDTO order);
        Task<PaymentDTO> GetOneOrdersPaymentById(int PaymentID);
        Task<List<PaymentDTO>> GetAllOrdersPayment();
    }
}
