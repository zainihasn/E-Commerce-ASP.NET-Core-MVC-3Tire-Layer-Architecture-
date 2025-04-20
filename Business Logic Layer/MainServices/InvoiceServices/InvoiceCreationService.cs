using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.InvoiceServices.Base;

namespace BusinessLogicLayer.MainServices.InvoiceServices
{

    public class InvoiceCreationService : IInvoiceCreationService
    {
        public (InvoiceDTO, List<InvoiceItemDTO>) CreateInvoiceFromOrder(OrdersDTO order, List<OrderItemDTO> items)
        {
            var invoice = new InvoiceDTO
            {
                OrderId = order.OrderId,
                InvoiceDate = DateTime.Now,
                CustomerName = order.GuestInfo?.FullName,
                CustomerEmail = order.GuestInfo?.Email,
                CustomerPhone = order.GuestInfo?.PhoneNumber,
                PaymentMethod = order.Payment?.CardName,
                Address=order.GuestInfo.Address
                
            };

            var invoiceItems = items.Select(item => new InvoiceItemDTO
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Price
            }).ToList();

            invoice.SubTotal = invoiceItems.Sum(i => i.Amount);
            invoice.Tax = invoice.SubTotal * 0.15m;
            invoice.Total = invoice.SubTotal + invoice.Tax;

            return (invoice, invoiceItems);
        }
    }
}
