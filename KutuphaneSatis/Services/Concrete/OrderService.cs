using Azure.Core;
using KutuphaneSatis.DTOs.Request.BookRequest;
using KutuphaneSatis.DTOs.Request.OrderRequest;
using KutuphaneSatis.DTOs.Response.OrderResponse;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Repositories.Concrete;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata;



namespace KutuphaneSatis.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IGenericRepository<OrderBook> _orderbookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;

        public OrderService(
            IBookRepository bookRepository,
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IGenericRepository<OrderBook> genericRepository,
            IUserRepository userRepository)
        {
            _orderbookRepository = genericRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        public void CreateOrder(int userid)
        {
            var user = _userRepository.GetByID(userid);
            var cart = _cartRepository.GetByID(user.CartId);
            var cartDetails = _cartRepository.GetCartDetails(user.CartId);
            List<OrderItemRequest> orderItemRequests = new List<OrderItemRequest>();

            foreach (var Book in cartDetails) { 
                OrderItemRequest orderitem = new OrderItemRequest
            {
                BookId = Book.BookId,
                BookName = Book.BookName,
                Quantity = Book.Quantity,
                UnitPrice = Book.Price,
               
            };
               orderItemRequests.Add(orderitem);

                var actualBook = _bookRepository.GetByID(Book.BookId);
                if (actualBook != null)
                {
                    actualBook.Stock -= Book.Quantity; // Kiralanan miktar kadar stoğu azalt
                    _bookRepository.Update(actualBook); // Veritabanında kitabın yeni stoğunu güncelle
                }
            }

            CreateOrderRequest request = new CreateOrderRequest
            {
                TotalPrice = cart.TotalPrice,
                UserId = userid,
                OrderBooks = orderItemRequests
               
            };

            Order order = new Order
            {
                TotalPrice = request.TotalPrice,
                UserId = request.UserId,
                OrderTime = DateOnly.FromDateTime(DateTime.Now),
                isDeleted = false,
                OrderBook = request.OrderBooks.Select(item => new OrderBook {

                    BookName = item.BookName,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    isDeleted = false,
                    UnitPrice = item.UnitPrice
                }).ToList(),
            };

            cart.TotalPrice = 0;
            cart.CartDetail.Clear();

            _orderRepository.Create(order);


        }



        public OrderDetailResponse GetOrderDetails(int id)
        {
            // 1. Ana siparişi getir
            var order = _orderRepository.GetByID(id);

            // 2. ÇÖZÜM BURADA: Bu siparişe (id) ait olan kitapları OrderBook tablosundan çekiyoruz
            var siparisinKitaplari = _orderbookRepository.GetAll().Where(x => x.OrderId == id).ToList();

            // 3. DTO'yu dolduruyoruz
            OrderDetailResponse orderDetailResponse = new OrderDetailResponse()
            {
                TotalPrice = order.TotalPrice,
                OrderTime = order.OrderTime,
                UserId = order.UserId,

                // order.OrderBook YERİNE, yukarıda çektiğimiz 'siparisinKitaplari' listesini kullanıyoruz:
                OrderItem = siparisinKitaplari.Select(item => new OrderItemResponse
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    BookName = item.BookName // <-- DİKKAT! (Aşağıdaki nota bak)
                }).ToList()
            };

            return orderDetailResponse;
        }



        // 1. Metodun dönüş tipini "Liste" dönecek şekilde güncelledik
        public List<OrderHistoryResponse> GetOrders()
        {
            // 2. Veritabanından tüm siparişleri (Entity listesini) çekiyoruz
            var orders = _orderRepository.GetAll();

            // İsteğe bağlı güvenlik kontrolü: Sipariş yoksa boş liste dön
            if (orders == null)
            {
                return new List<OrderHistoryResponse>();
            }

            // 3. Entity listesini, Select() yardımıyla DTO listesine dönüştürüyoruz
            List<OrderHistoryResponse> ordersHistoryResponse = orders.Select(order => new OrderHistoryResponse
            {
                // BaseEntities'den gelen ID'yi OrderId'ye atıyoruz
                OrderId = order.Id,
                UserId = order.UserId,
                OrderTime = order.OrderTime,
                TotalPrice = order.TotalPrice

            }).ToList(); // Sonucu bir Listeye çeviriyoruz

            // 4. Hazırladığımız listeyi dışarıya (Controller'a) teslim ediyoruz
            return ordersHistoryResponse;
        }


    }
}
