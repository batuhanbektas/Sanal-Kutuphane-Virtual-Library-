using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Request.OrderRequest
{
    public class OrderItemRequest
    {


        public int BookId { get; set; }

        public int Quantity { get; set; }
    }
}
