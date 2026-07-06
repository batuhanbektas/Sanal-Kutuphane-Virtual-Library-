using KutuphaneSatis.DTOs.Request.CartRequest;
using KutuphaneSatis.DTOs.Response.CartResponses;
using KutuphaneSatis.Models.Concrete;
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



            public CartResponse GetCart(int id)
            {
            
                var cart = _cartRepository.GetAll().FirstOrDefault(x => x.Id == id);
                var cartitems = _cartRepository.GetCartDetails(id);

                if(cart == null)
                {

 
                
            
                }

            
                CartResponse cartResponse = new CartResponse()
                {
                    CartItems = cartitems,
                    TotalPrice = cart.TotalPrice,
                    UserId = cart.UserId

                };
            
                return cartResponse;

            }

        public void CreateCart(CreateCartRequest cartRequest) 
        {
            Cart cart = new Cart()
            {
                CartDetail = null,
                TotalPrice = 0,
                UserId = cartRequest.UserId
            };
        }



    }

}