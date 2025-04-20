using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.MainServices.ProductsServices.Baes;

namespace BusinessLogicLayer.MainServices.ProductsServices
{
    public class ProductStockService : IProductStockService
    {
        private readonly IProductManagementService _productService;

        public ProductStockService(IProductManagementService productService)
        {
            _productService = productService;
        }

        public async Task<bool> ReduceStockAsync(int productId, int quantity)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product == null || product.Quantity < quantity)
                return false;

            product.Quantity -= quantity;
            product.Sold += quantity;
            product.IsActive = product.Quantity > 0;
            await _productService.UpdateAsync(product);

            return true;
        }
    }

}
