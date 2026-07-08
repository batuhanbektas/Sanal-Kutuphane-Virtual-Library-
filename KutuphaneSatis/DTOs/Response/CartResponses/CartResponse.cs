using KutuphaneSatis.DTOs.Request;
using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Response.CartResponses
{
    public class CartResponse
    {
        public int UserId { get; set; }

        public List<CartDetail> CartItems { get; set; }

        public decimal TotalPrice { get; set; }




    }
}
