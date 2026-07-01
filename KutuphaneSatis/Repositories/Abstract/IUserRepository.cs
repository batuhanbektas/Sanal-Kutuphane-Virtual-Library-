using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.Repositories.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {

        public User GetByEmail(string email);

        public User GetUserWithOrders(int userId);

        public User GetUserWithRentals(int userId);






    }
}
