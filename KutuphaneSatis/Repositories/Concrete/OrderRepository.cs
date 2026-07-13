using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;

namespace KutuphaneSatis.Repositories.Concrete
{
    public class OrderRepository : GenericRepository <Order> , IOrderRepository
    {

         public OrderRepository(AppDbContext context) : base(context) { }


        public Order GetOrderByUserId(int id)
        {
            return _dbSet
                .Where(x => x.UserId == id)
                .FirstOrDefault();

        }
        public int ReturnOrderId(int Userid)
        {
            return _dbSet
                .Where(x => x.UserId == Userid)
                .Select(x => x.Id)
                .FirstOrDefault();


        }

        public List<OrderBook> GetOrderDetails(int id)
        {
            return _dbSet
                .Where(x => x.Id == id)
                // Sadece silinMEMİŞ (isDeleted == false) olan detayları getir
                .SelectMany(x => x.OrderBook.Where(d => d.isDeleted == false))
                .ToList();
        }

        


        public IEnumerable<Order> OSortedDate()
        {
            return _dbSet.OrderBy(x => x.OrderTime).ToList();
        }
    }
}
