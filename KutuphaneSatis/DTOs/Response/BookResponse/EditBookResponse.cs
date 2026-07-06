namespace KutuphaneSatis.DTOs.Response.BookResponse
{
    public class EditBookResponse
    {

        public string Name { get; set; }

        public string AuthorName { get; set; }
        public string? Description { get; set; }

        public int PageNumber { get; set; } = 0;

        public int Stock { get; set; } = 0;

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public int ID { get; set; }

    }
}
