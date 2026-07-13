using KutuphaneSatis.DTOs.Response.RentResponse;

namespace KutuphaneSatis.Services.Abstract
{
    public interface IRentalService
    {
        public List<RentalHistoryResponse> GetRental();

        public void CreateRent(int userid);

        public RentalDetailResponse GetRentDetails(int id);


    }
}
