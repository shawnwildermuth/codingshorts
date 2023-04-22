using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RepoOrNot.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Terms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CompanyName", "ContactName", "Phone" },
                values: new object[,]
                {
                    { 1, "Acme Shipping", "Bob Smith", "404-555-2020" },
                    { 2, "Pete's Coffee", "Pete Johnson", "203-456-0098" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "DueDate", "OrderDate", "OrderNumber", "Terms" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 5, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4748), new DateTime(2023, 4, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4746), "1000", "Net 30" },
                    { 2, 1, new DateTime(2023, 5, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4757), new DateTime(2023, 4, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4756), "1001", "Net 90" },
                    { 3, 2, new DateTime(2023, 5, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4783), new DateTime(2023, 4, 15, 7, 23, 18, 261, DateTimeKind.Utc).AddTicks(4783), "1002", "Net 15" }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "Discount", "Notes", "OrderId", "ProductName", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 0.0, null, 1, "Labels", 24.0, 14.99 },
                    { 2, 0.25, null, 1, "Boxes", 144.0, 3.9900000000000002 },
                    { 3, 0.0, null, 1, "Receipt Paper", 4.0, 19.989999999999998 },
                    { 4, 0.050000000000000003, null, 2, "Printer Ink", 4.0, 114.98 },
                    { 5, 0.050000000000000003, null, 2, "Boxes", 24.0, 3.9900000000000002 },
                    { 6, 0.0, null, 3, "Pens", 12.0, 4.9800000000000004 },
                    { 7, 0.10000000000000001, null, 3, "Pencils", 12.0, 1.99 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
