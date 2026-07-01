using KutuphaneSatis.DTOs.Request;
using KutuphaneSatis.DTOs.Response;

namespace KutuphaneSatis.Services.Abstract
{
    public interface ICategoryService
    {
        public List<CategoryListItemResponse> GetAllCategories();

        public void AddCategory(CreateCategoryRequest categoryRequest);

        public void RemoveCategory(int id);
    }
}
