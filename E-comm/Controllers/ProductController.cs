using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.Base;
using E_comm.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_comm.Controllers
{
    public class ProductController : Controller
    {
     
        private readonly IProductService _productService;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


       

        
        public async Task<IActionResult> Index(int CategoryId)
        {
            var products = await _productService.GetAllProductById(CategoryId,100);
            var viewModels = new List<ProductWithImagesViewModelProduct>();
            foreach (var product in products)
            {            
                var images = await _productService.GetOneProductImageDTOById(product.ProductId);
                viewModels.Add(new ProductWithImagesViewModelProduct
                {
                    Product = product,
                    Images = images,
                });
            }
            return View(viewModels);
        }


        public async Task<IActionResult> ProductDetails(int ProductId,int CategoryId )
        {
            var product = await _productService.GetDetailsById(ProductId);
            if (product == null) return NotFound();
            var image = await _productService.GetAllProductImageDTOById(ProductId);
            var review = await _productService.GetProductReviewsWithUserAsync(ProductId);
            var top5SimilarProducts = await _productService.GetAllProductById(CategoryId, 4);
            var similarWithImages = new List<ProductWithImagesViewModelProduct>();
            foreach (var p in top5SimilarProducts)
            {
                var images = await _productService.GetOneProductImageDTOById(p.ProductId);
                similarWithImages.Add(new ProductWithImagesViewModelProduct
                {
                    Product = p,
                    Images = images
                });
            }
            var viewModel = new ProductDetailsViewModelProduct
            {
                Product = product,
                imageDTO = image,
                reviewDTO = review,
                SimilarProducts = similarWithImages,
                Review = new ProductReviewDTO()

            };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(ProductDetailsViewModelProduct viewModel)
        {
            int categoryId = viewModel.Product.CategoryId;
            int productId = viewModel.Product.ProductId;
            foreach (var key in ModelState.Keys.ToList())
            {
                if (!key.StartsWith("Review"))
                {
                    ModelState.Remove(key);
                }
            }       
            if (ModelState.IsValid)
            {
                
                var ReView = viewModel.Review;

                if (!User.Identity.IsAuthenticated)
                {
                    string returnUrl = Url.Action("ProductDetails", "Product", new { ProductId = productId });
                    return Redirect("/Identity/Account/Login?returnUrl=" + Uri.EscapeDataString(returnUrl));
                }       
                await _productService.SaveProductReview(ReView); 
                return RedirectToAction("ProductDetails", new { CategoryId = categoryId, ProductId = productId });
            }
            else
            {
                return RedirectToAction("ProductDetails", new { CategoryId = categoryId, ProductId = productId });
            }
        }


    }
}
