using KutuphaneSatis.DTOs.Request.CartRequest;
using KutuphaneSatis.DTOs.Response.CartResponses;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Repositories.Concrete;
using Microsoft.AspNetCore.Components.Forms;

namespace KutuphaneSatis.Services.Concrete
{
    public class CartService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IGenericRepository<CartDetail> _cartDetailRepository;

        public CartService(
        IBookRepository bookRepository,
        ICartRepository cartRepository,
        IGenericRepository<CartDetail> cartDetailRepository)
        {

            _bookRepository = bookRepository;
            _cartRepository = cartRepository;
            _cartDetailRepository = cartDetailRepository; // Ataması yapıldı
        }



        public CartResponse GetCart(int id)
        {
        
            var cart = _cartRepository.GetAll().FirstOrDefault(x => x.Id == id);
            var cartitems = _cartRepository.GetCartDetails(id);

            if(cart == null){return null;}

        
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
                TotalPrice = 0,
                UserId = cartRequest.UserId
            };

            _cartRepository.Create(cart);


        }

        public void CreateDetailandAdd(CartItemRequest cartitem)
        {
            if (cartitem == null) { throw new ArgumentException("Eklenecek ürün bilgisi boş olamaz!"); }

            var activeCart = _cartRepository.GetByID(cartitem.CartId);

            if (activeCart != null){
                CartDetail cartdetail = new CartDetail()
                {
                    CartId = cartitem.CartId,
                    BookId = cartitem.BookId,
                    Quantity = cartitem.Quantity,
                    Price = cartitem.UnitPrice * cartitem.Quantity


                };
                if (activeCart.CartDetail == null)
                {
                    activeCart.CartDetail = new List<CartDetail>();
                }

                activeCart.CartDetail.Add(cartdetail);

                activeCart.TotalPrice += cartdetail.Price;

                _cartRepository.Update(activeCart);
            }
            else
            {
                throw new Exception("Sayfayi yenileyiniz");
            }
           

        }



        public void RemoveItemFromCart(int cartDetailId)
        {
            // 1. Silinecek sepet detayını (ürünü) buluyoruz
            var cartDetail = _cartDetailRepository.GetByID(cartDetailId);

            if (cartDetail == null || cartDetail.isDeleted)
            {
                throw new Exception("Silinmek istenen ürün sepette bulunamadı veya zaten silinmiş.");
            }

            // 2. Bu ürünün bağlı olduğu Ana Sepeti (Cart) buluyoruz
            var activeCart = _cartRepository.GetByID(cartDetail.CartId);

            if (activeCart != null)
            {
                // 3. Sepetin toplam fiyatından, sildiğimiz ürünün fiyatını düşüyoruz
                activeCart.TotalPrice -= cartDetail.Price;

                // Fiyatın eksiye düşmemesi için (matematiksel bir hata olmaması adına) güvenlik:
                if (activeCart.TotalPrice < 0)
                {
                    activeCart.TotalPrice = 0;
                }

                // Sepetin yeni fiyatını veritabanına yansıtıyoruz
                _cartRepository.Update(activeCart);
            }

            // 4. Son olarak ürünü sepetten siliyoruz (Senin Repository'ndeki isDeleted = true çalışacak)
            _cartDetailRepository.Delete(cartDetailId);
        }

        public void ClearCart(int cartId)
        {
            var activeCart = _cartRepository.GetByID(cartId);

            if (activeCart != null)
            {
                var cartDetails = _cartRepository.GetCartDetails(cartId);

                foreach (var detail in cartDetails)
                {
                    _cartDetailRepository.Delete(detail.Id);
                }
                activeCart.TotalPrice = 0;

                _cartRepository.Update(activeCart);
            }
            else
            {
                throw new Exception("Boşaltılmak istenen sepet bulunamadı.");
            }

        }


        public void UpdateItemQuantity(int cartDetailId, int newQuantity)
        {
            var cartdetail = _cartDetailRepository.GetByID(cartDetailId);

            cartdetail.Quantity = newQuantity;

            _cartDetailRepository.Update(cartdetail);

        }
    }

}