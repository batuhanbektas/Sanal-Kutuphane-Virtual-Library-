using KutuphaneSatis.DTOs.Request.CategoryRequest;
using KutuphaneSatis.DTOs.Response.CategoryResponse;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Repositories.Concrete;
using KutuphaneSatis.Services.Abstract;

namespace KutuphaneSatis.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {

            _categoryRepository = categoryRepository;

        }
        public List<CategoryListItemResponse> GetAllCategories()
        {
            // Parametre ismini 'category' olarak değiştirmek okunabilirliği artırır
            var categories = _categoryRepository.GetAll()
                                                .Where(category => !category.isDeleted) ;

            return categories.Select(c => new CategoryListItemResponse
            {
                Name = c.Name,
                Id = c.Id,
                Description = c.Description,
                
            }).ToList();
        }

        public void AddCategory(CreateCategoryRequest categoryRequest)
        {
            Category category = new Category()
            {
                Name = categoryRequest.CatName,
                Description = categoryRequest.Description


            };

            _categoryRepository.Create(category);

        }

        public void RemoveCategory(int id)
        {
            // Veritabanından bütün kitapları çekip isim eşleştirmesi yapmaya gerek kalmadı!
            _categoryRepository.Delete(id);
        }
    }
}
