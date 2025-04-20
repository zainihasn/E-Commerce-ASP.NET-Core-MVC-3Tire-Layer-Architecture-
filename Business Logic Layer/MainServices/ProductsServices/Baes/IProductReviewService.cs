using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.ProductsServices.Baes
{
    public interface IProductReviewService
    {
        Task AddReviewAsync(ProductReviewDTO review);
        Task<List<ProductReviewDTO>> GetReviewsWithUserAsync(int productId);
        Task<ProductReviewDTO> CreateReviewAsync(ProductReviewDTO ReView);
    }

}
