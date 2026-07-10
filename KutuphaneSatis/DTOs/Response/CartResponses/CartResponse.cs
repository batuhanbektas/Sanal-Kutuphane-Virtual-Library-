using KutuphaneSatis.DTOs.Request;
using KutuphaneSatis.DTOs.Response.CartResponse.CartResponse;

using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Response.CartResponses
{
    public class CartResponse
    {
        public int UserId { get; set; }

        public List<CartItemResponse> CartItems { get; set; }

        public decimal TotalPrice { get; set; }




    }
}
