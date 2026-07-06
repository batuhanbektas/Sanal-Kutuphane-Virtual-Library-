using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Request.OrderRequest
{
    public class CreateOrderRequest
    {



        public int UserId { get; set; }

        public List<OrderItemRequest> OrderBooks { get; set; }

    }
}
