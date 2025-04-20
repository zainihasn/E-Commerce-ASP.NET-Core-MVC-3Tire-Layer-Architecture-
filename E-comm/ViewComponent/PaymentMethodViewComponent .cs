using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Service.Base;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_comm.ViewComponents
{
    public class PaymentMethodViewComponent : ViewComponent
    {
        private readonly IPaymentService _paymentService;

        public PaymentMethodViewComponent(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selected = 1)
        {
            var paymentMethods = await _paymentService.GetAllOrdersPayment();
            var selectList = new SelectList(paymentMethods, "PaymentId", "CardName", selected);
            return View("Default", selectList);
        }
    }

}
