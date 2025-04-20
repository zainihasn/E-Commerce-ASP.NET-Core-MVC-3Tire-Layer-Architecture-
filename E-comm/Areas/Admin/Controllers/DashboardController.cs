using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.Base;
using BusinessLogicLayer.Service.Base;
using E_comm.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_comm.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IProductService _productService;

        public DashboardController(IDashboardService dashboardService,IProductService productService)
        {
            _dashboardService = dashboardService;
            _productService = productService;
            
        }
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {                   
            if (!startDate.HasValue || !endDate.HasValue)
            {
                endDate = DateTime.Today;
                startDate = endDate.Value.AddDays(-6);
            
            }
            var topSellingWithImages = new List<ProductWithImagesViewModelHome>();
            var LowStockProduct = new List<ProductWithImagesViewModelHome>();
            var data = await _dashboardService.GetCustomRangeStatsAsync(startDate.Value, endDate.Value);
            var lowStockProducts = await _productService.GetLowStockProductsAsync(4);
            var Top5Products = await _productService.GetTopSellingProducts(4);
            foreach (var product in Top5Products)
            {
                var images = await _productService.GetOneProductImageDTOById(product.ProductId);
                topSellingWithImages.Add(new ProductWithImagesViewModelHome
                {
                    Product = product,
                    Images = images
                });
            }
            foreach (var product in lowStockProducts)
            {
                var images = await _productService.GetOneProductImageDTOById(product.ProductId);
                LowStockProduct.Add(new ProductWithImagesViewModelHome
                {
                    Product = product,
                    Images = images
                });
            }
            var model = new DashboardFilterViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                DashboardData = data,
                LowStockProduct= LowStockProduct,
                TopSellingProducts=topSellingWithImages
                
            };
            return View(model);
        }
    }
}
