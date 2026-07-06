using KutuphaneSatis.DTOs.Request.CategoryRequest;
using KutuphaneSatis.DTOs.Response.CategoryResponse;

namespace KutuphaneSatis.Services.Abstract
{
    public interface ICategoryService
    {
        public List<CategoryListItemResponse> GetAllCategories();

        public void AddCategory(CreateCategoryRequest categoryRequest);

        public void RemoveCategory(int id);
    }
}
