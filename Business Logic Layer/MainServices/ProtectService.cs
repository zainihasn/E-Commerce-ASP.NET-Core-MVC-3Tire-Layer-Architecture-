
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.ProductsServices.Baes;
using BusinessLogicLayer.Service.Base;



namespace BusinessLogicLayer.Service
{

    public class ProductService : IProductService
    {
        private readonly IProductManagementService _managementService;
        private readonly IProductImageService _imageService;
        private readonly IProductReviewService _reviewService;
        private readonly IProductQueryService _queryService;
        private readonly IProductStockService _stockService;

        public ProductService(
            IProductManagementService managementService,
            IProductImageService imageService,
            IProductReviewService reviewService,
            IProductQueryService queryService,
            IProductStockService stockService)
        {
            _managementService = managementService;
            _imageService = imageService;
            _reviewService = reviewService;
            _queryService = queryService;
            _stockService = stockService;
        }

        

        public Task<List<ProductDTO>> GetAll() 
            => _managementService.GetAllAsync();

        public Task<ProductDTO> GetById(int id) 
            => _managementService.GetByIdAsync(id);

        public Task Add(ProductDTO productDto)
            => _managementService.AddAsync(productDto);

        public Task Update(ProductDTO productDto)
            => _managementService.UpdateAsync(productDto);

        public Task Delete(int productId) 
            => _managementService.DeleteAsync(productId);

        public Task<List<ProductDTO>> GetAllProductById(int categoryId, int? take) 
            => _queryService.GetByCategoryAsync(categoryId, take);

        public Task<ProductDTO> GetDetailsById(int productId) 
            => _queryService.GetDetailsByIdAsync(productId);

        public Task<List<ProductImageDTO>> GetAllProductImageDTOById(int productId) 
            => _imageService.GetAllByIdAsync(productId);

        public Task<ProductImageDTO> GetOneProductImageDTOById(int productId) 
            => _imageService.GetByIdAsync(productId);

        public Task AddProductImageDTO(ProductImageDTO productDto) 
            => _imageService.AddAsync(productDto);

        public Task UpdateProductImageDTO(ProductImageDTO productDto)
            => _imageService.UpdateAsync(productDto);

        public Task DeleteProductImageDTO(int id) 
            => _imageService.DeleteAsync(id);

        public Task<List<ProductDTO>> Search(string search) 
            => _queryService.SearchAsync(search);

        public Task<List<ProductDTO>> GetTopSellingProducts(int topCount) 
            => _queryService.GetTopSellingAsync(topCount);

        public Task<List<ProductImageDTO>> GetAllProductImagesAsync() 
            => _imageService.GetAllAsync();

        public Task<bool> ReduceStockAsync(int productId, int quantity) 
            => _stockService.ReduceStockAsync(productId, quantity);

        public Task<List<ProductReviewDTO>> GetProductReviewsWithUserAsync(int productId) 
            => _reviewService.GetReviewsWithUserAsync(productId);

        public async Task SaveProductReview(ProductReviewDTO review )
        {
           var Review = await _reviewService.CreateReviewAsync(review);
         await   _reviewService.AddReviewAsync(Review);
        }

        public Task<List<ProductDTO>> GetLowStockProductsAsync(int threshold)
          => _queryService.GetLowStockProductsAsync(threshold);
    }

}


