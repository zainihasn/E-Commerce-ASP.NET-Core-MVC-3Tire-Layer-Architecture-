using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.Base;
using BusinessLogicLayer.Service.CartItemSerVice.Base;
using BusinessLogicLayer.Service.CartItemServices.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BusinessLogicLayer.Service.CartItemSerVice
{
    public class CartManagerService : ICartManagerService
    {
        private readonly ICartCookieService _cookieService;
        private readonly ICartItemEditorService _itemEditor;
        private readonly IProductService _productService;
        private readonly IGuestService _guestService;

        public CartManagerService(ICartCookieService cookieService,
            ICartItemEditorService itemEditor,
            IProductService productService,
            IGuestService guestService)
        {
            _cookieService = cookieService;
            _itemEditor = itemEditor;
            _productService = productService;
            _guestService = guestService;
        }

        public async Task<List<CartItemDTO>> GetCartAsync()
        {
            return  _cookieService.GetCartFromCookie();
        }

        public async Task AddToCartAsync(ProductDTO product, int quantity)
        {
            if (product == null || product.ProductId <= 0 || quantity <= 0)
                throw new ArgumentException("بيانات المنتج غير صحيحة!");

            var cartItems = await GetCartAsync();
            var existingItem = cartItems.FirstOrDefault(c => c.ProductId == product.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.Amount = existingItem.Quantity * existingItem.Price;
            }
            else
            {
                var images = await _productService.GetOneProductImageDTOById(product.ProductId);
                var guest = _guestService.GetGuestUserNameFromCookie();
                cartItems.Add(new CartItemDTO
                {
                    ProductImages = images.ImageUrl,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = quantity,
                    USerName = guest,
                    Amount = product.Price * quantity
                });
            }

            _cookieService.SaveCartToCookie(cartItems);
        }

        public void ClearCart()
        {
            _cookieService.ClearCartCookie();
        }

        public async Task Delete(int productId)
        {
            var cartItems = await GetCartAsync();
            var item = cartItems.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                cartItems.Remove(item);
                _cookieService.SaveCartToCookie(cartItems);
            }
        }

        public async Task EditPlus(int productId)
        {
            var cartItems = await GetCartAsync();
            var item = cartItems.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                _itemEditor.IncreaseQuantity(item);
                _cookieService.SaveCartToCookie(cartItems);
            }
        }

        public async Task EditMins(int productId)
        {
            var cartItems = await GetCartAsync();
            var item = cartItems.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                _itemEditor.DecreaseQuantity(item);
                _cookieService.SaveCartToCookie(cartItems);
            }
        }

     
    }

} 

