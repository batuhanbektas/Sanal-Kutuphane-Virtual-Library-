using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneSatis.Repositories.Concrete
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {


        public CategoryRepository(AppDbContext context) : base (context){ }

       
    }
}
