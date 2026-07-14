    using KutuphaneSatis.Models.Abstract;

namespace KutuphaneSatis.Models.Concrete
{
    public class RentalBook : BaseEntities
    {



        public int BookId { get; set; }

        public Book Book { get; set; }

        public int RentalId { get; set; }

        public Rental Rental { get; set; }

        public int RentalDurationDays { get; set; }

        public int ReturnedQuantitiy { get; set; } = 0;

        public int RentalQuantity    { get; set; } = 0;

        public decimal UnitPrice { get; set; }

        public string BookName  { get; set; }

    }
}
