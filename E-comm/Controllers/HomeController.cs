using System.Diagnostics;
using E_comm.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Service.Base;
using E_comm.ViewModels;

namespace E_comm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IProductService _productService;
      

        public HomeController(ICategoryService CategoryService, IProductService ProductService)
        {
            _CategoryService = CategoryService;
            _productService = ProductService;
      
        }


       


        public async Task<IActionResult> Index(string? Search)
        {               
            var viewModel = new CategoryProductViewModelHome();
            if (!string.IsNullOrEmpty(Search))
            {
                var products = await _productService.Search(Search);
                var searchResults = new List<ProductWithImagesViewModelHome>();

                foreach (var product in products)
                {
                    var images = await _productService.GetOneProductImageDTOById(product.ProductId);
                    searchResults.Add(new ProductWithImagesViewModelHome
                    {
                        Product = product,
                        Images = images
                    });
                }
                viewModel.SearchResults = searchResults;
                viewModel.SearchTerm = Search;
                return View(viewModel);
            }
            else
            {
                var allCategories = await _CategoryService.GetAll();
                var topSellingProducts = await _productService.GetTopSellingProducts(4);

                var topSellingWithImages = new List<ProductWithImagesViewModelHome>();

                foreach (var product in topSellingProducts)
                {
                    var images = await _productService.GetOneProductImageDTOById(product.ProductId);
                    topSellingWithImages.Add(new ProductWithImagesViewModelHome
                    {
                        Product = product,
                        Images = images
                    });
                }

                viewModel.Categories = allCategories.ToList();
                viewModel.TopSellingProducts = topSellingWithImages;

                return View(viewModel);
            }
        }

        

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
