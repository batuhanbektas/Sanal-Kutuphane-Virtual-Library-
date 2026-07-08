using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KutuphaneSatis.Controllers
{
    public class CartController : Controller
    {

        private readonly ICartService _cartService;



        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCart()
        {
           var cart = User.FindFirst(cart)
        }




        [HttpPost]
        public IActionResult UpdateQuantity(int cartDetailId, int newQuantity)
        {
            _cartService.UpdateItemQuantity(cartDetailId, newQuantity);
            return RedirectToAction("Index"); // Aynı sayfaya (sepete) geri dön
        }

        [HttpPost]
        public IActionResult ClearCart(int cartId)
        {
            _cartService.ClearCart(cartId);
            return RedirectToAction("Index");
        }
    }
}
