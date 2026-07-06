using KutuphaneSatis.Models.Abstract;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace KutuphaneSatis.Models.Concrete
{

    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntities
    {

        public string Name { get; set; }
        public string Email { get; set; } 
        public string Surname { get; set; }
        public string Password { get; set; }


        public ICollection<Order>? Orders { get; set; } = null;

        public ICollection<Rental>? Rents { get; set; } = null;


        public User() 
        {
            bool IsAdmin = false;

            bool IsActive = true;

        }



    }
}
