using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KutuphaneSatis.Migrations
{
    /// <inheritdoc />
    public partial class TestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "isDeleted" },
                values: new object[,]
                {
                    { 1, "Gelecek, uzay ve ileri teknoloji temalı eserler.", "Bilim Kurgu", false },
                    { 2, "Geçmişte yaşanmış olayları ve dönemleri anlatan eserler.", "Tarih", false },
                    { 3, "Bireylerin potansiyellerini keşfetmelerine yardımcı olan kitaplar.", "Kişisel Gelişim", false }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorName", "CategoryID", "Description", "Name", "PageNumber", "Price", "Stock", "isDeleted" },
                values: new object[,]
                {
                    { 1, "Frank Herbert", 1, "Çöl gezegeni Arrakis'in destansı hikayesi.", "Dune", 712, 150.00m, 5, false },
                    { 2, "Isaac Asimov", 1, "Galaktik İmparatorluk çökerken kurulan yeni bir medeniyet.", "Vakıf", 400, 120.50m, 3, false },
                    { 3, "Yuval Noah Harari", 2, "İnsan türünün kısa bir tarihi.", "Sapiens", 416, 185.00m, 10, false },
                    { 4, "Mustafa Kemal Atatürk", 2, "Türkiye Cumhuriyeti'nin kuruluş dönemi belgesi.", "Nutuk", 600, 90.00m, 20, false },
                    { 5, "James Clear", 3, "Küçük değişimlerin yarattığı büyük sonuçlar.", "Atomik Alışkanlıklar", 352, 140.00m, 7, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
