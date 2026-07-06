using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneSatis.Repositories.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {


        public UserRepository (AppDbContext context) : base (context){}

        public User GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(x => x.Email == email);
        }
        
        public bool IsThereUserWithEmail(string email)
        {
            return _context.User.Any(u => u.Email == email);
        }
        public User GetUserWithOrders(int userId)
        {
            return _dbSet.Include(x => x.Orders).FirstOrDefault(x => x.Id == userId);
        }

        public User GetUserWithRentals(int userId)
        {
            return _dbSet.Include(x => x.Rents).FirstOrDefault(x => x.Id == userId);
        }

        public string GetPassword(int userId) 
        {
            return _dbSet.Where(x => x.Id == userId).Select(x=>x.Password).FirstOrDefault();
            
        }
    }
}
