
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.CartItemServices.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BusinessLogicLayer.Service.CartItemServices
{
    public class CartCookieService : ICartCookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartCookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItemDTO> GetCartFromCookie()
        {
            var cartCookie = _httpContextAccessor.HttpContext?.Request.Cookies["Cart"];
            if (string.IsNullOrEmpty(cartCookie))
                return new List<CartItemDTO>();
            return JsonConvert.DeserializeObject<List<CartItemDTO>>(cartCookie) ?? new List<CartItemDTO>();
        }

        public void SaveCartToCookie(List<CartItemDTO> cartItems)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("Cart",
                JsonConvert.SerializeObject(cartItems),
                new CookieOptions { Expires = DateTime.UtcNow.AddDays(1), HttpOnly = true });
        }

        public void ClearCartCookie()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("Cart");
        }
    }


}
