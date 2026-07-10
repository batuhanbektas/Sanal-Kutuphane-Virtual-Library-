using KutuphaneSatis.Models.Abstract;

namespace KutuphaneSatis.Models.Concrete
{
    public class CartDetail : BaseEntities
    {

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public string BookName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
