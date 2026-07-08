namespace KutuphaneSatis.DTOs.Response.CartResponse.CartResponse
{
    public class CartItemResponse
    {

        public int BookId { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int CartId { get; set; }


    }
}
