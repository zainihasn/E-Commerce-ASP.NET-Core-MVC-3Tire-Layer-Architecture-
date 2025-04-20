using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.InvoiceServices.Base;
using BusinessLogicLayer.Service.Base;

namespace BusinessLogicLayer.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceCreationService _creationService;
        private readonly IInvoicePersistenceService _persistenceService;
        private readonly IInvoiceHtmlGeneratorService _htmlService;

        public InvoiceService(
            IInvoiceCreationService creationService,
            IInvoicePersistenceService persistenceService,
            IInvoiceHtmlGeneratorService htmlService)
        {
            _creationService = creationService;
            _persistenceService = persistenceService;
            _htmlService = htmlService;
        }

        public (InvoiceDTO, List<InvoiceItemDTO>) CreateInvoiceFromOrder(OrdersDTO order, List<OrderItemDTO> items)
            => _creationService.CreateInvoiceFromOrder(order, items);

        public async Task<(InvoiceDTO, List<InvoiceItemDTO>)> SaveInvoiceAsync(OrdersDTO order, List<OrderItemDTO> items)
        {
            var (invoice, invoiceItems) = CreateInvoiceFromOrder(order, items);
            var savedInvoice = await _persistenceService.Add(invoice);
            foreach (var item in invoiceItems)
            {
                item.InvoiceId = savedInvoice.InvoiceId;
                await _persistenceService.AddInvoiceItemDTO(item);
            }
            return (savedInvoice, invoiceItems);
        }


        public async Task<string> GenerateInvoiceHtml(InvoiceDTO invoice, List<InvoiceItemDTO> invoiceItems)
            => await _htmlService.GenerateInvoiceHtml(invoice, invoiceItems);

        public async Task<InvoiceDTO> Add(InvoiceDTO invoiceDTO)
            => await _persistenceService.Add(invoiceDTO);

        public async Task AddInvoiceItemDTO(InvoiceItemDTO invoiceItemDTO)
            => await _persistenceService.AddInvoiceItemDTO(invoiceItemDTO);

        public async Task<InvoiceDTO> GetInvoiceByIdAsync(int invoiceId)
            => await _persistenceService.GetInvoiceByIdAsync(invoiceId);

        public async Task<List<InvoiceDTO>> GetAllInvoicesAsync()
            => await _persistenceService.GetAllInvoicesAsync();

        public async Task<List<InvoiceItemDTO>> GetAllInvoiceItemByIdAsync(int invoiceId)
            => await _persistenceService.GetAllInvoiceItemByIdAsync(invoiceId);

        public async Task<InvoiceItemDTO> GetInvoiceByIdAsyncItemAsync(int invoiceItemId)
            => await _persistenceService.GetInvoiceByIdAsyncItemAsync(invoiceItemId);
    }

}