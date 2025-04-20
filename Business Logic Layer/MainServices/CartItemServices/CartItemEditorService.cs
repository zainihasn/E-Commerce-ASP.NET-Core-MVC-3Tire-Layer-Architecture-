using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.CartItemServices.Base;

namespace BusinessLogicLayer.Service.CartItemServices
{
    public class CartItemEditorService : ICartItemEditorService
    {
        public void IncreaseQuantity(CartItemDTO item)
        {
            item.Quantity += 1;
            item.Amount = item.Quantity * item.Price;
        }

        public void DecreaseQuantity(CartItemDTO item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity -= 1;
                item.Amount = item.Quantity * item.Price;
            }
        }
    }

}
