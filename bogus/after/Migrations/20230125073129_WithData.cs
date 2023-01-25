using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FakingIt.Migrations
{
    /// <inheritdoc />
    public partial class WithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false)
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
                    { 51, "629 Muller Ridges", null, null, "East Orie", null, "08923", "South Carolina" },
                    { 52, "1721 Gusikowski Trace", "Suite 738", null, "Port Eusebio", null, "49249", "Wisconsin" },
                    { 53, "51423 Adelle Rest", null, null, "Jastburgh", null, "50403", "Oregon" },
                    { 54, "300 Rath Station", "Suite 177", null, "Jonathanfort", null, "41319", "Indiana" },
                    { 55, "2406 Lydia Meadow", null, null, "Kilbacktown", null, "82606-7232", "Nebraska" },
                    { 56, "72277 Weber Flat", null, null, "North Eliane", null, "56363-3794", "New Mexico" },
                    { 57, "065 Salma Crossing", "Suite 019", null, "Evashire", null, "26754", "Delaware" },
                    { 58, "259 Jacobs Ramp", "Suite 383", null, "Port Fabiola", null, "73246", "West Virginia" },
                    { 59, "953 Madison Forges", null, null, "New Whitney", null, "12673", "Idaho" },
                    { 60, "2921 Alberto Green", "Apt. 084", null, "Edgarshire", null, "14268-9235", "Mississippi" },
                    { 61, "54009 Arch Freeway", null, null, "Port Kaitlin", null, "69176", "Georgia" },
                    { 62, "008 Waino Gateway", null, null, "Vestaville", null, "02847", "New Mexico" },
                    { 63, "09184 Koch Club", null, null, "Torphyville", null, "61499", "Hawaii" },
                    { 64, "57023 Reichert Crest", "Suite 359", null, "Port Dock", null, "84054-1061", "New Mexico" },
                    { 65, "899 Upton Loop", "Apt. 796", null, "Lake Eliezer", null, "24879", "Massachusetts" },
                    { 66, "4394 Rachael Course", "Apt. 916", null, "North Arden", null, "45781", "Vermont" },
                    { 67, "82338 Adelbert Terrace", "Suite 520", null, "Cartermouth", null, "71139-2153", "Mississippi" },
                    { 68, "6813 Judy Brooks", "Apt. 685", null, "Willardshire", null, "09272", "South Dakota" },
                    { 69, "0667 Anthony Valleys", "Suite 048", null, "West Virgil", null, "68713-4393", "Louisiana" },
                    { 70, "8266 Ashley Plains", "Suite 224", null, "New Mckaylaburgh", null, "56163", "Colorado" },
                    { 71, "6512 Daniela Plains", "Apt. 359", null, "Lake Louveniachester", null, "61875", "Georgia" },
                    { 72, "53875 Theodora Summit", null, null, "Alyceberg", null, "07402-9994", "Missouri" },
                    { 73, "943 Sawayn Rest", "Apt. 499", null, "West Kendrick", null, "60469", "West Virginia" },
                    { 74, "169 Trisha Route", "Suite 339", null, "Wuckertchester", null, "38857-3405", "California" },
                    { 75, "191 Shaylee Cliffs", null, null, "Jammiebury", null, "27695", "New Jersey" },
                    { 76, "8104 Pinkie Divide", null, null, "Daphneyhaven", null, "14092-2312", "Ohio" },
                    { 77, "16503 Sammie Inlet", "Suite 371", null, "South Eldonville", null, "14098", "Wyoming" },
                    { 78, "8791 Reyna Pine", null, null, "Addieville", null, "44257-6911", "Nebraska" },
                    { 79, "501 Heaney View", "Apt. 279", null, "Bahringerburgh", null, "31200-9144", "Utah" },
                    { 80, "658 Conn Rapids", null, null, "New Westonshire", null, "86229-9508", "South Dakota" },
                    { 81, "032 Ernser Lights", "Apt. 002", null, "Morissettemouth", null, "80600-1167", "Wisconsin" },
                    { 82, "6676 Emmerich Courts", "Suite 633", null, "New Sylvan", null, "87659-9792", "Rhode Island" },
                    { 83, "375 Bergnaum Squares", null, null, "Port Breannaburgh", null, "32413", "Nevada" },
                    { 84, "824 Sabina Curve", "Suite 479", null, "Laishaton", null, "00805-5736", "Washington" },
                    { 85, "64502 Weissnat Mount", "Suite 476", null, "Schulistbury", null, "69444", "Wyoming" },
                    { 86, "41718 Keeling Drive", null, null, "North Kaycee", null, "43430", "South Carolina" },
                    { 87, "48241 Champlin Bypass", "Suite 918", null, "Nadermouth", null, "92168", "South Dakota" },
                    { 88, "9613 Huels Brook", null, null, "West Rico", null, "85869", "Alaska" },
                    { 89, "140 Mueller Vista", null, null, "Kozeyville", null, "84439-4490", "Washington" },
                    { 90, "76121 Gislason Rue", null, null, "West Maurice", null, "05988", "Iowa" },
                    { 91, "31902 Waelchi Station", null, null, "Maraberg", null, "46519-6717", "Wyoming" },
                    { 92, "641 Vincenza Lock", null, null, "New Ernestoville", null, "97339", "Illinois" },
                    { 93, "0169 Luna Lakes", null, null, "Agustinahaven", null, "82024", "Maryland" },
                    { 94, "207 Kassulke Route", null, null, "Prosaccoberg", null, "89104", "Iowa" },
                    { 95, "9478 Boyle Grove", null, null, "Tremaynechester", null, "27258", "Georgia" },
                    { 96, "123 Kellen Court", "Suite 377", null, "Lake Rhiannonhaven", null, "02109", "Massachusetts" },
                    { 97, "86611 Ralph Turnpike", "Apt. 679", null, "Rolandofurt", null, "49154", "Texas" },
                    { 98, "58629 Caleigh Meadows", null, null, "Boscofurt", null, "11973", "Nevada" },
                    { 99, "6963 Rosanna Manors", "Apt. 525", null, "Derickshire", null, "67328-4326", "Mississippi" },
                    { 100, "73057 Ferne Place", null, null, "Langmouth", null, "97160", "Nevada" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AddressId", "CompanyName", "ContactName", "Phone" },
                values: new object[,]
                {
                    { 51, 51, "Howe, Ortiz and Ritchie", "Vito Lehner", "382-582-4764" },
                    { 52, 52, "Ankunding Group", "Tessie Braun", "834-345-3323" },
                    { 53, 53, "Schimmel, Mayer and Douglas", "Telly Ratke", "215-831-4391" },
                    { 54, 54, "Kovacek Group", "Emerson Heller", "743-651-6040" },
                    { 55, 55, "Reichert Group", "Ashton Gerhold", "991-675-5090" },
                    { 56, 56, "Bergstrom - Davis", "Ona Jacobson", null },
                    { 57, 57, "Osinski Inc", "Madalyn Willms", "229-201-5194" },
                    { 58, 58, "Okuneva - Goldner", "Zoe Haley", null },
                    { 59, 59, "Jenkins, Feeney and Hegmann", "Reymundo Johnston", "509-483-4890" },
                    { 60, 60, "Rolfson and Sons", "Selena Blick", null },
                    { 61, 61, "Stoltenberg Group", "Nicklaus Boyle", "619-294-4714" },
                    { 62, 62, "Fay, Crist and McClure", "Laurianne Champlin", "717-730-4259" },
                    { 63, 63, "Harris Group", "Britney Hagenes", "371-691-3672" },
                    { 64, 64, "Beer and Sons", "Penelope Bartell", "621-263-1976" },
                    { 65, 65, "Davis - Maggio", "Rubye Kuhn", "805-784-2524" },
                    { 66, 66, "Doyle and Sons", "Daphnee Friesen", "363-379-7847" },
                    { 67, 67, "Pfannerstill Group", "Jermain Fay", "523-522-1187" },
                    { 68, 68, "Hamill Inc", "Nathaniel Jakubowski", "529-395-7891" },
                    { 69, 69, "Zieme Group", "Reymundo Haley", "978-808-0905" },
                    { 70, 70, "Bosco LLC", "Kelley Hamill", "285-265-3864" },
                    { 71, 71, "Blick Inc", "Bianka Towne", "915-639-3513" },
                    { 72, 72, "Block - Raynor", "Nickolas Hilpert", "609-813-6448" },
                    { 73, 73, "Jakubowski - Von", "Roy VonRueden", "515-640-9162" },
                    { 74, 74, "Ledner Group", "Orie Walker", null },
                    { 75, 75, "Roberts - Osinski", "Clark Gibson", "694-252-9589" },
                    { 76, 76, "Ziemann, Hickle and Williamson", "Jadyn Bernier", "434-268-2855" },
                    { 77, 77, "Jenkins, Hessel and Bailey", "Alycia Bergstrom", "802-534-1930" },
                    { 78, 78, "Mayer, Lynch and Predovic", "Madison Koch", null },
                    { 79, 79, "Kuhic, McDermott and Friesen", "Reina Ward", "647-595-1527" },
                    { 80, 80, "Yundt, Balistreri and Botsford", "Darryl Wilderman", null },
                    { 81, 81, "Steuber LLC", "Logan Botsford", "712-965-0636" },
                    { 82, 82, "Schneider Inc", "Lorenza Bashirian", "578-666-6291" },
                    { 83, 83, "Dickens, Haley and Johnston", "Devin Sanford", "808-492-3259" },
                    { 84, 84, "Dibbert, Schaefer and Grant", "Beryl Wiza", "787-449-3924" },
                    { 85, 85, "Koelpin Group", "Clementina Corwin", "565-214-2350" },
                    { 86, 86, "Herzog - Schinner", "Hailie Haley", "750-340-3369" },
                    { 87, 87, "Herman, Bergstrom and Bogisich", "Thalia Breitenberg", "775-758-2041" },
                    { 88, 88, "Yost LLC", "Jarod Bergstrom", "724-506-4629" },
                    { 89, 89, "Kilback, Baumbach and Lemke", "Yoshiko Stiedemann", "306-772-3200" },
                    { 90, 90, "Fadel, Quigley and Gulgowski", "Daryl Ritchie", "784-600-3026" },
                    { 91, 91, "Murazik - Hills", "Lenore Jacobs", "494-749-2810" },
                    { 92, 92, "Rohan - Blanda", "Clotilde Walter", "639-713-2675" },
                    { 93, 93, "Rutherford - Schamberger", "Gregory Padberg", "499-638-3739" },
                    { 94, 94, "Hilll, Schiller and Hayes", "Damien Jenkins", "790-962-9536" },
                    { 95, 95, "O'Conner - Borer", "Felipe Orn", "921-326-7356" },
                    { 96, 96, "Grimes and Sons", "Vesta Douglas", "308-346-6294" },
                    { 97, 97, "Johns and Sons", "Mallory Torphy", "935-410-1025" },
                    { 98, 98, "Block Group", "Cordell Borer", "734-795-1369" },
                    { 99, 99, "Nader, Lind and Wyman", "Elsa Wisozk", "408-240-1005" },
                    { 100, 100, "Monahan Inc", "Anabelle Gerhold", "979-528-4009" }
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
