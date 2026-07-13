using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Response.OrderResponse
{
    public class OrderItemResponse
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public string BookName { get; set; }

        public int orderitemid { get; set; }

        public int orderid { get; set; }

        public decimal UnitPrice { get; set; }

    }
}
