using KutuphaneSatis.DTOs.Request.BookRequest;
using KutuphaneSatis.DTOs.Response.BookResponse;

namespace KutuphaneSatis.Services.Abstract
{
    public interface IBookService
    {

        void AddBook(CreateBookRequest createBook);

        public List<BookListItemResponse> GetBookList();
        
        public BookDetailResponse GetBookDetail(int id);
        public void RemoveBook(int id);

        public  List<BookListItemResponse> GetCatalogByCategory(int  categoryId);

        public void EditBook(EditBookRequest editBook);
    }
}
