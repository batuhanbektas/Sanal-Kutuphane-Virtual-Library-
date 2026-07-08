using KutuphaneSatis.Data;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace KutuphaneSatis.Repositories.Concrete
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context) { }




        public List<CartDetail> GetCartDetails(int id)
        {
            return _dbSet
                .Where(x => x.Id == id)
                .SelectMany(x => x.CartDetail) // Sepetin içindeki koleksiyonu direkt dışarı çıkartır
                .ToList(); // Sepet yoksa veya boşsa kendi kendine boş liste döner
        }
        


    }
}
