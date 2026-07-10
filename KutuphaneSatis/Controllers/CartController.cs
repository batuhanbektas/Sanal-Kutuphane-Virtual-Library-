using KutuphaneSatis.DTOs.Request.CartRequest;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KutuphaneSatis.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        private readonly ICartService _cartService;
        private readonly IUserLRService _userservice;
        private readonly IBookService _bookService;



        public CartController(ICartService cartService,IUserLRService userservice, IBookService bookService)
        {
            _cartService = cartService;
            _userservice = userservice;
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var useridString = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userid = int.Parse(useridString);
            var user = _userservice.GetUserById(userid);

            var cartid = user.CartId;

            var cart = _cartService.GetCart(cartid);

            return View (cart);

        }


        [HttpPost]
        public IActionResult AddItemCart(int bookId, int quantity = 1)
        {
            var book = _bookService.GetBook(bookId);

            if (book != null && book.Stock>0)
            {
                var useridString = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var userid = int.Parse(useridString);

                var user = _userservice.GetUserById(userid);


                if (user != null)
                {
                    CartItemRequest cartitem = new CartItemRequest()
                    {
                        BookId = bookId,
                        CartId = user.CartId,
                        Quantity = quantity,
                        UnitPrice = book.Price,
                        BookName = book.Name

                    };

                    _cartService.CreateDetailandAdd(cartitem);

                    return RedirectToAction("GetCart");
                }
                else
                {
                    ModelState.AddModelError("", "Boyle Bir Kullanici yok");
                    return View();

                }
            }
            else
            {
                ModelState.AddModelError("", "Boyle bir kitap yok veya stokta kalmamis");
                return View();
            }
        }


        

        [HttpPost]
        public IActionResult UpdateQuantity(int cartDetailId, int newQuantity)
        {
            _cartService.UpdateItemQuantity(cartDetailId, newQuantity);
            return RedirectToAction("GetCart"); // Aynı sayfaya (sepete) geri dön
        }

        [HttpPost]
        public IActionResult ClearCart(int cartId)
        {
            _cartService.ClearCart(cartId);
            return RedirectToAction("GetCart");
        }
    }
}
