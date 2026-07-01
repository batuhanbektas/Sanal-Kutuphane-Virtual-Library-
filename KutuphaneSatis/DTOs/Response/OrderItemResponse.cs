using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Response
{
    public class OrderItemResponse
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

    }
}
