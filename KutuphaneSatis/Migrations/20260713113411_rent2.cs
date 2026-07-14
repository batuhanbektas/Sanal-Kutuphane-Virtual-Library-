using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneSatis.Migrations
{
    /// <inheritdoc />
    public partial class rent2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "totalQuantity",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalQuantity",
                table: "Cart");
        }
    }
}
