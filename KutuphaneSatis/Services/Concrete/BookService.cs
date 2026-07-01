using KutuphaneSatis.DTOs.Request;
using KutuphaneSatis.DTOs.Response;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Repositories.Concrete;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace KutuphaneSatis.Services.Concrete
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepistory;

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {

            _bookRepository = bookRepository;
            _categoryRepistory = categoryRepository;

        }
        public List<BookListItemResponse> GetBookList()
        {
            var books = _bookRepository.GetBooksWithCategories();

            var ResponseList = books.Select(book => new BookListItemResponse
            {
                BookId = book.Id,
                Name = book.Name,
                Price = book.Price,
                Stock = book.Stock,

                CatName = book.Category.Name
            }).ToList();

            return ResponseList;
        }
        public BookDetailResponse GetBookDetail(int id)
        {
            var bookEntity = _bookRepository.GetByID(id);

            if (bookEntity == null)
            {
                return null;

            }

            BookDetailResponse response = new BookDetailResponse()
            {
                Name = bookEntity.Name,
                Description = bookEntity.Description,
                AuthorName = bookEntity.AuthorName,
                Stock = bookEntity.Stock,
                PageNumber = bookEntity.PageNumber,
                Price = bookEntity.Price,
                CatName = bookEntity.Category.Name


            };
            return response;

        }

        public List<BookListItemResponse> GetCatalogByCategory(int categoryId)
        {

            var books = _bookRepository.GetBooksByCategory(categoryId);

            var ResponseList = books.Select(book => new BookListItemResponse
            {
                BookId = book.Id,
                Name = book.Name,
                Price = book.Price,
                Stock = book.Stock,

                CatName = book.Category.Name
            }).ToList();

            return ResponseList;
        }

        public void AddBook(CreateBookRequest createBook)
        {
            var category = _categoryRepistory.GetAll();

            Book book = new Book()
            {
                Name = createBook.Name,
                Description = createBook.Description,
                AuthorName = createBook.AuthorName,
                PageNumber = createBook.PageNumber,
                Price = createBook.Price,
                Stock = createBook.Stock,
                Category = category.FirstOrDefault(category => category.Name == createBook.CatName)
            };

            _bookRepository.Create(book);
        }
        public void RemoveBook(int id)
        {
            // Veritabanından bütün kitapları çekip isim eşleştirmesi yapmaya gerek kalmadı!
            _bookRepository.Delete(id);
        }


    }
}
