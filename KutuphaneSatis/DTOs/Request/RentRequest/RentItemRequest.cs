namespace KutuphaneSatis.DTOs.Request.RentRequest
{
    public class RentItemRequest
    {


        public int BookId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int RentId { get; set; }

        public string BookName { get; set; }
    }
}
