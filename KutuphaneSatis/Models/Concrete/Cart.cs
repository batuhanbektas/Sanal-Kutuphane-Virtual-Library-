using KutuphaneSatis.Models.Abstract;

namespace KutuphaneSatis.Models.Concrete
{
    public class Cart : BaseEntities
    {


        public ICollection<CartDetail> CartDetail { get; set; }

       
        public Cart()
        {
            CartDetail = new List<CartDetail>();
        }

        public decimal TotalPrice { get; set; }




        public int UserId { get; set; }

        public User User { get; set; }


    }
}
