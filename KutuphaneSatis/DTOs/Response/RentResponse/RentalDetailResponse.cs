using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.DTOs.Response.RentResponse
{
    public class RentalDetailResponse
    {
        public DateOnly RStartTime { get; set; }

        public DateOnly REndTime { get; set; }


        public decimal TotalPrice { get; set; }


        public List<RentItemResponse> RentItem { get; set; }



    }
}
