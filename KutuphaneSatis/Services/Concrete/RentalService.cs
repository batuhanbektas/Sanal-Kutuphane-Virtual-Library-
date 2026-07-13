using Azure.Core;
using KutuphaneSatis.DTOs.Request.BookRequest;
using KutuphaneSatis.DTOs.Request.RentRequest;
using KutuphaneSatis.DTOs.Response.RentResponse;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Repositories.Concrete;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata;



namespace KutuphaneSatis.Services.Concrete
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IGenericRepository<RentalBook> _rentalbookRepository;
        private readonly IUserRepository _userRepository;

        public RentalService(
            IRentalRepository rentalRepository,
            ICartRepository cartRepository,
            IGenericRepository<RentalBook> genericRepository,
            IUserRepository userRepository)
        {
            _rentalbookRepository = genericRepository;
            _cartRepository = cartRepository;
            _rentalRepository = rentalRepository;
            _userRepository = userRepository;
        }

        public void CreateRent(int userid)
        {
            var user = _userRepository.GetByID(userid);
            var cart = _cartRepository.GetByID(user.CartId);
            var cartDetails = _cartRepository.GetCartDetails(user.CartId);
            List<RentItemRequest> rentItemRequests = new List<RentItemRequest>();

            foreach (var Book in cartDetails)
            {
                RentItemRequest rentitem = new RentItemRequest
                {
                    BookId = Book.BookId,
                    BookName = Book.BookName,
                    Quantity = Book.Quantity,
                    UnitPrice = Book.Price,
                    
                    
                };
                rentItemRequests.Add(rentitem);
            }

            CreateRentRequest request = new CreateRentRequest
            {
                TotalPrice = cart.TotalPrice,
                UserId = userid,
                RentBooks = rentItemRequests,
                
            };

            Rental rent = new Rental
            {
                TotalPrice = request.TotalPrice,
                UserId = request.UserId,
                RStartTime = DateOnly.FromDateTime(DateTime.Now),
                isDeleted = false,
                RentalBook = request.RentBooks.Select(item => new RentalBook
                {
                    BookName = item.BookName,
                    isDeleted = false,
                    BookId = item.BookId,
                    RentalDurationDays = 7,
                    RentalQuantity = item.Quantity,
                    ReturnedQuantitiy = 0,
                    UnitPrice = item.UnitPrice,
                   
                }).ToList(),
            };

            cart.TotalPrice = 0;
            cart.CartDetail.Clear();

            _rentalRepository.Create(rent);


        }



        public RentalDetailResponse GetRentDetails(int id)
        {
            // 1. Ana siparişi getir
            var rent = _rentalRepository.GetByID(id);

            // 2. ÇÖZÜM BURADA: Bu siparişe (id) ait olan kitapları OrderBook tablosundan çekiyoruz
            var siparisinKitaplari = _rentalbookRepository.GetAll().Where(x => x.RentalId == id).ToList();

            // 3. DTO'yu dolduruyoruz
            RentalDetailResponse rentalDetailResponse = new RentalDetailResponse()
            {
                REndTime = rent.REndTime,
                RStartTime = rent.RStartTime,
                TotalPrice = rent.TotalPrice,
                UserId = rent.UserId,

                // order.OrderBook YERİNE, yukarıda çektiğimiz 'siparisinKitaplari' listesini kullanıyoruz:
                RentItem = siparisinKitaplari.Select(item => new RentItemResponse
                {
                    BookId = item.BookId,
                    BookName = item.BookName,
                    RentalDurationDays = item.RentalDurationDays,
                    RentalId = item.RentalId,
                    RentalQuantity = item.RentalQuantity,
                    ReturnedQuantitiy = item.ReturnedQuantitiy,
                    UnitPrice = item.UnitPrice


                    
                    // <-- DİKKAT! (Aşağıdaki nota bak)
                }).ToList()
            };

            return rentalDetailResponse;
        }



        // 1. Metodun dönüş tipini "Liste" dönecek şekilde güncelledik
        public List<RentalHistoryResponse> GetRental()
        {
            // 2. Veritabanından tüm siparişleri (Entity listesini) çekiyoruz
            var rents= _rentalRepository.GetAll();

            // İsteğe bağlı güvenlik kontrolü: Sipariş yoksa boş liste dön
            if (rents == null)
            {
                return new List<RentalHistoryResponse>();
            }

            // 3. Entity listesini, Select() yardımıyla DTO listesine dönüştürüyoruz
            List<RentalHistoryResponse> rentsHistoryResponse = rents.Select(rent => new RentalHistoryResponse
            {
                // BaseEntities'den gelen ID'yi OrderId'ye atıyoruz
                RentalId = rent.Id,
                UserId = rent.UserId,
                TotalPrice = rent.TotalPrice,
                RStartTime = rent.RStartTime,
                REndTime = rent.REndTime,


            }).ToList(); // Sonucu bir Listeye çeviriyoruz

            // 4. Hazırladığımız listeyi dışarıya (Controller'a) teslim ediyoruz
            return rentsHistoryResponse;
        }


    }
}
