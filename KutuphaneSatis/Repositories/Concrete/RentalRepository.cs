using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;

namespace KutuphaneSatis.Repositories.Concrete
{
    public class RentalRepository : GenericRepository<Rental>, IRentalRepository
    {

        public RentalRepository(AppDbContext context) : base(context) { }


        public Rental GetRentByUserId(int id)
        {
            return _dbSet
                .Where(x => x.UserId == id)
                .FirstOrDefault();

        }
        public int ReturnRentId(int Userid)
        {
            return _dbSet
                .Where(x => x.UserId == Userid)
                .Select(x => x.Id)
                .FirstOrDefault();


        }

        public List<RentalBook> GetRentalDetails(int id)
        {
            return _dbSet
                .Where(x => x.Id == id)
                // Sadece silinMEMİŞ (isDeleted == false) olan detayları getir
                .SelectMany(x => x.RentalBook.Where(d => d.isDeleted == false))
                .ToList();
        }

        public IEnumerable<Rental> RSortedStartDate()
        {
            return _dbSet.OrderBy(x => x.RStartTime).ToList();
        }


        public IEnumerable<Rental> OSortedDate()
        {
            return _dbSet.OrderBy(x => x.RStartTime).ToList();
        }

        IEnumerable<Rental> IRentalRepository.RSortedEndDate()
        {
            return _dbSet.OrderBy(x => x.REndTime ).ToList();
        }
    }
}

