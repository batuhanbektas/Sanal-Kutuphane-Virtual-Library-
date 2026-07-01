using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Response
{
    public class RentalHistoryResponse
    {
        public DateOnly RStartTime { get; set; }

        public DateOnly REndTime { get; set; }


        public decimal TotalPrice { get; set; }

        public int RentalId { get; set; }

        public int UserId { get; set; }

    }
}
