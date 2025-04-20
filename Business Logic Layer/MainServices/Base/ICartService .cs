using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Service.Base
{
    public interface ICartService
    {
        Task<List<CartItemDTO>> GetCartAsync();
        Task AddToCartAsync(ProductDTO product, int quantity);
        Task<bool> ConfirmOrderAsync(List<CartItemDTO> cartItems, GuestInfoDTO guestInfoDTO, int? guestId, PaymentDTO paymentDTO, string cardToken);
        void ClearCart();
        Task Delete(int ProductId);
        Task EditPlus(int ProductId);
        Task EditMins(int ProductId);
    }
}
