using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;

namespace KutuphaneSatis.Repositories.Concrete
{
    public class OrderRepository : GenericRepository <Order> , IOrderRepository
    {

         public OrderRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Order> OSortedDate()
        {
            return _dbSet.OrderBy(x => x.OrderTime).ToList();
        }
    }
}
