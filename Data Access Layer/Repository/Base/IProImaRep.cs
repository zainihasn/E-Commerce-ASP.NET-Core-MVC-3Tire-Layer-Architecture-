using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;

namespace DataAccessLayer.Repository.Base
{
    public interface IProImaRep : IRepository<Product>
    {
        //product_images_repository
        //special_repository_to_add_images 
        Task<Product?> GetProductRatingById(int productId);
        Task<List<Product>> GetProductsBySearchAsync(string search);
        Task<List<Product>> GetTopSellingProductsAsync(int topCount);
        Task<List<ProductReview>> GetProductReviewsWithUserAsync(int ProductId);
        Task<List<Product>> GetLowStockProductsAsync(int threshold);
        Task<GuestInfo> FindOneAsync(Expression<Func<GuestInfo, bool>> predicate);




    }
}
