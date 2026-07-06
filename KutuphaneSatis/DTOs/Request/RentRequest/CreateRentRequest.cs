namespace KutuphaneSatis.DTOs.Request.RentRequest
{
    public class CreateRentRequest
    {
        public int UserId { get; set; }

        public List<RentItemRequest> RentBooks { get; set; }
    }
}
