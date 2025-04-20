using System.Security.Claims;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.Base;
using BusinessLogicLayer.Service.Base;
using BusinessLogicLayer.Service.CheckoutService.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.MainServices
{
    public class CheckoutServices : ICheckoutServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICartService _cartService;
        private readonly ICheckoutService _checkoutService;
        private readonly IOrdersService _ordersService;
        private readonly IInvoiceService _invoiceService;
        private readonly INotifications _notifications;
        private readonly IPaymentService _paymentService;
        private readonly IGuestService _guestService;

        public CheckoutServices(
            UserManager<IdentityUser> userManager,
            ICartService cartService,
            ICheckoutService checkoutService,
            IOrdersService ordersService,
            IInvoiceService invoiceService,
            INotifications notifications,
            IPaymentService paymentService,
            IGuestService guestService)
        {
            _guestService = guestService;
            _userManager = userManager;
            _cartService = cartService;
            _checkoutService = checkoutService;
            _ordersService = ordersService;
            _invoiceService = invoiceService;
            _notifications = notifications;
            _paymentService = paymentService;
        }

        public async Task<CheckoutResultDTO> ProcessCheckoutAsync(PaymentDTO Payment, GuestInfoDTO GuestInfoDTO, ClaimsPrincipal user, HttpContext httpContext , string CardToken)
        {
            var guestId = httpContext.Session.GetInt32("GuestId");
            var cartItems = await _cartService.GetCartAsync();

            if (!cartItems.Any())
                return CheckoutResultDTO.Failed();

            var guestInfo = await GetGuestInfoAsync(GuestInfoDTO, user, guestId);
            if (guestInfo == null)
                return CheckoutResultDTO.Failed();

            var paymentMethod = await _paymentService.GetOneOrdersPaymentById(Payment.PaymentId);
            if (paymentMethod == null)
                return CheckoutResultDTO.Failed();

            bool isConfirmed = await _checkoutService.ConfirmOrderAsync(cartItems, guestInfo, guestId, paymentMethod, CardToken);
            if (!isConfirmed)
                return CheckoutResultDTO.Failed();

            var (order, orderItems) = await _ordersService.SaveOrderAsync(guestId ?? 0, cartItems, paymentMethod);
            var (savedInvoice, invoiceItems) = await _invoiceService.SaveInvoiceAsync(order, orderItems);
            var invoiceHtml = await _invoiceService.GenerateInvoiceHtml(savedInvoice, invoiceItems);

            await _notifications.SendMessageAsync(invoiceHtml);

            return CheckoutResultDTO.Success(invoiceHtml);
        }

        private async Task<GuestInfoDTO> GetGuestInfoAsync(GuestInfoDTO model, ClaimsPrincipal user, int? guestId)
        {
            if (user.Identity?.IsAuthenticated == true)
            {
                var currentUser = await _userManager.GetUserAsync(user);
                var guset = await _guestService.GetGuestInfoByUserNameAsync(currentUser.UserName);
                return guset;
            }
            else if (guestId.HasValue)
            {
                return model;
            }

            return null;
        }
    }

}
