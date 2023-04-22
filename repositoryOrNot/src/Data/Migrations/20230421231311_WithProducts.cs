using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RepoOrNot.Migrations
{
    /// <inheritdoc />
    public partial class WithProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "OrderDate" },
                values: new object[] { new DateTime(2023, 5, 21, 23, 13, 11, 255, DateTimeKind.Utc).AddTicks(2228), new DateTime(2023, 4, 21, 23, 13, 11, 255, DateTimeKind.Utc).AddTicks(2228) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DueDate", "OrderDate" },
                values: new object[] { new DateTime(2023, 5, 21, 23, 13, 11, 255, DateTimeKind.Utc).AddTicks(2236), new DateTime(2023, 4, 21, 23, 13, 11, 255, DateTimeKind.Utc).AddTicks(2235) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DueDate", "OrderDate" },
                values: new object[] { new DateTime(2023, 5, 21, 23, 13, 11, 255, DateTimeKind.Utc).AddTicks(2237), new DateTime(2023, 4, 21, 23, 13, 11, 255, DateTimeKind.Utc).AddTicks(2237) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Image", "ProductName", "Unit", "UnitPrice" },
                values: new object[,]
                {
                    { 1, null, null, "Envelopes", "Box", 4.99m },
                    { 2, null, null, "Binders", "Each", 3.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DueDate", "OrderDate" },
                values: new object[] { new DateTime(2023, 5, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4748), new DateTime(2023, 4, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4746) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DueDate", "OrderDate" },
                values: new object[] { new DateTime(2023, 5, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4757), new DateTime(2023, 4, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4756) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DueDate", "OrderDate" },
                values: new object[] { new DateTime(2023, 5, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4783), new DateTime(2023, 4, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4783) });
        }
    }
}
