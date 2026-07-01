using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Response
{
    public class OrderDetailResponse
    {
        public DateOnly OrderTime { get; set; }


        public decimal TotalPrice { get; set; }


        public List<OrderItemResponse> OrderItem    { get; set; }
        


    }
}
