using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.InvoiceServices.Base;
using BusinessLogicLayer.Service.Base;

namespace BusinessLogicLayer.MainServices.InvoiceServices
{
    public class InvoiceHtmlGeneratorService : IInvoiceHtmlGeneratorService
    {
        private readonly IProductService _productService;

        public InvoiceHtmlGeneratorService(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<string> GenerateInvoiceHtml(InvoiceDTO invoice, List<InvoiceItemDTO> invoiceItems)
        {
            var sb = new StringBuilder();

            sb.Append("<html><head>");
            sb.Append("<style>");
            sb.Append(@"
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 40px; background-color: #f9f9f9; color: #333; }
        h2 { color: #444; }
        p { margin: 5px 0; }
        table { width: 100%; border-collapse: collapse; margin-top: 20px; }
        th, td { border: 1px solid #ddd; padding: 8px; text-align: center; }
        th { background-color: #f2f2f2; font-weight: bold; }
        tr:nth-child(even) { background-color: #f9f9f9; }
        tr:hover { background-color: #f1f1f1; }
        .summary { margin-top: 20px; font-size: 1.1em; }
        strong { font-size: 1.2em; color: #000; }
    ");
            sb.Append("</style>");
            sb.Append("</head><body>");

            sb.Append($"<h2>فاتورة رقم: {invoice.InvoiceId}</h2>");
            sb.Append($"<p>🗓 التاريخ: {invoice.InvoiceDate.ToShortDateString()}</p>");
            sb.Append($"<p>👤 العميل: {invoice.CustomerName}</p>");
            sb.Append($"<p>📧 الايميل: {invoice.CustomerEmail}</p>");
            sb.Append($"<p>📞 الهاتف: {invoice.CustomerPhone}</p>");
            sb.Append($"<p>📞 العـنوان: {invoice.Address}</p>");
            sb.Append($"<p>💳 طريقة الدفع: {invoice.PaymentMethod}</p>");

            sb.Append("<table><thead><tr><th>المنتج</th><th>الكمية</th><th>السعر</th><th>الإجمالي</th></tr></thead><tbody>");
            foreach (var item in invoiceItems)
            {
                var product = await _productService.GetById(item.ProductId);
                var productName = product?.ProductName ?? "غير معروف";

                sb.Append($"<tr><td>{productName}</td><td>{item.Quantity}</td><td>{item.UnitPrice:C}</td><td>{item.Amount:C}</td></tr>");
            }
            sb.Append("</tbody></table>");

            sb.Append("<div class='summary'>");
            sb.Append($"<p>المجموع الفرعي: {invoice.SubTotal:C}</p>");
            sb.Append($"<p>الضريبة: {invoice.Tax:C}</p>");
            sb.Append($"<p><strong>الإجمالي: {invoice.Total:C}</strong></p>");
            sb.Append("</div>");

            sb.Append("</body></html>");

            return sb.ToString();
        }
    }
}
