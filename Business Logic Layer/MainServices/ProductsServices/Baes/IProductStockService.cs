using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.MainServices.ProductsServices.Baes
{
    public interface IProductStockService
    {
        Task<bool> ReduceStockAsync(int productId, int quantity);
    }

}
