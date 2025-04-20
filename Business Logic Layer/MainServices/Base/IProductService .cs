using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Service.Base
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAll();
        Task<List<ProductDTO>> GetAllProductById(int CategoryId, int? take);
        Task<ProductDTO> GetById(int ProductId);
        Task<ProductDTO> GetDetailsById(int ProductId);
        Task<List<ProductReviewDTO>> GetProductReviewsWithUserAsync(int ProductId);
        Task <List<ProductImageDTO>>GetAllProductImageDTOById(int ProductId);
        Task<List<ProductDTO>> GetTopSellingProducts(int topCount);
        Task<ProductImageDTO>GetOneProductImageDTOById(int ProductId);
        Task<List<ProductDTO>> GetLowStockProductsAsync(int threshold);
        Task<List<ProductDTO>> Search(string Search);
        Task Add(ProductDTO productDto);
        Task Update(ProductDTO productDto);
        Task Delete(int ProductId);
        Task<List<ProductImageDTO>> GetAllProductImagesAsync();
        Task AddProductImageDTO(ProductImageDTO productDto);
        Task UpdateProductImageDTO(ProductImageDTO productDto);
        Task DeleteProductImageDTO(int ProductImageId);
        Task<bool> ReduceStockAsync(int productId, int quantity);
        Task SaveProductReview(ProductReviewDTO Reviews );

    }
}
