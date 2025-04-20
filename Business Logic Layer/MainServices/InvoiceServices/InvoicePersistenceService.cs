using System;
using System.Collections.Generic;

using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.InvoiceServices.Base;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.Base;

namespace BusinessLogicLayer.MainServices.InvoiceServices
{
    public class InvoicePersistenceService : IInvoicePersistenceService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public InvoicePersistenceService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<InvoiceDTO> Add(InvoiceDTO invoiceDTO)
        {
            var invoice = _mapper.Map<Invoice>(invoiceDTO);
            await _unit.Invoice.AddOneAsync(invoice);
            return _mapper.Map<InvoiceDTO>(invoice);
        }

        public async Task AddInvoiceItemDTO(InvoiceItemDTO invoiceItemDTO)
        {
            var invoiceItem = _mapper.Map<InvoiceItem>(invoiceItemDTO);
            await _unit.InvoiceItem.AddOneAsync(invoiceItem);
        }

        public async Task<List<InvoiceItemDTO>> GetAllInvoiceItemByIdAsync(int invoiceId)
        {
            var items = await _unit.InvoiceItem.FindAllAsync(i => i.InvoiceId == invoiceId);
            return _mapper.Map<List<InvoiceItemDTO>>(items);
        }

        public async Task<InvoiceItemDTO> GetInvoiceByIdAsyncItemAsync(int invoiceItemId)
        {
            var item = await _unit.InvoiceItem.FindByIdAsync(invoiceItemId);
            return _mapper.Map<InvoiceItemDTO>(item);
        }

        public async Task<InvoiceDTO> GetInvoiceByIdAsync(int invoiceId)
        {
            var invoice = await _unit.Invoice.FindByIdAsync(invoiceId);
            return _mapper.Map<InvoiceDTO>(invoice);
        }

        public async Task<List<InvoiceDTO>> GetAllInvoicesAsync()
        {
            var invoices = await _unit.Invoice.FindAllAsync();
            return _mapper.Map<List<InvoiceDTO>>(invoices);
        }
    }
}
