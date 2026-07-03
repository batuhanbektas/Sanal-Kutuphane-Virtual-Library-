namespace KutuphaneSatis.DTOs.Request
{
    public class CreateCartRequest
    {

        public int UserId { get; set; }

        public List<CartItemRequest> CartItems { get; set; }

        public decimal TotalPrice { get; set; }

        public int CartId { get; set; }
    }
}
