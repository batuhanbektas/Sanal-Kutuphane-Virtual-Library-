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
                // Sadece silinMEMİŞ (isDeleted == false) olan detayları getir
                .SelectMany(x => x.CartDetail.Where(d => d.isDeleted == false))
                .ToList();
        }

        public Cart GetCartByUserId(int id)
        {
            return _dbSet
                .Where(x => x.UserId == id)
                .FirstOrDefault();

        }


        public int ReturnCartId(int Userid)
        {
            return _dbSet
                .Where(x => x.UserId == Userid)
                .Select(x => x.Id)
                .FirstOrDefault();


        }



    } 
}

