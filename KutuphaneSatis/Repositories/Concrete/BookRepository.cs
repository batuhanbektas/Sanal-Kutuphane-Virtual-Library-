using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneSatis.Repositories.Concrete
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {



        public BookRepository(AppDbContext context) : base(context) { }

        // Repository içindeki metodun şu şekilde görünmeli:
        public List<Book> GetBooksByCategory(int categoryId)
        {
            return _dbSet
                .Include(b => b.Category) // Kategori verisini de beraberinde getir!
                .Where(b => b.CategoryID == categoryId)
                .ToList();
        }



        public IEnumerable<Book> GetBooksWithCategories()
        {
            return _dbSet.Include(x => x.Category).ToList();
        }
    }
}
