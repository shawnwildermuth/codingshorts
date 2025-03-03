using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeMoney.Migrations
{
    /// <inheritdoc />
    public partial class Sizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Width",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "OrderItem");
        }
    }
}
