using KutuphaneSatis.Models.Abstract;

namespace KutuphaneSatis.Models.Concrete
{
    public class Message : BaseEntities
    {
        string Topic { get; set; }

        string Text { get; set; }

        int UserId { get; set; }

        User User { get; set; }

    }
}
