using KutuphaneSatis.DTOs.Request.BookRequest;
using KutuphaneSatis.DTOs.Response.OrderResponse;
using KutuphaneSatis.DTOs.Request.OrderRequest;
using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace KutuphaneSatis.Controllers
{

    
    public class OrderController : Controller
    {
            private readonly IOrderService _orderService;
            private readonly IBookService _bookService;
            private readonly ICartService _cartService;



            public OrderController(IBookService bookService, IOrderService orderService, ICartService cartService)
            {
                _bookService = bookService;
                _orderService = orderService;
                _cartService = cartService;
            }


        [HttpGet]
        public IActionResult GetOrders() 
        {
            var orders = _orderService.GetOrders();

            return View(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetails(int id)
        {
            var orderdetails = _orderService.GetOrderDetails(id);
            return View(orderdetails);
        }










        
    }
}
