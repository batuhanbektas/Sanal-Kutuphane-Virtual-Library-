using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.Repositories.Abstract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public IEnumerable<Order> OSortedDate();


    }
}
