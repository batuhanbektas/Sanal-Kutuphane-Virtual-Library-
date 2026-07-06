using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.Repositories.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {

        public User GetByEmail(string email);

        public User GetUserWithOrders(int userId);
        public bool IsThereUserWithEmail(string email);

        public User GetUserWithRentals(int userId);


        public string GetPassword(int userId);




    }
}
