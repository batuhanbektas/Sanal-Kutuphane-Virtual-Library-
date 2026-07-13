using KutuphaneSatis.DTOs.Response.OrderResponse;

namespace KutuphaneSatis.Services.Abstract
{
    public interface IOrderService
    {

        public void CreateOrder(int userid);

        public OrderDetailResponse GetOrderDetails(int id);

        public List<OrderHistoryResponse> GetOrders();
    }
}
