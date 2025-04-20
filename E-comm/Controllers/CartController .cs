using System.Diagnostics;
using E_comm.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Service.Base;
using BusinessLogicLayer.DTOs;
using E_comm.ViewModels;
using Microsoft.AspNetCore.Identity;
using BusinessLogicLayer.MainServices.Base;

namespace E_comm.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;
        private readonly IProductService _ProductService;
        private readonly IGuestService _GuestService;
        private readonly ICheckoutServices _checkoutServices;
        private readonly UserManager<IdentityUser> _userManager;


        public CartController(ICartService CartService, IProductService ProductService, IGuestService GuestService
               , UserManager<IdentityUser> userManager,
                ICheckoutServices checkoutServices)
        {
            _checkoutServices = checkoutServices;
            _CartService = CartService;
            _ProductService = ProductService;
            _GuestService = GuestService;
            _userManager = userManager;

        }


        public async Task<IActionResult> Index()
        {

            int guestId = await _GuestService.EnsureGuestIdInSessionAsync(HttpContext);
            var cart = await _CartService.GetCartAsync();
            return View(cart);


        }


        public async Task<IActionResult> AddToCart(int ProductId, int quantity = 1)
        {
            var product = await _ProductService.GetById(ProductId);
            if (product == null)
            {
                return View("Message");
            }
            if (product.IsActive == false) return View("Message");
            await _CartService.AddToCartAsync(product, quantity);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteFromCart(int? ProductId)
        {
            if (ProductId == null || ProductId == 0) return NotFound();
            await _CartService.Delete(ProductId.Value);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditPlus(int? ProductId)
        {
            if (ProductId == null || ProductId == 0) return NotFound();
            await _CartService.EditPlus(ProductId.Value);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> EditMins(int? ProductId)
        {
            if (ProductId == null || ProductId == 0) return NotFound();
            await _CartService.EditMins(ProductId.Value);
            return RedirectToAction("Index");
        }


        public IActionResult Clear()
        {
            _CartService.ClearCart();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CheckOut ()
        {
            var cart = await _CartService.GetCartAsync();
            decimal TotalAmount = cart.Sum(item => item.Amount);
            var viewModel = new ProductDetailsViewModelCartItems
            {
                PaymentDTO = new PaymentDTO(),
                GuestInfoDTO = new GuestInfoDTO(),
                TotalAmount = TotalAmount

            };         
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(ProductDetailsViewModelCartItems model, string cardToken)
        {
            foreach (var key in ModelState.Keys.ToList())
            {
                if (!key.StartsWith("GuestInfoDTO"))
                {
                    ModelState.Remove(key);
                }
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var result = await _checkoutServices.ProcessCheckoutAsync(model.PaymentDTO, model.GuestInfoDTO, User, HttpContext, cardToken);
                if (!result.IsSuccess)
                {
                    return View("MessageError");
                }
                _CartService.ClearCart();
                return View("Successful", model: result.InvoiceHtml);
            }
            return View();
        }


        public async Task<IActionResult> Successful()
        {
            var CartItem =await  _CartService.GetCartAsync();
             _CartService.ClearCart();
            return View(CartItem);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
