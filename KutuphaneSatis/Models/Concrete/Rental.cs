using KutuphaneSatis.Models.Abstract;

namespace KutuphaneSatis.Models.Concrete
{
    public class Rental : BaseEntities
    {

        public DateOnly RStartTime { get; set; }

        public DateOnly REndTime { get; set; }


        public User User { get; set; }
        public int UserId { get; set; }

       

        public ICollection<RentalBook> RentalBook { get; set; }


    }
}
