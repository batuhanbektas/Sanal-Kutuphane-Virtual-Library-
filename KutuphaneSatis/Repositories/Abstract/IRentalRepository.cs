using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.Repositories.Abstract
{
    public interface IRentalRepository : IGenericRepository<Rental>
    {

        public IEnumerable<Rental> RSortedEndDate();

        public IEnumerable<Rental> RSortedStartDate();


    }
}
