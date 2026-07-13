using KutuphaneSatis.Models.Abstract;

namespace KutuphaneSatis.Models.Concrete
{
    public class OrderBook : BaseEntities
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public string BookName { get; set; }


        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }



    }
}
