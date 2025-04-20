using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.Base;
using BusinessLogicLayer.Service.CartItemSerVice.Base;
using BusinessLogicLayer.Service.OrdersServices.Base;


namespace BusinessLogicLayer.Service
{
    public class CartService : ICartService
    {
        private readonly ICartManagerService _cartManagerService;
        private readonly IOrderProcessorService _orderProcessorService;

        public CartService(ICartManagerService cartManagerService, IOrderProcessorService orderProcessorService)
        {
            _cartManagerService = cartManagerService;
            _orderProcessorService = orderProcessorService;
        }

        public Task<List<CartItemDTO>> GetCartAsync() => _cartManagerService.GetCartAsync();
        public Task AddToCartAsync(ProductDTO product, int quantity) => _cartManagerService.AddToCartAsync(product, quantity);
        public Task Delete(int ProductId) => _cartManagerService.Delete(ProductId);
        public Task EditPlus(int ProductId) => _cartManagerService.EditPlus(ProductId);
        public Task EditMins(int ProductId) => _cartManagerService.EditMins(ProductId);
        public void ClearCart() => _cartManagerService.ClearCart();
        public Task<bool> ConfirmOrderAsync(List<CartItemDTO> cartItems, GuestInfoDTO guestInfoDTO, int? guestId, PaymentDTO paymentDTO, string cardToken)
       => _orderProcessorService.ConfirmOrderAsync(cartItems, guestInfoDTO, guestId, paymentDTO, cardToken);
    }
}


