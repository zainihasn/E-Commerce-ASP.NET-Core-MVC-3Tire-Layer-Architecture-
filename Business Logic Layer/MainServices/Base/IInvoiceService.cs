using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Service.Base
{
    public interface IInvoiceService
    {
        (InvoiceDTO invoice, List<InvoiceItemDTO> invoiceItems) CreateInvoiceFromOrder(OrdersDTO order, List<OrderItemDTO> items);
        Task<(InvoiceDTO, List<InvoiceItemDTO>)> SaveInvoiceAsync(OrdersDTO order, List<OrderItemDTO> orderItems);
        Task<string> GenerateInvoiceHtml(InvoiceDTO invoice, List<InvoiceItemDTO> invoiceItems);
        Task<InvoiceDTO> Add(InvoiceDTO invoiceDTO);
        Task AddInvoiceItemDTO(InvoiceItemDTO invoiceItemDTO);
        Task<InvoiceDTO> GetInvoiceByIdAsync(int InvoiceId);
        Task<List<InvoiceDTO>> GetAllInvoicesAsync();
        Task<InvoiceItemDTO> GetInvoiceByIdAsyncItemAsync(int InvoiceItemId);
        Task<List<InvoiceItemDTO>> GetAllInvoiceItemByIdAsync(int InvoiceItemId);
    }
}
