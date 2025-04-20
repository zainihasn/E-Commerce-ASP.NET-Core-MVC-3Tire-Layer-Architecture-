using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.ProductsServices.Baes
{
    public interface IProductQueryService
    {
        Task<List<ProductDTO>> GetByCategoryAsync(int categoryId, int? take);
        Task<ProductDTO> GetDetailsByIdAsync(int productId);
        Task<List<ProductDTO>> SearchAsync(string keyword);
        Task<List<ProductDTO>> GetTopSellingAsync(int count);
        Task<List<ProductDTO>> GetLowStockProductsAsync(int threshold);
    }

}
