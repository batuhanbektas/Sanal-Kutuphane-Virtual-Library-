using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Repositories.Concrete;

namespace KutuphaneSatis.Services.Concrete
{
    public class CartService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICartRepository _cartRepository;


        public CartService(IBookRepository bookRepository, ICartRepository cartRepository)
        {

            _bookRepository = bookRepository;
            _cartRepository = cartRepository;
        }






    }
}
