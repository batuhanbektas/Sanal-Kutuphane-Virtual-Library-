using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;

namespace KutuphaneSatis.Repositories.Concrete
{
    public class RentalRepository : GenericRepository<Rental> , IRentalRepository
    {

        public RentalRepository(AppDbContext context) : base(context)  { }

        public IEnumerable<Rental> RSortedEndDate()
        {
            return _dbSet.OrderBy(x => x.REndTime).ToList();
        }

        public IEnumerable<Rental> RSortedStartDate()
        {
            return _dbSet.OrderBy(x => x.RStartTime).ToList();
        }



    }
}
