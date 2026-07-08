namespace KutuphaneSatis.DTOs.Request.CartRequest
{
    public class CartItemRequest
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int CartId { get; set; }

    }
}
