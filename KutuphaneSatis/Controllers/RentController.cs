using KutuphaneSatis.DTOs.Request.BookRequest;
using KutuphaneSatis.DTOs.Response.OrderResponse;
using KutuphaneSatis.DTOs.Request.OrderRequest;
using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace KutuphaneSatis.Controllers
{


    public class RentController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;
        private readonly ICartService _cartService;
        private readonly IRentalService _rentalService;



        public RentController(IBookService bookService, IOrderService orderService, ICartService cartService, IRentalService rentalService)
        {
            _bookService = bookService;
            _orderService = orderService;
            _cartService = cartService;
            _rentalService = rentalService;
        }


        [HttpGet]
        public IActionResult GetRents()
        {
            var rents = _rentalService.GetRental();

            return View(rents);
        }

        [HttpGet("{id}")]
        public IActionResult GetRentalDetails(int id)
        {
            var rentdetails = _rentalService.GetRentDetails(id);
            return View(rentdetails);
        }











    }
}
