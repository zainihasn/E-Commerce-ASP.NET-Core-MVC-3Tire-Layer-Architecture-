using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.Base;
using BusinessLogicLayer.Service.CartItemServices.Base;
using BusinessLogicLayer.Service.CheckoutService.Base;

public class CheckoutService : ICheckoutService
{
    private readonly IGuestService _guestService;
    private readonly IPaymentService _paymentService;
    private readonly IOrdersService _ordersService;
    private readonly IInvoiceService _invoiceService;
    private readonly ICartCookieService _cookieService;

    public CheckoutService(IGuestService guestService, IPaymentService paymentService,
        IOrdersService ordersService, IInvoiceService invoiceService, ICartCookieService cookieService)
    {
        _cookieService = cookieService;
        _guestService = guestService;
        _paymentService = paymentService;
        _ordersService = ordersService;
        _invoiceService = invoiceService;
    }

    public async Task<bool> ConfirmOrderAsync(List<CartItemDTO> cartItems, GuestInfoDTO guestInfoDTO, int? guestId, PaymentDTO paymentDTO, string cardToken)
    {
        if (!await ValidateGuestAndCartAsync(guestId)) return false;
        await _guestService.UpdateGuestInfoAsync(guestId.Value, guestInfoDTO);
        bool paymentSuccess = await HandlePaymentAsync(paymentDTO, totalAmount(cartItems), cardToken);
        if (!paymentSuccess) return false;
        return true;
    }
     

    private decimal totalAmount(List<CartItemDTO> cartItem)
    {
        var totalAmount = cartItem.Sum(i => i.Amount);
        return totalAmount;
    }

    private Task<bool> ValidateGuestAndCartAsync(int? guestId)
    {
        var cartItems = _cookieService.GetCartFromCookie();
        return Task.FromResult(guestId.HasValue && guestId != 0 && cartItems != null && cartItems.Any());
    }

    private Task<bool> HandlePaymentAsync(PaymentDTO paymentDTO, decimal total, string cardToken)
    {
        bool result = paymentDTO.CardName.ToLower() == "cache"
                      || _paymentService.ProcessPayment(total, paymentDTO.CardName, cardToken);
        return Task.FromResult(result);
    }

}
