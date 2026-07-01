using KutuphaneSatis.DTOs.Request;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneSatis.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;



        public CategoryController(ICategoryService categoryService) 
        {
            _categoryService = categoryService;        
        
        }

        [HttpGet("ManageCategory")]
        public IActionResult ManageCategory()
        {
            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory([FromForm] CreateCategoryRequest request)
        {
            _categoryService.AddCategory(request);

            // DİKKAT: RedirectToAction değil, sadece Redirect kullanıyoruz!
            return Redirect("/api/Book/GetCatalog");
        }

        [HttpPost("RemoveCategory")]
        public IActionResult RemoveCategory([FromForm] int id)
        {
            _categoryService.RemoveCategory(id);

            // Yönlendirme adresini API yapına uygun şekilde tam yol olarak belirtelim
            return Redirect("/api/Category/ManageCategory");
        }




    }
}
