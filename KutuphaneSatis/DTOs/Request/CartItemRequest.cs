namespace KutuphaneSatis.DTOs.Request
{
    public class CartItemRequest
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public double UnitPrice { get; set; }


    }
}
