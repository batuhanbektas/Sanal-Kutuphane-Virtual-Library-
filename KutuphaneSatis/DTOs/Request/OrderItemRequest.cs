using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Request
{
    public class OrderItemRequest
    {


        public int BookId { get; set; }

        public int Quantity { get; set; }
    }
}
