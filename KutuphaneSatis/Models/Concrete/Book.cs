using KutuphaneSatis.Models.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace KutuphaneSatis.Models.Concrete
{
    public class Book : BaseEntities
    {

        public string Name { get; set; }

        public string AuthorName { get; set; }
        public string Description { get; set; }

        public int PageNumber { get; set; } = 0;

        public int Stock { get; set; } = 0;

        public int CategoryID { get; set; }


        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

        public decimal Price { get; set; }

    }
}
