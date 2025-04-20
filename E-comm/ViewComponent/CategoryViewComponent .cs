using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Service.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace E_comm.ViewComponents
{
    
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selected = 1)
        {
            var categories = await _categoryService.GetAll();
            var selectList = new SelectList(categories, "CategoryId", "CategoryName", selected);
            return View("Default", selectList); 
        }
    }

}

