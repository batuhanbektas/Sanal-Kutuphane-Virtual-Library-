using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.Repositories.Abstract
{
    public interface IBookRepository : IGenericRepository<Book>
    {


        public IEnumerable<Book> GetBooksWithCategories();

        public List<Book> GetBooksByCategory(int categoryId);




    }
}
