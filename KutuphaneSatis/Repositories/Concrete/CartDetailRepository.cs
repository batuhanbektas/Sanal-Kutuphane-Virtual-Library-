using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;

namespace KutuphaneSatis.Repositories.Concrete
{
    // Önce Abstract klasöründe ICartDetailRepository interface'ini oluşturmayı unutma!
    public class CartDetailRepository : GenericRepository<CartDetail>, ICartDetailRepository
    {
        public CartDetailRepository(AppDbContext context) : base(context)
        {
        }

        // İleride CartDetail'e özel metotlar gerekirse buraya yazabilirsin.
    }
}