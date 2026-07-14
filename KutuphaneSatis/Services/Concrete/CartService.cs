using KutuphaneSatis.DTOs.Request.CartRequest;
using KutuphaneSatis.DTOs.Response.CartResponse.CartResponse;
using KutuphaneSatis.DTOs.Response.CartResponses;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Repositories.Concrete;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Components.Forms;

namespace KutuphaneSatis.Services.Concrete
{
    public class CartService : ICartService
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
            _cartDetailRepository = cartDetailRepository;
        }

        public CartResponse GetCart(int id)
        {
            var cart = _cartRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (cart == null)
            {
                return null;
            }

            List<CartDetail> cartitems = _cartRepository.GetCartDetails(id);

            List<CartItemResponse> itemResponses = cartitems.Select(item => new CartItemResponse
            {
                CartId = item.CartId,
                BookId = item.BookId,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                BookName = item.BookName,
                cartitemid = item.Id
            }).ToList();

            CartResponse cartResponse = new CartResponse()
            {
                UserId = cart.UserId,
                TotalPrice = cart.TotalPrice,
                CartItems = itemResponses
            };

            return cartResponse;
        }

        public void CreateCart(CreateCartRequest cartRequest)
        {
            Cart cart = new Cart()
            {
                TotalPrice = 0,
                UserId = cartRequest.UserId,
                totalQuantity = 0 // EKLENDİ: Başlangıçta 0 atandı
            };

            _cartRepository.Create(cart);
        }

        public void CreateDetailandAdd(CartItemRequest cartitem)
        {
            if (cartitem == null) { throw new ArgumentException("Eklenecek ürün bilgisi boş olamaz!"); }

            var activeCart = _cartRepository.GetByID(cartitem.CartId);

            if (activeCart != null)
            {
                if (activeCart.CartDetail == null)
                {
                    activeCart.CartDetail = new List<CartDetail>();
                }

                // EKLENDİ: Sepette bu kitap zaten var mı (ve silinmemiş mi) kontrol ediyoruz
                var existingItem = activeCart.CartDetail.FirstOrDefault(x => x.BookId == cartitem.BookId && x.isDeleted == false);

                if (existingItem != null)
                {
                    // EKLENDİ: Kitap zaten varsa, yeni satır açmak yerine sadece miktarını ve fiyatını artırıyoruz
                    existingItem.Quantity += cartitem.Quantity;
                    existingItem.Price += (cartitem.UnitPrice * cartitem.Quantity);

                    _cartDetailRepository.Update(existingItem);
                }
                else
                {
                    // Kitap sepette hiç yoksa, senin orijinal kodunla yeni bir satır olarak ekliyoruz
                    CartDetail cartdetail = new CartDetail()
                    {
                        CartId = cartitem.CartId,
                        BookId = cartitem.BookId,
                        Quantity = cartitem.Quantity,
                        Price = cartitem.UnitPrice * cartitem.Quantity,
                        BookName = cartitem.BookName,
                    };
                    activeCart.CartDetail.Add(cartdetail);
                }

                // Ana sepetin toplam fiyat ve toplam adet bilgilerini her halükarda güncelliyoruz
                activeCart.TotalPrice += (cartitem.UnitPrice * cartitem.Quantity);
                activeCart.totalQuantity += cartitem.Quantity;

                _cartRepository.Update(activeCart);
            }
            else
            {
                throw new Exception("Sayfayi yenileyiniz");
            }
        }

        public void RemoveItemFromCart(int cartDetailId)
        {
            var cartDetail = _cartDetailRepository.GetByID(cartDetailId);

            if (cartDetail == null || cartDetail.isDeleted)
            {
                throw new Exception("Silinmek istenen ürün sepette bulunamadı veya zaten silinmiş.");
            }

            var activeCart = _cartRepository.GetByID(cartDetail.CartId);

            if (activeCart != null)
            {
                activeCart.TotalPrice -= cartDetail.Price;

                // EKLENDİ: Sildiğimiz ürünün adedini de ana sepetten düşüyoruz
                activeCart.totalQuantity -= cartDetail.Quantity;

                if (activeCart.TotalPrice < 0) activeCart.TotalPrice = 0;
                if (activeCart.totalQuantity < 0) activeCart.totalQuantity = 0; // EKLENDİ: Eksi adede düşmesini engelliyoruz

                _cartRepository.Update(activeCart);
            }

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

                // EKLENDİ: Sepet tamamen boşaldığı için toplam adedi de sıfırlıyoruz
                activeCart.totalQuantity = 0;

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

            if (cartdetail != null)
            {
                decimal unitPrice = cartdetail.Price / cartdetail.Quantity;
                decimal oldLineTotal = cartdetail.Price;

                // EKLENDİ: Yeni miktar atanmadan önce eski miktarı hafızaya alıyoruz
                int oldQuantity = cartdetail.Quantity;

                cartdetail.Quantity = newQuantity;
                cartdetail.Price = unitPrice * newQuantity;

                _cartDetailRepository.Update(cartdetail);

                var activeCart = _cartRepository.GetByID(cartdetail.CartId);

                if (activeCart != null)
                {
                    // Fiyat Güncellemesi
                    activeCart.TotalPrice -= oldLineTotal;
                    activeCart.TotalPrice += cartdetail.Price;

                    // EKLENDİ: Toplam Adet Güncellemesi (Eskiyi çıkar, yeniyi ekle)
                    activeCart.totalQuantity -= oldQuantity;
                    activeCart.totalQuantity += newQuantity;

                    _cartRepository.Update(activeCart);
                }
            }
        }
    }
}