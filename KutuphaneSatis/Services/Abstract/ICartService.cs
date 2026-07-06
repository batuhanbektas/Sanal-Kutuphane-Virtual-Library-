using KutuphaneSatis.DTOs.Response.CartResponses;

namespace KutuphaneSatis.Services.Abstract
{
    public interface ICartService
    {

        public CartResponse GetCart(int id);



    }
}
