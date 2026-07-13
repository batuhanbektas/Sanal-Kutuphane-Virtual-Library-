using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Data;

namespace KutuphaneSatis.Repositories.Concrete
{
    public class OrderBookRepository : GenericRepository<OrderBook>, IOrderBookRepository
    {


        public OrderBookRepository(AppDbContext context) : base(context) { }








    }
}
