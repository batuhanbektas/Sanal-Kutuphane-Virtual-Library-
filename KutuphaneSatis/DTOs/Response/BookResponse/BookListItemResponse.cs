namespace KutuphaneSatis.DTOs.Response.BookResponse
{
    public class BookListItemResponse
    {
        public int BookId { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
        
        public string CatName { get; set; }
        

    }
}
