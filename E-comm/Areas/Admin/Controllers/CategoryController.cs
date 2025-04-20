using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace E_comm.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public CategoryController(ICategoryService CategoryService,IHostingEnvironment Host)
        {
            _CategoryService = CategoryService;
            _hostingEnvironment = Host;
        }


        public async Task <IActionResult> Index()
        {         
            return View(await _CategoryService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? CategoryId)
        {
            if (CategoryId != 0 && CategoryId != null)
            {
                var Category = await _CategoryService.GetById(CategoryId.Value);
                if (Category == null) return NotFound();
                return View(Category);
            }
            return View();
           
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int? CategoryId, CategoryDTO Category)
        {
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images");

            if (Category.ClientFile != null)
            {
                string fileName = Path.GetFileName(Category.ClientFile.FileName);
                string fullPath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await Category.ClientFile.CopyToAsync(stream);
                }
                Category.CategoryImageUrl = fileName;
                await _CategoryService.Update(Category);
            }
            else
            {

                if (Category.ClientFile != null)
                {
                    string fileName = Path.GetFileName(Category.ClientFile.FileName);
                    string fullPath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await Category.ClientFile.CopyToAsync(stream);
                    }
                    Category.CategoryImageUrl = fileName;                  
                    await _CategoryService.Add(Category);
                }
            }
            return RedirectToAction("Index");
        }
    
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? CategoryId)
        {
            if (CategoryId != 0 && CategoryId != null)
            {
                await _CategoryService.Delete(CategoryId.Value);
            }
           
            return RedirectToAction("Index");
        }
    }
}
