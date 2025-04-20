using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.InvoiceServices.Base
{
    public interface IInvoicePersistenceService
    {
        Task<InvoiceDTO> Add(InvoiceDTO invoiceDTO);
        Task AddInvoiceItemDTO(InvoiceItemDTO invoiceItemDTO);
        Task<List<InvoiceItemDTO>> GetAllInvoiceItemByIdAsync(int invoiceId);
        Task<InvoiceItemDTO> GetInvoiceByIdAsyncItemAsync(int invoiceItemId);
        Task<InvoiceDTO> GetInvoiceByIdAsync(int invoiceId);
        Task<List<InvoiceDTO>> GetAllInvoicesAsync();
    }
}
