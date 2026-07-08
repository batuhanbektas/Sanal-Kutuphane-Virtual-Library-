using KutuphaneSatis.DTOs.Request.CartRequest;
using KutuphaneSatis.DTOs.Response.CartResponses;

namespace KutuphaneSatis.Services.Abstract
{
    public interface ICartService
    {

        public CartResponse GetCart(int id);
        public void CreateCart(CreateCartRequest cartRequest);
        public void CreateDetailandAdd(CartItemRequest cartitem);
        public void RemoveItemFromCart(int cartDetailId);
        public void ClearCart(int cartId);
        public void UpdateItemQuantity(int cartDetailId, int newQuantity);



    }
}
