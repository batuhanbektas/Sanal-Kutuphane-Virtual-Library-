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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Var olan ayarları ezmemek için temel metodu çağırıyoruz
            base.OnModelCreating(modelBuilder);

            // Bire-Bir (One-to-One) İlişki Kurulumu
            modelBuilder.Entity<User>()
                .HasOne(a => a.Cart)     // Asıl tablonun BİR TANE bağlı tablosu vardır
                .WithOne(b => b.User)     // Bağlı tablonun da BİR TANE asıl tablosu vardır
                .HasForeignKey<Cart>(b => b.UserId); // Yabancı Anahtar bağlı tablonun içindedir
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Message> Messages { get; set; }


        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderBook> OrderBooks { get; set; }


        public DbSet<Rental> Rentals { get; set; }

        public DbSet<RentalBook> RentalDetails { get; set; }

        public DbSet<User> User { get; set;  }

        public DbSet<Cart> Cart {  get; set; }

        public DbSet<CartDetail> CartDetail { get; set; }
    }
}
