using Microsoft.EntityFrameworkCore;
using KutuphaneSatis.Models;
using KutuphaneSatis.Models.Concrete;

namespace KutuphaneSatis.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Message> Messages { get; set; }


        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderBook> OrderBooks { get; set; }


        public DbSet<Rental> Rentals { get; set; }

        public DbSet<RentalBook> RentalDetails { get; set; }

        public DbSet<User> User { get; set;  }
    }
}
