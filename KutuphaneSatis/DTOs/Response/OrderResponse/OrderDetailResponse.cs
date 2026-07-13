using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Response.OrderResponse
{
    public class OrderDetailResponse
    {
        public DateOnly OrderTime { get; set; }

        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }


        public List<OrderItemResponse> OrderItem    { get; set; }
        


    }
}
