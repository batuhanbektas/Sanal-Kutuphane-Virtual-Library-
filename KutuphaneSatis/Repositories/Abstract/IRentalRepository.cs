using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.Repositories.Abstract
{
    public interface IRentalRepository : IGenericRepository<Rental>
    {
        public Rental GetRentByUserId(int id);

        public int ReturnRentId(int Userid);

        public List<RentalBook> GetRentalDetails(int id);


        public IEnumerable<Rental> RSortedEndDate();

        public IEnumerable<Rental> RSortedStartDate();


    }
}
