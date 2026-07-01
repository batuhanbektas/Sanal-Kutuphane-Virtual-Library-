using KutuphaneSatis.Models.Abstract;

namespace KutuphaneSatis.Models.Concrete
{
    public class Order : BaseEntities
    {
        public DateOnly OrderTime { get; set; }

        public ICollection<OrderBook> OrderBook { get; set; }

        public Order()
        {
            OrderBook = new List<OrderBook>();
        }
        
        public decimal TotalPrice { get; set; }


        public int UserId { get; set; }

        public User User { get; set; }

    }
}
