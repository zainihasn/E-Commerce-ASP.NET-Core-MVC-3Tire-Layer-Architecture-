using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Service.Base;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_comm.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductViewComponent(IProductService productService)
        {
            _productService = productService;
        }
     
        public async Task<IViewComponentResult> InvokeAsync(int selected = 1)
        {
            var products = await _productService.GetAll(); 
            var selectList = new SelectList(products, "ProductId", "ProductName", selected); 
            return View("Default", selectList); 
        }
    }
}
