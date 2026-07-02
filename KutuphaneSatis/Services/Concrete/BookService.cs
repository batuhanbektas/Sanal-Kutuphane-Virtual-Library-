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
        private readonly ICategoryRepository _categoryRepository;

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {

            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;

        }
        public List<BookListItemResponse> GetBookList()
        {
            var books = _bookRepository.GetBooksWithCategories();

            var ResponseList = books
                .Where(book => !book.isDeleted)
                .Select(book => new BookListItemResponse
            {
                BookId = book.Id,
                Name = book.Name,
                Price = book.Price,
                Stock = book.Stock,


                    // "Eğer book.Category null değilse adını al, null ise boşluk döndür"
                CatName = (!book.Category.isDeleted) ? book.Category.Name : " "
                


                }).ToList();

            return ResponseList;
        }
        public BookDetailResponse GetBookDetail(int id)
        {
            // "Bana ID'si şu olan kitabı getir, gelirken yanına Category nesnesini de (Include) alıp gelsin."
            var bookEntity = _bookRepository.GetByID(id, b => b.Category);


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
                // "Eğer book.Category null değilse adını al, null ise boşluk döndür"
                CatName = (!bookEntity.Category.isDeleted) ? bookEntity.Category.Name : " "


            };
            return response;

        }

        public List<BookListItemResponse> GetCatalogByCategory(int categoryId)
        {

            var books = _bookRepository.GetBooksByCategory(categoryId);

            var ResponseList = books
                .Where(book => !book.isDeleted)
                .Select(book => new BookListItemResponse
            {
                BookId = book.Id,
                Name = book.Name,
                Price = book.Price,
                Stock = book.Stock,

                 CatName = (!book.Category.isDeleted) ? book.Category.Name : " "
                }).ToList();

            return ResponseList;
        }

        public void AddBook(CreateBookRequest createBook)
        {

            var category = _categoryRepository.GetAll().FirstOrDefault(c => c.Id == createBook.CategoryId);

            if (category == null)
            {
                throw new Exception("Belirtilen kategori bulunamadı veya silinmiş.");
            }

            Book book = new Book()
            {
                Name = createBook.Name,
                Description = createBook.Description,
                AuthorName = createBook.AuthorName,
                PageNumber = createBook.PageNumber,
                Price = createBook.Price,
                Stock = createBook.Stock,
                Category = category, // Bulduğumuz geçerli kategoriyi atıyoruz
                CategoryID = category.Id // İlişkisel veritabanı kuralları gereği ID'yi de eşle
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
