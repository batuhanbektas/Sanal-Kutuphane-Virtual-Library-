using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetRentalDetails(int id)
        {
            var rentdetails = _rentalService.GetRentDetails(id);
            return View(rentdetails);
        }

        [HttpPost]
        public IActionResult ReturnRents(int rentBookId, int quantity)
        {
            _rentalService.ReturnRent(rentBookId, quantity);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult DeleteRent(int rentId)
        {
            _rentalService.DeleteRent(rentId);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}