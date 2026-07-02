    using KutuphaneSatis.DTOs.Request;
    using KutuphaneSatis.Repositories.Abstract;
    using KutuphaneSatis.Services.Abstract;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Client;

namespace KutuphaneSatis.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BookController : Controller
        {

            private readonly IBookService _bookService;
            private readonly ICategoryService _categoryService;
            


            public BookController(IBookService bookService,ICategoryService categoryService)
            {
                _bookService = bookService;
                _categoryService= categoryService;
            }




        [HttpGet("GetCatalog")]
        // int? (soru işareti) bunun boş (null) gönderilebileceği anlamına gelir.
        public IActionResult GetCatalog([FromQuery] int? categoryId = null)
        {

            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = categories;

            // Eğer kimse kategori seçmemişse (categoryId null ise)
            if (categoryId == null)
            {
                var allBooks = _bookService.GetBookList();
                return View(allBooks); // Sayfaya tüm kitapları yolla
            }
            else // Eğer URL'den bir kategori ID'si gelmişse (örneğin Fantezi = 3)
            {
                var filteredBooks = _bookService.GetCatalogByCategory(categoryId.Value);
                return View(filteredBooks); // Sayfaya sadece fantezi kitaplarını yolla
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
            {
                var bookDetail = _bookService.GetBookDetail(id);

                if (bookDetail == null)
                {
                    return NotFound("Aradiginiz Kitap Bulanamadi.");
                 }
                else
                {
                return View(bookDetail);
            }
            
            }

            



        [HttpGet("AddBook")]
        public IActionResult AddBook()
            {
                var categories = _categoryService.GetAllCategories();

                ViewBag.Categories = categories;

                return View();
            }

        [HttpPost("AddBook")]
        public IActionResult AddBook([FromForm] CreateBookRequest createBookRequest)
            {
                if(createBookRequest.Stock != 0 && createBookRequest.PageNumber != 0 && createBookRequest.Price != 0)
                {
                    _bookService.AddBook(createBookRequest);
                    return RedirectToAction("GetCatalog");
                }
                else
                {
                    return BadRequest("Hatali Giris");
                }
                    
            
            }


        [HttpGet("RemoveBook")]
        public IActionResult RemoveBook()
        {
            var booklist = _bookService.GetBookList();

            return View(booklist);
        }

        [HttpPost("RemoveBook")]
        public IActionResult RemoveBook([FromForm] int bookId)
        {
            _bookService.RemoveBook(bookId);
            return RedirectToAction("GetCatalog");
        }


        

            
        
        
        }

    } 



    

