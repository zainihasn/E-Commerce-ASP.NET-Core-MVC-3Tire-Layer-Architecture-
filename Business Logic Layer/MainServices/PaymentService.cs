using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.PaymentServices.Base;
using BusinessLogicLayer.Service.Base;


namespace BusinessLogicLayer.MainServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentPersistenceService _paymentPersistence;
        private readonly IPaymentProcessorService _paymentProcessor;

        public PaymentService(IPaymentPersistenceService paymentPersistence, IPaymentProcessorService paymentProcessor)
        {
            _paymentPersistence = paymentPersistence;
            _paymentProcessor = paymentProcessor;
        }

        public async Task<PaymentDTO> GetOneOrdersPaymentById(int PaymentID)
            => await _paymentPersistence.GetByIdAsync(PaymentID);

        public async Task<List<PaymentDTO>> GetAllOrdersPayment()
            => await _paymentPersistence.GetAllAsync();

        public bool ProcessPayment(decimal amount, string method, string cardToken)
            => _paymentProcessor.ProcessPayment(amount, method, cardToken);

    }

}