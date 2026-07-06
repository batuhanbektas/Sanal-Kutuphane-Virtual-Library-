using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Response.RentResponse
{
    public class RentItemResponse
    {


        public int BookId { get; set; }

        public int RentalDurationDays { get; set; }

        public int ReturnedQuantitiy { get; set; } = 0;

        public int RentalQuantity { get; set; } = 0;

        public double UnitPrice { get; set; }
    }
}
