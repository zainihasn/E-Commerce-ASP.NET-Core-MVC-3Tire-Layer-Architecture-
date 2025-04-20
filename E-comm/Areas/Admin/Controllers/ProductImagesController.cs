using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service;
using BusinessLogicLayer.Service.Base;
using E_comm.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static E_comm.Controllers.HomeController;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace E_comm.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class ProductImagesController : Controller
        {
            private readonly IProductService _ProductService;
            private readonly IHostingEnvironment _hostingEnvironment;
            public ProductImagesController(IProductService ProductService, IHostingEnvironment HostingEnvironment)
            {
                _ProductService = ProductService;
                _hostingEnvironment = HostingEnvironment;
        }

        public async Task ProductList(int selected = 1)
        {
            var Product = await _ProductService.GetAll();
            SelectList listItems = new SelectList(Product, "ProductId", "ProductName", selected);
            ViewBag.ProductList = listItems;
        }


       


            public async Task<IActionResult> Index()
            {
            var viewModel = new List<ProductImagesDetailsViewModelAdminProductImages>();
            var ProductImages = await _ProductService.GetAllProductImagesAsync();
            foreach(var item in ProductImages)
            {
                var Product = await _ProductService.GetById(item.ProductId);
                viewModel.Add(new ProductImagesDetailsViewModelAdminProductImages
                {
                    Product= Product,
                    imageDTO=item
                });
            }
                return View(viewModel);
            }







            [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? ImageId)
        {
            
            var productList = await _ProductService.GetAll();
            await ProductList();

            if (ImageId != null && ImageId != 0)
            {
                var productImages = await _ProductService.GetAllProductImagesAsync();
                var image = productImages.FirstOrDefault(x => x.ImageId == ImageId);
                if (image != null)
                {
                    var product = await _ProductService.GetById(image.ProductId);
                    var viewModel = new ProductImagesDetailsViewModelAdminProductImages
                    {
                        Product = product,
                        imageDTO = image
                    };

                    return View(viewModel);
                }
            }
            return View(new ProductImagesDetailsViewModelAdminProductImages());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int? ImageId, ProductImagesDetailsViewModelAdminProductImages ProductImage)
        {
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
            if (ImageId != null && ImageId != 0)
            {
                if (ProductImage.imageDTO.ClientFile != null)
                {
                    string fileName = Path.GetFileName(ProductImage.imageDTO.ClientFile.FileName);
                    string fullPath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await ProductImage.imageDTO.ClientFile.CopyToAsync(stream);
                    }
                    ProductImage.imageDTO.ImageUrl = fileName;
                    await _ProductService.UpdateProductImageDTO(ProductImage.imageDTO);
                }
            }
            else
            {
                if (ProductImage.imageDTO.ClientFile != null)
                {
                    string fileName = Path.GetFileName(ProductImage.imageDTO.ClientFile.FileName);
                    string fullPath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await ProductImage.imageDTO.ClientFile.CopyToAsync(stream);
                    }
                    ProductImage.imageDTO.ImageUrl = fileName;
                    await _ProductService.AddProductImageDTO(ProductImage.imageDTO);
                }
            }
       
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? ImageId)
        {
            if (ImageId != 0 && ImageId != null)
                {
                    await _ProductService.DeleteProductImageDTO(ImageId.Value);
                }
                 return RedirectToAction("Index");
            }
        }
    }

