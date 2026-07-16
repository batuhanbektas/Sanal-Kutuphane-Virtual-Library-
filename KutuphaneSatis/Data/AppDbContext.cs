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
     
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<User>()
                .HasOne(a => a.Cart)     
                .WithOne(b => b.User)     
                .HasForeignKey<Cart>(b => b.UserId); 


            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Bilim Kurgu", Description = "Gelecek, uzay ve ileri teknoloji temalı eserler." },
                new Category { Id = 2, Name = "Tarih", Description = "Geçmişte yaşanmış olayları ve dönemleri anlatan eserler." },
                new Category { Id = 3, Name = "Kişisel Gelişim", Description = "Bireylerin potansiyellerini keşfetmelerine yardımcı olan kitaplar." }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, CategoryID = 1, Name = "Dune", AuthorName = "Frank Herbert", Description = "Çöl gezegeni Arrakis'in destansı hikayesi.", PageNumber = 712, Stock = 5, Price = 150.00m },
                new Book { Id = 2, CategoryID = 1, Name = "Vakıf", AuthorName = "Isaac Asimov", Description = "Galaktik İmparatorluk çökerken kurulan yeni bir medeniyet.", PageNumber = 400, Stock = 3, Price = 120.50m },
                new Book { Id = 3, CategoryID = 2, Name = "Sapiens", AuthorName = "Yuval Noah Harari", Description = "İnsan türünün kısa bir tarihi.", PageNumber = 416, Stock = 10, Price = 185.00m },
                new Book { Id = 4, CategoryID = 2, Name = "Nutuk", AuthorName = "Mustafa Kemal Atatürk", Description = "Türkiye Cumhuriyeti'nin kuruluş dönemi belgesi.", PageNumber = 600, Stock = 20, Price = 90.00m },
                new Book { Id = 5, CategoryID = 3, Name = "Atomik Alışkanlıklar", AuthorName = "James Clear", Description = "Küçük değişimlerin yarattığı büyük sonuçlar.", PageNumber = 352, Stock = 7, Price = 140.00m }
            );
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBook> OrderBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalBook> RentalDetails { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }
    }
}