using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.Base;
using E_comm.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_comm.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
      
        private readonly IProductService _ProductService;
        private readonly ICategoryService _CategoryService;
        public HomeController(IProductService ProductService,ICategoryService CategoryService) 
        {
            _ProductService = ProductService;
            _CategoryService = CategoryService;      
        }

      
        public async Task<IActionResult> Index()
        {
            var products = await _ProductService.GetAll();
            var viewModels = new List<ProductDetailsViewModelHomeAdmin>();
            foreach (var product in products)
            {
                var images = await _ProductService.GetOneProductImageDTOById(product.ProductId);
                var Category = await _CategoryService.GetById(product.CategoryId);
                viewModels.Add(new ProductDetailsViewModelHomeAdmin
                {
                    Product = product,
                    imageDTO = images,
                    Category = Category
                });
            }
            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int?ProductId)
        {
          
            if (ProductId != 0 && ProductId != null)
            {
                var Product = await _ProductService.GetById(ProductId.Value);
                if (Product == null) return NotFound();
                return View(Product);
            }
            return View();
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int? ProductId, ProductDTO product)
        {
            foreach (var key in ModelState.Keys.ToList())
            {
                if (!key.StartsWith("ProductDTO"))
                {
                    ModelState.Remove(key);
                }
            }
            if (ModelState.IsValid)
            {
                if (ProductId != 0 && ProductId != null)
                {
                    await _ProductService.Update(product);
                }
                else await _ProductService.Add(product);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
    
}
