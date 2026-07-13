using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.Repositories.Abstract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public IEnumerable<Order> OSortedDate();

        public Order GetOrderByUserId(int id);

        public int ReturnOrderId(int Userid);

        public List<OrderBook> GetOrderDetails(int id);



    }
}
