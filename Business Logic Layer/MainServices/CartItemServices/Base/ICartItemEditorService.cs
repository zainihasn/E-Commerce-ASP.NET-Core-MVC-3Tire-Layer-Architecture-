using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Service.CartItemServices.Base
{
    public interface ICartItemEditorService
    {
        void IncreaseQuantity(CartItemDTO item);
        void DecreaseQuantity(CartItemDTO item);
    }

}
