using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneSatis.Repositories.Concrete
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context) { }




        public List<CartDetail> GetCartDetails(int id)
        {
            var details = _dbSet
                .Where(x => x.Id == id)
                .Select(x => x.CartDetail)
                .FirstOrDefault();

            // Eğer o ID'de bir sepet yoksa details null döner. Null referans hatası yememek için kontrol ediyoruz.
            if (details != null)
            {
                return details.ToList(); // ICollection'ı List'e çevirip döndürüyoruz.
            }

            return new List<CartDetail>(); // Sepet yoksa boş liste dön.
        }

    }
}
