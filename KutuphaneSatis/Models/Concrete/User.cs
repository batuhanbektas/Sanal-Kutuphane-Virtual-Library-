using KutuphaneSatis.Models.Abstract;

namespace KutuphaneSatis.Models.Concrete
{
    public class User : BaseEntities
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }


        public ICollection<Order> Orders { get; set; }

        public ICollection<Rental> Rents { get; set; }


        public User() 
        {
            bool IsAdmin = false;

            bool IsActive = true;

        }



    }
}
