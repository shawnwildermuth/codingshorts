using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BakeAndCake.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
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
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsLoyaltyMember = table.Column<bool>(type: "bit", nullable: false),
                    LoyaltyPoints = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CostPerUnit = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    StockQuantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ReorderThreshold = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IsAllergen = table.Column<bool>(type: "bit", nullable: false),
                    AllergenInfo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsPieOfTheWeek = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllergyInformation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PreparationTimeMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomPies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DedicationMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Size = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CrustType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PrimaryFilling = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SpecialInstructions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    EstimatedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequiredByDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomPies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomPies_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequiredByDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Fulfilment = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ServedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductIngredients",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    QuantityRequired = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIngredients", x => new { x.ProductId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_ProductIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductIngredients_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomPieIngredients",
                columns: table => new
                {
                    CustomPieId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomPieIngredients", x => new { x.CustomPieId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_CustomPieIngredients_CustomPies_CustomPieId",
                        column: x => x.CustomPieId,
                        principalTable: "CustomPies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomPieIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    CustomPieId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecialRequests = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_CustomPies_CustomPieId",
                        column: x => x.CustomPieId,
                        principalTable: "CustomPies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeGiven = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "FirstName", "IsLoyaltyMember", "LastName", "LoyaltyPoints", "Phone" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "margaret.whitfield@email.com", "Margaret", true, "Whitfield", 340, "07700900001" },
                    { 2, null, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "robert.craine@email.com", "Robert", true, "Craine", 120, "07700900002" },
                    { 3, null, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "saoirse.murphy@email.com", "Saoirse", false, "Murphy", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "AllergenInfo", "CostPerUnit", "Description", "IsAllergen", "Name", "ReorderThreshold", "StockQuantity", "Unit" },
                values: new object[,]
                {
                    { 1, "Gluten", 0.0015m, null, true, "Plain Flour", 4000m, 20000m, "g" },
                    { 2, "Dairy", 0.0120m, null, true, "Butter", 2000m, 10000m, "g" },
                    { 3, null, 0.0020m, null, false, "Caster Sugar", 3000m, 15000m, "g" },
                    { 4, "Eggs", 0.3500m, null, true, "Free-Range Eggs", 60m, 300m, "pc" },
                    { 5, "Dairy", 0.0008m, null, true, "Whole Milk", 3000m, 15000m, "ml" },
                    { 6, "Dairy", 0.0035m, null, true, "Double Cream", 1000m, 5000m, "ml" },
                    { 7, null, 0.0040m, null, false, "Bramley Apples", 2000m, 12000m, "g" },
                    { 8, null, 0.0120m, null, false, "Blackberries", 1000m, 5000m, "g" },
                    { 9, null, 0.0090m, null, false, "Cherry (pitted)", 1200m, 6000m, "g" },
                    { 10, null, 0.0180m, null, false, "Cocoa Powder", 600m, 3000m, "g" },
                    { 11, "May contain traces of nuts", 0.0250m, null, true, "Dark Chocolate", 800m, 4000m, "g" },
                    { 12, null, 0.0110m, null, false, "Minced Beef", 1500m, 8000m, "g" },
                    { 13, null, 0.0095m, null, false, "Diced Chicken", 1500m, 8000m, "g" },
                    { 14, null, 0.0030m, null, false, "Leeks", 1000m, 5000m, "g" },
                    { 15, "Dairy", 0.0140m, null, true, "Cheddar Cheese", 800m, 4000m, "g" },
                    { 16, null, 0.0025m, null, false, "Icing Sugar", 1000m, 5000m, "g" },
                    { 17, null, 0.0500m, null, false, "Vanilla Extract", 100m, 500m, "ml" },
                    { 18, null, 0.0050m, null, false, "Baking Powder", 400m, 2000m, "g" },
                    { 19, null, 0.0005m, null, false, "Salt", 500m, 5000m, "g" },
                    { 20, "Gluten, Dairy", 1.2000m, null, true, "Puff Pastry Sheets", 20m, 100m, "pc" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AllergyInformation", "Category", "Description", "ImageUrl", "IsAvailable", "IsPieOfTheWeek", "Name", "PreparationTimeMinutes", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { 1, "Contains Gluten, Dairy, Eggs", "Pie", null, null, true, false, "Classic Apple Pie", 60, 12.95m, "Old family recipe, Bramley apples & cinnamon" },
                    { 2, "Contains Gluten, Dairy, Eggs, Nuts", "Tart", null, null, true, false, "Cherry & Almond Tart", 45, 8.50m, "Sweet shortcrust with fresh cherries" },
                    { 3, "Contains Gluten, Dairy", "Pie", null, null, true, true, "Steak & Ale Pie", 90, 14.50m, "Slow-braised beef in rich ale gravy" },
                    { 4, "Contains Gluten, Dairy, Eggs", "Pie", null, null, true, false, "Chicken, Leek & Cheese Pie", 75, 13.00m, "Creamy filling with Cheddar top crust" },
                    { 5, "Contains Gluten, Dairy, Eggs", "Tart", null, null, true, false, "Chocolate Silk Tart", 50, 9.95m, "Dark chocolate ganache in a crisp shell" },
                    { 6, "Contains Gluten, Dairy", "Pie", null, null, true, false, "Blackberry & Apple Crumble Pie", 55, 11.50m, "Seasonal berries under a buttery crumble" },
                    { 7, "Contains Gluten, Dairy, Eggs", "Quiche", null, null, true, false, "Cheese & Leek Quiche", 50, 8.00m, "Savory custard with Cheddar & fresh leeks" },
                    { 8, "Contains Gluten, Dairy, Eggs", "Tart", null, null, true, false, "Vanilla Custard Tart", 40, 7.50m, "Silky-smooth custard in shortcrust pastry" }
                });

            migrationBuilder.InsertData(
                table: "ProductIngredients",
                columns: new[] { "IngredientId", "ProductId", "QuantityRequired" },
                values: new object[,]
                {
                    { 1, 1, 350m },
                    { 2, 1, 175m },
                    { 3, 1, 120m },
                    { 4, 1, 2m },
                    { 7, 1, 750m },
                    { 1, 3, 300m },
                    { 2, 3, 150m },
                    { 12, 3, 600m },
                    { 1, 4, 300m },
                    { 2, 4, 150m },
                    { 13, 4, 500m },
                    { 14, 4, 200m },
                    { 15, 4, 150m },
                    { 1, 5, 200m },
                    { 2, 5, 100m },
                    { 6, 5, 250m },
                    { 11, 5, 300m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomPieIngredients_IngredientId",
                table: "CustomPieIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomPies_CustomerId",
                table: "CustomPies",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CustomPieId",
                table: "OrderItems",
                column: "CustomPieId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDate",
                table: "Orders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Status",
                table: "Orders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_IngredientId",
                table: "ProductIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_OrderId",
                table: "Receipts",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ReceiptNumber",
                table: "Receipts",
                column: "ReceiptNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomPieIngredients");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductIngredients");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "CustomPies");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
