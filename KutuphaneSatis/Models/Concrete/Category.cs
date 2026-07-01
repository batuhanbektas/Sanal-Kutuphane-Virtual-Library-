    using KutuphaneSatis.Models.Abstract;
    using System.ComponentModel.DataAnnotations;

    namespace KutuphaneSatis.Models.Concrete
    {
        public class Category : BaseEntities
        {


            public string Name { get; set; }
            public string Description { get; set; }
            public ICollection<Book> Book { get; set; }



        }
    }
