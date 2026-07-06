using KutuphaneSatis.DTOs.Response;
using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Request.BookRequest
{
    public class CreateBookRequest
    {


        public string Name { get; set; }

        public string AuthorName { get; set; }
        public string? Description { get; set; }

        public int PageNumber { get; set; } = 0;

        public int Stock { get; set; } = 0;

        public decimal Price { get; set; }

        public int CategoryId { get; set; }


    }
}
