using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class postgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address1 = table.Column<string>(type: "text", nullable: true),
                    Address2 = table.Column<string>(type: "text", nullable: true),
                    Address3 = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    StateProvince = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    ContactName = table.Column<string>(type: "text", nullable: true),
                    AddressId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Terms = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Discount = table.Column<double>(type: "double precision", nullable: false)
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
                table: "Addresses",
                columns: new[] { "Id", "Address1", "Address2", "Address3", "City", "Country", "PostalCode", "StateProvince" },
                values: new object[,]
                {
                    { 1, "2125 Reynold Estate", "Apt. 962", null, "New Katharinamouth", null, "49786-9864", "Wisconsin" },
                    { 2, "10025 Arjun Inlet", null, null, "Lake Daronburgh", null, "11664", "Montana" },
                    { 3, "448 Cara Flats", "Suite 286", null, "Brendanfurt", null, "06626", "Iowa" },
                    { 4, "969 Lang Expressway", null, null, "Boyleburgh", null, "99821-3638", "Montana" },
                    { 5, "213 Abernathy Shores", "Apt. 592", null, "West Emiliaville", null, "87195-9738", "Wisconsin" },
                    { 6, "170 Shaniya Stravenue", null, null, "Rooseveltfort", null, "08599", "New Hampshire" },
                    { 7, "9009 Altenwerth Radial", null, null, "West Nicoburgh", null, "78462", "Illinois" },
                    { 8, "42779 Lind Spurs", null, null, "Jacobsonville", null, "83659-2524", "New Hampshire" },
                    { 9, "3815 Shayna Spring", "Apt. 409", null, "Lake Carlistad", null, "53697", "Delaware" },
                    { 10, "58924 Ila Neck", "Apt. 033", null, "New Sonnyshire", null, "95468-6409", "California" },
                    { 11, "236 Austyn Junctions", "Suite 660", null, "Lake Philip", null, "30846", "Rhode Island" },
                    { 12, "779 Jayne Wells", null, null, "Greenholtshire", null, "99347-2268", "Nebraska" },
                    { 13, "5241 Annabelle Brook", null, null, "Chaddville", null, "30428-2843", "Maryland" },
                    { 14, "96538 Destinee Heights", null, null, "Lake Giuseppeton", null, "36833-8512", "Ohio" },
                    { 15, "96509 Tod Common", null, null, "Corwinfurt", null, "08877", "Kentucky" },
                    { 16, "0030 Shaina Alley", null, null, "West Clarkborough", null, "34274", "California" },
                    { 17, "03210 Tina Extensions", "Apt. 260", null, "Rhiannonmouth", null, "18318-8577", "Louisiana" },
                    { 18, "8874 Maximillia Lane", null, null, "Lake Caramouth", null, "55798", "Illinois" },
                    { 19, "34557 Bradtke Cove", null, null, "Lake Franciscahaven", null, "58146", "Louisiana" },
                    { 20, "82805 Emard Terrace", null, null, "Port Efren", null, "62017-1868", "Ohio" },
                    { 21, "9293 Orn Lodge", null, null, "Wilhelmton", null, "86554", "Alabama" },
                    { 22, "46060 Devon Mall", "Apt. 040", null, "Dorthyborough", null, "06912", "North Carolina" },
                    { 23, "1291 Jerad Shoal", "Suite 489", null, "Fritschfurt", null, "86501-7539", "Idaho" },
                    { 24, "9385 Aurelia Unions", null, null, "East Jaiden", null, "87133", "Oregon" },
                    { 25, "07172 Moen Pines", "Suite 643", null, "Crookston", null, "27751", "Oregon" },
                    { 26, "101 Dixie Oval", "Suite 763", null, "South Nataliemouth", null, "26717-3673", "Delaware" },
                    { 27, "47378 Leffler Center", "Apt. 175", null, "Florinefurt", null, "24559-2405", "Massachusetts" },
                    { 28, "972 Koelpin Ranch", null, null, "Terryshire", null, "12536-2817", "North Dakota" },
                    { 29, "785 Rutherford Views", "Suite 360", null, "Haleyburgh", null, "40466", "Maine" },
                    { 30, "826 Raven Village", null, null, "Johnathanbury", null, "49604", "New York" },
                    { 31, "1303 Bonita Grove", "Suite 320", null, "Strosinfurt", null, "05450-7878", "Ohio" },
                    { 32, "283 Wunsch Glen", null, null, "Lake Demond", null, "47648", "Vermont" },
                    { 33, "82344 VonRueden Course", null, null, "Rowefort", null, "97181", "New Hampshire" },
                    { 34, "91214 Imelda Isle", "Suite 336", null, "West Joshua", null, "04053-7902", "Connecticut" },
                    { 35, "5509 Cremin Spur", null, null, "Jacobsonfurt", null, "07519", "Massachusetts" },
                    { 36, "16900 Elton Ramp", "Apt. 901", null, "Port Isabelbury", null, "29321-6380", "Minnesota" },
                    { 37, "74238 Theo Avenue", "Apt. 430", null, "Heathcoteport", null, "02173-8070", "Texas" },
                    { 38, "5961 Antwon Burgs", null, null, "Aldenville", null, "44714-9921", "West Virginia" },
                    { 39, "1730 Lolita Plaza", null, null, "West Wilfredland", null, "57191", "Kansas" },
                    { 40, "002 Gislason Trafficway", null, null, "New Elyseville", null, "64415-8486", "West Virginia" },
                    { 41, "5249 Keebler Fall", null, null, "East Makaylaburgh", null, "78474-2694", "Pennsylvania" },
                    { 42, "2211 Dessie Key", null, null, "Lake Guillermoview", null, "99578", "Georgia" },
                    { 43, "98397 Arlo Wells", null, null, "Ankundingmouth", null, "05530", "Mississippi" },
                    { 44, "386 O'Hara Mill", null, null, "Port Sincerestad", null, "39351-3930", "Connecticut" },
                    { 45, "091 Laurine Street", "Apt. 644", null, "New Guillermoside", null, "45154-0916", "Texas" },
                    { 46, "79604 Joyce Walks", "Apt. 017", null, "O'Konland", null, "61250-9452", "Nebraska" },
                    { 47, "8939 West Garden", null, null, "New Scottie", null, "55784-3000", "Iowa" },
                    { 48, "4193 Ebony Hills", "Apt. 895", null, "Madisonview", null, "88816-6061", "Missouri" },
                    { 49, "9544 Friesen Stravenue", "Suite 951", null, "Lake Gussie", null, "00298-3129", "Vermont" },
                    { 50, "0285 Hammes Shoals", null, null, "New Kelvin", null, "36428-0604", "Arkansas" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AddressId", "CompanyName", "ContactName", "Phone" },
                values: new object[,]
                {
                    { 1, 1, "Macejkovic - Blanda", "Reynold Davis", "425-339-6282" },
                    { 2, 2, "Ryan - Wolff", "Minnie Kessler", null },
                    { 3, 3, "Herman Inc", "Austin Beatty", "490-676-0072" },
                    { 4, 4, "Koepp - Cummings", "Caleb Moen", "747-511-2448" },
                    { 5, 5, "Schulist LLC", "Sasha Pagac", "322-706-6268" },
                    { 6, 6, "Lang, Dickens and White", "Manuela Wiegand", "597-528-0356" },
                    { 7, 7, "Stamm, Ernser and Cronin", "Francesca Murazik", "489-850-7213" },
                    { 8, 8, "Welch - Frami", "Stephanie Blanda", "419-498-7195" },
                    { 9, 9, "Haag, Senger and Nienow", "Myron Herzog", "917-809-4141" },
                    { 10, 10, "Schmitt, Mann and Johnston", "Abbie Stokes", null },
                    { 11, 11, "VonRueden, Gutmann and Parker", "Josefina Cormier", "272-524-7846" },
                    { 12, 12, "Cartwright - Welch", "Kale Sawayn", "577-492-2110" },
                    { 13, 13, "D'Amore - Mann", "Santino Streich", "459-725-2425" },
                    { 14, 14, "Runolfsdottir, Haley and Stracke", "Brionna Lang", "309-571-8161" },
                    { 15, 15, "Hagenes - O'Connell", "Terrence Schamberger", "544-255-8924" },
                    { 16, 16, "Howell and Sons", "Randall Dibbert", "790-989-5468" },
                    { 17, 17, "Bogan - Veum", "Whitney Ward", "432-236-9660" },
                    { 18, 18, "Schmidt Group", "Oda Frami", "484-268-9449" },
                    { 19, 19, "Upton, Kerluke and Trantow", "Eriberto Kreiger", "529-857-9934" },
                    { 20, 20, "Fahey LLC", "Sallie Bergstrom", "500-252-4197" },
                    { 21, 21, "Funk, Rodriguez and D'Amore", "Carmela Howe", "804-428-2843" },
                    { 22, 22, "Gaylord and Sons", "Erin VonRueden", "738-639-1310" },
                    { 23, 23, "Block - Parker", "Kurt Hagenes", null },
                    { 24, 24, "Borer, Wuckert and Osinski", "Kamille Bernier", "928-630-9163" },
                    { 25, 25, "Smith Group", "Pietro Rohan", "248-600-0309" },
                    { 26, 26, "Gibson, Gislason and Cummings", "Gilberto Dicki", "403-242-7411" },
                    { 27, 27, "Donnelly, Abernathy and Gutkowski", "Edgardo Davis", "226-509-5873" },
                    { 28, 28, "Towne and Sons", "Carolina Schroeder", "977-635-2648" },
                    { 29, 29, "Konopelski, Stroman and Yost", "Shany Little", "271-352-1557" },
                    { 30, 30, "Hegmann, Green and Schmidt", "Baron Carroll", null },
                    { 31, 31, "Hettinger, Trantow and Hermann", "Adrien Macejkovic", "846-332-5288" },
                    { 32, 32, "Boehm, Lesch and Blanda", "Martina Heidenreich", null },
                    { 33, 33, "Dach, Shields and Mills", "Reuben Schaefer", "564-692-9396" },
                    { 34, 34, "Corkery, Ratke and Wuckert", "Armand Armstrong", "465-854-9002" },
                    { 35, 35, "Pfannerstill - Abernathy", "Marley Barton", null },
                    { 36, 36, "Welch - Cormier", "Effie Casper", "747-312-9174" },
                    { 37, 37, "Langworth, Swaniawski and Fritsch", "Margot Farrell", "765-801-7539" },
                    { 38, 38, "Green - Botsford", "Tara Witting", "455-899-0032" },
                    { 39, 39, "Haley, Schultz and Runte", "Beatrice Heidenreich", "526-466-0717" },
                    { 40, 40, "O'Kon, Krajcik and Harris", "Waylon Zboncak", "371-227-7518" },
                    { 41, 41, "Gleichner LLC", "Carson Beer", "276-937-2577" },
                    { 42, 42, "Grant, Paucek and Sauer", "Brayan Rohan", "473-711-5504" },
                    { 43, 43, "Runolfsson - Treutel", "Colleen Cartwright", "755-636-4824" },
                    { 44, 44, "Wolf - Grant", "Jennings Bahringer", "699-446-9720" },
                    { 45, 45, "Jakubowski Inc", "Wilford Terry", "951-725-3628" },
                    { 46, 46, "McLaughlin, Yost and Lubowitz", "Oliver Weimann", "859-836-0673" },
                    { 47, 47, "Brakus - Klocko", "Antone Kiehn", "727-738-9826" },
                    { 48, 48, "Cummings - Durgan", "Dax Lind", "663-549-6045" },
                    { 49, 49, "Cartwright and Sons", "Brianne Howell", "293-420-5986" },
                    { 50, 50, "Bayer - Luettgen", "Hulda Marks", "287-787-9792" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");

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

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
