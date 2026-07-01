using KutuphaneSatis.Models;

namespace KutuphaneSatis.Models.Concrete
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
