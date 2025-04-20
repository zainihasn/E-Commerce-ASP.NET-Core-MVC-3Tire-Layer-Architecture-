using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Service.CartItemSerVice.Base
{
    public interface ICartManagerService
    {

        Task<List<CartItemDTO>> GetCartAsync();  
        void ClearCart();
        Task AddToCartAsync(ProductDTO product, int quantity);
        Task EditPlus(int productId);
        Task EditMins(int productId);
        Task Delete(int productId);
    }
}
