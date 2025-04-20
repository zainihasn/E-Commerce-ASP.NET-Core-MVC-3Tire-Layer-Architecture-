using BusinessLogicLayer.Service;
using BusinessLogicLayer.Service.Base;
using E_comm.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_comm.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IOrdersService _orderService;
        private readonly IProductService _productService;
        private readonly IPaymentService _paymentService;
        private readonly IGuestService _guestService;
        public InvoiceController(IInvoiceService invoiceService, IOrdersService orderService,
            IProductService productService, IGuestService guestService, IPaymentService PaymentService)
        {
            _invoiceService = invoiceService;
            _orderService = orderService;
            _productService = productService;
            _guestService = guestService;
            _paymentService = PaymentService;
        }

        // عرض قائمة الفواتير
        public async Task<IActionResult> Index()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return View(invoices);
        }

     
        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null) return NotFound();
            var invoiceItems = await _invoiceService.GetAllInvoiceItemByIdAsync(id);
            var productDetails = new List<ProductDetailsInvoiceItemAdmin>();
            foreach (var item in invoiceItems)
            {
                var product = await _productService.GetById(item.ProductId);
                productDetails.Add(new ProductDetailsInvoiceItemAdmin
                {
                    InvoiceItem = item,
                    Product = product
                });
            }
            var viewModel = new InvoiceDetailsViewModel
            {
                Invoice = invoice,
                Items = productDetails
            };
            return View(viewModel);
        }

        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var order = await _orderService.GetById(orderId);
            if (order == null)
                return NotFound();
            var guestInfo = order.GuestInfo =  await _guestService.GetById(order.GuestId.Value);
            var payment = await _paymentService.GetOneOrdersPaymentById(order.PaymentId);
            var orderItems = await _orderService.GetAllOrderItemByIdAsync(orderId);
            var orderItemsWithProducts = new List<OrderItemWithProductViewModel>();
            foreach (var item in orderItems)
            {
                var product = await _productService.GetById(item.ProductId);  
                orderItemsWithProducts.Add(new OrderItemWithProductViewModel
                {
                    OrderItem = item,
                    Product = product
                });
            }
            var viewModel = new OrderDetailsViewModel
            {
                Order = order,
                GuestInfo = guestInfo,
                Payment = payment,
                OrderItems = orderItemsWithProducts
            };

            return View(viewModel);
        }

    }
}
