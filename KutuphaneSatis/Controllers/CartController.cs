using KutuphaneSatis.DTOs.Request.CartRequest;
using KutuphaneSatis.DTOs.Request.OrderRequest;
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
        private readonly IOrderService _orderService;
        private readonly IRentalService _rentService;



        public CartController(ICartService cartService,IUserLRService userservice, IBookService bookService, IOrderService orderService,IRentalService rentService)
        {
            _cartService = cartService;
            _userservice = userservice;
            _bookService = bookService;
            _orderService = orderService;
            _rentService = rentService;
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

        public IActionResult RemoveItem(int cartDetailId) 
        {
            _cartService.RemoveItemFromCart(cartDetailId);
            return RedirectToAction("GetCart");

        }


        [HttpPost]
        public IActionResult ClearCart(int cartId)
        {
            _cartService.ClearCart(cartId);
            return RedirectToAction("GetCart");
        }


        [HttpPost]
        public IActionResult CreateOrder()
        {

            var useridString = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userid = int.Parse(useridString);


            _orderService.CreateOrder(userid);

            return RedirectToAction("GetOrders", "Order");


        }

        [HttpPost]
        public IActionResult CreateRent()
        {

            var useridString = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userid = int.Parse(useridString);

            _rentService.CreateRent(userid);


            return RedirectToAction("GetRents", "Rent");


        }



    }
}
