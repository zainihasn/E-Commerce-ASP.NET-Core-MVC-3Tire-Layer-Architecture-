using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.InvoiceServices.Base
{
    public interface IInvoiceCreationService
    {
        (InvoiceDTO invoice, List<InvoiceItemDTO> invoiceItems) CreateInvoiceFromOrder(OrdersDTO order, List<OrderItemDTO> items);
    }

}
