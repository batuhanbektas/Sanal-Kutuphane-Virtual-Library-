using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneSatis.Migrations
{
    /// <inheritdoc />
    public partial class Cartitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookName",
                table: "CartDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookName",
                table: "CartDetail");
        }
    }
}
