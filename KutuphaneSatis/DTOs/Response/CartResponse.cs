using KutuphaneSatis.DTOs.Request;

namespace KutuphaneSatis.DTOs.Response
{
    public class CartResponse
    {
        public int UserId { get; set; }

        public List<CartItemRequest> CartItems { get; set; }

        public decimal TotalPrice { get; set; }


    }
}
