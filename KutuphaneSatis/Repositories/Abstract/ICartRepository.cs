using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.Repositories.Abstract
{
    public interface ICartRepository : IGenericRepository<Cart>
    {

        public List<CartDetail> GetCartDetails(int id);

    }
}
