namespace KutuphaneSatis.DTOs.Response.OrderResponse
{
    public class OrderHistoryResponse
    {
        public DateOnly OrderTime { get; set; }

        public decimal TotalPrice { get; set; }

        public int OrderId { get; set; }

        public int UserId { get; set; }

    }
}
