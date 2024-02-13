using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AddressBook.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityTown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookEntryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_BookEntries_BookEntryId",
                        column: x => x.BookEntryId,
                        principalTable: "BookEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BookEntries",
                columns: new[] { "Id", "CellPhone", "CompanyName", "DateOfBirth", "Email", "FirstName", "Gender", "HomePhone", "LastName", "MiddleName", "UserName", "WorkPhone" },
                values: new object[,]
                {
                    { 26, "830-799-4314", "Daugherty - Yundt", new DateTime(1957, 9, 1, 12, 37, 47, 500, DateTimeKind.Local).AddTicks(1217), "Nikko7@yahoo.com", "Nikko", "Female", "321-564-9581", "Walsh", "Fanny", null, "868-806-1235" },
                    { 27, "381-428-1380", "Weissnat, Jacobs and Klein", new DateTime(2003, 10, 12, 16, 21, 10, 471, DateTimeKind.Local).AddTicks(5759), "Jeanne48@hotmail.com", "Jeanne", "Male", "403-464-9914", "O'Keefe", "Randi", null, null },
                    { 28, null, "Stroman - Hamill", new DateTime(1987, 2, 12, 19, 0, 10, 778, DateTimeKind.Local).AddTicks(2899), "Shannon_DuBuque72@hotmail.com", "Shannon", "Male", "688-943-1740", "DuBuque", "Wilfred", null, "416-378-0335" },
                    { 29, null, "Yost Inc", new DateTime(1999, 9, 16, 4, 26, 51, 675, DateTimeKind.Local).AddTicks(2747), "Skylar45@yahoo.com", "Skylar", "Male", null, "Hessel", "Serenity", null, "766-525-8579" },
                    { 30, null, "Kirlin, Schamberger and Nicolas", new DateTime(1955, 5, 29, 19, 23, 24, 732, DateTimeKind.Local).AddTicks(6598), "Moises_Lowe@gmail.com", "Moises", "Female", "317-365-5789", "Lowe", "Cheyenne", null, "698-335-3493" },
                    { 31, null, "Murphy Inc", new DateTime(1987, 4, 29, 4, 6, 12, 611, DateTimeKind.Local).AddTicks(1569), "Samanta57@yahoo.com", "Samanta", "Female", "858-996-0082", "Smith", "Erik", null, null },
                    { 32, "862-507-6899", "Feeney and Sons", new DateTime(1976, 5, 1, 18, 1, 36, 242, DateTimeKind.Local).AddTicks(4358), "Graham28@hotmail.com", "Graham", "Male", "370-498-2324", "Konopelski", "Ben", null, "853-537-0071" },
                    { 33, null, "Johnston, Schulist and Howell", new DateTime(1996, 9, 20, 7, 29, 26, 455, DateTimeKind.Local).AddTicks(6507), "Mikel_Cruickshank@gmail.com", "Mikel", "Male", "511-755-0362", "Cruickshank", "Margarita", null, null },
                    { 34, null, "Hettinger and Sons", new DateTime(1961, 8, 27, 11, 36, 17, 352, DateTimeKind.Local).AddTicks(2376), "Mariane63@yahoo.com", "Mariane", "Male", null, "Fadel", "Geovanny", null, null },
                    { 35, "860-839-7464", "Schinner, Goldner and Schimmel", new DateTime(1976, 4, 28, 11, 22, 56, 161, DateTimeKind.Local).AddTicks(9473), "Kamryn.Boyle13@hotmail.com", "Kamryn", "Male", null, "Boyle", "Russ", null, null },
                    { 36, null, "Lind - Mosciski", new DateTime(1996, 11, 10, 14, 4, 43, 206, DateTimeKind.Local).AddTicks(2316), "Edna63@gmail.com", "Edna", "Male", null, "Barrows", "Ellis", null, null },
                    { 37, null, "Wuckert - Bergnaum", new DateTime(1971, 12, 25, 19, 15, 6, 468, DateTimeKind.Local).AddTicks(225), "Fannie_Luettgen@hotmail.com", "Fannie", "Female", "921-502-3443", "Luettgen", "Jessica", null, "937-242-8137" },
                    { 38, null, "Bergstrom - Price", new DateTime(1978, 1, 3, 13, 58, 49, 765, DateTimeKind.Local).AddTicks(1870), "Leonor.Brown84@yahoo.com", "Leonor", "Female", "841-455-9375", "Brown", "Andres", null, null },
                    { 39, null, "Bailey Inc", new DateTime(1980, 3, 31, 10, 43, 13, 371, DateTimeKind.Local).AddTicks(8492), "Judge_Wisoky@hotmail.com", "Judge", "Female", "210-905-0189", "Wisoky", "Kali", null, null },
                    { 40, null, "Gulgowski Group", new DateTime(1988, 8, 6, 7, 41, 7, 875, DateTimeKind.Local).AddTicks(8980), "Cordelia_Halvorson92@yahoo.com", "Cordelia", "Female", null, "Halvorson", "Elliott", null, null },
                    { 41, "364-666-5317", "Wisoky LLC", new DateTime(1993, 3, 16, 10, 37, 17, 151, DateTimeKind.Local).AddTicks(1165), "Citlalli.Rogahn61@hotmail.com", "Citlalli", "Male", null, "Rogahn", "May", null, null },
                    { 42, "491-764-8356", "Stokes, Bradtke and Mills", new DateTime(1957, 12, 20, 12, 58, 28, 698, DateTimeKind.Local).AddTicks(6247), "Lea.Grant41@gmail.com", "Lea", "Male", "792-950-9751", "Grant", "Winifred", null, null },
                    { 43, null, "Crooks - Leffler", new DateTime(1991, 1, 13, 7, 10, 47, 420, DateTimeKind.Local).AddTicks(9125), "Vernon87@gmail.com", "Vernon", "Male", "995-660-2184", "Runolfsson", "Axel", null, null },
                    { 44, "213-581-9061", "Moen, Considine and Ortiz", new DateTime(1955, 7, 25, 15, 45, 34, 578, DateTimeKind.Local).AddTicks(889), "Jess.Leffler@yahoo.com", "Jess", "Male", "266-460-0625", "Leffler", "Bennie", null, null },
                    { 45, null, "Hauck - Will", new DateTime(1975, 8, 28, 13, 4, 9, 322, DateTimeKind.Local).AddTicks(3950), "Arno.Kling23@gmail.com", "Arno", "Male", null, "Kling", "Kaylie", null, "914-995-3197" },
                    { 46, null, "Weimann, Stroman and Koch", new DateTime(1958, 8, 8, 20, 49, 46, 868, DateTimeKind.Local).AddTicks(9749), "Mitchel.Wuckert@hotmail.com", "Mitchel", "Male", "472-337-5097", "Wuckert", "Angelina", null, null },
                    { 47, null, "Weber - Crist", new DateTime(1982, 10, 10, 19, 3, 44, 884, DateTimeKind.Local).AddTicks(8441), "Raul_Effertz82@gmail.com", "Raul", "Female", null, "Effertz", "Emilio", null, "587-353-3155" },
                    { 48, "666-270-9743", "Kunze, Medhurst and Abshire", new DateTime(1983, 7, 16, 5, 47, 6, 320, DateTimeKind.Local).AddTicks(5316), "Antwon.Kassulke@gmail.com", "Antwon", "Male", null, "Kassulke", "Lowell", null, null },
                    { 49, "565-482-0966", "Stiedemann - Aufderhar", new DateTime(1997, 5, 13, 19, 39, 22, 972, DateTimeKind.Local).AddTicks(5367), "Richmond28@gmail.com", "Richmond", "Female", "494-280-2551", "Ferry", "Shane", null, null },
                    { 50, null, "Cruickshank LLC", new DateTime(1997, 4, 5, 12, 2, 38, 427, DateTimeKind.Local).AddTicks(5967), "Camden91@yahoo.com", "Camden", "Female", "374-855-5304", "Crona", "Joana", null, null }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "BookEntryId", "CityTown", "Country", "Line1", "Line2", "Line3", "Name", "PostalCode", "StateProvince" },
                values: new object[,]
                {
                    { 51, 26, "South Daniela", "Qatar", "906 Corene Lodge", "Suite 210", "Suite 163", "Auto Loan Account modular", "51243-5980", "New York" },
                    { 52, 26, "Alyceberg", "Reunion", "61298 Purdy Mountains", "Suite 387", "Apt. 740", "Credit Card Account yellow", "07402-9994", "Missouri" },
                    { 53, 27, "Kiehnside", "Virgin Islands, U.S.", "9972 Dayna Knoll", "Apt. 592", "Apt. 469", "Customer synthesize", "69633", "Oregon" },
                    { 54, 27, "Fritschtown", "Falkland Islands (Malvinas)", "5734 Quinton Stravenue", null, "Suite 019", "Personal Loan Account Developer", "64354", "Pennsylvania" },
                    { 55, 28, "South Augustusside", "Haiti", "8104 Pinkie Divide", null, "Suite 286", "alarm Designer", "31291", "Hawaii" },
                    { 56, 28, "South Reyna", "Albania", "515 Meaghan Drive", null, "Apt. 098", "PCI Generic Granite Chair", "79122-7917", "New Jersey" },
                    { 57, 29, "Noetown", "Isle of Man", "91117 Satterfield Rapids", null, "Apt. 027", "sky blue District", "73120-0914", "Indiana" },
                    { 58, 29, "Port Theodora", "Jamaica", "598 Schuppe Knolls", "Apt. 059", "Suite 986", "reboot Belize Dollar", "80782", "New Hampshire" },
                    { 59, 30, "Emmerichland", "Tunisia", "67998 Willy Views", null, "Apt. 675", "Incredible pink", "76663-3736", "New York" },
                    { 60, 30, "East Uriah", "Botswana", "9297 Wunsch Road", "Suite 073", "Suite 589", "synthesize USB", "54324", "Connecticut" },
                    { 61, 31, "Maymiebury", "Isle of Man", "64798 Feil Knoll", "Suite 509", "Apt. 080", "web-enabled back up", "26956", "North Dakota" },
                    { 62, 31, "South Carmelmouth", "Algeria", "839 Leffler Walk", null, "Apt. 305", "Burgs upward-trending", "10523-4057", "Florida" },
                    { 63, 32, "Raphaelborough", "New Zealand", "048 Meagan Creek", "Apt. 415", "Apt. 877", "Isle Groves", "16864", "Virginia" },
                    { 64, 32, "Muellershire", "Central African Republic", "6133 Evie Locks", "Suite 018", "Suite 692", "synergies Orchestrator", "00340", "Delaware" },
                    { 65, 33, "East Sophiaborough", "United States of America", "4902 Zachariah Lane", "Apt. 827", null, "indexing Unbranded Metal Bacon", "62205", "Maryland" },
                    { 66, 33, "Port Mariannachester", "Iran", "2920 Zetta Centers", null, "Suite 946", "SAS Intelligent Granite Sausages", "06094-6412", "Georgia" },
                    { 67, 34, "Mayertside", "Ukraine", "97339 Dena Avenue", null, "Apt. 169", "program Customer-focused", "08348-2024", "Illinois" },
                    { 68, 34, "Boyleburgh", "Nepal", "91287 Hessel Creek", "Apt. 218", "Apt. 049", "1080p mindshare", "78097", "Virginia" },
                    { 69, 35, "Lake Rhiannonhaven", "Micronesia", "58961 Pearlie Forge", null, "Suite 377", "next generation Quality", "02109", "Massachusetts" },
                    { 70, 35, "East Jeffrey", "Benin", "79678 Bernadine Plain", "Suite 824", "Apt. 544", "Intelligent Soft Salad generating", "62999-4407", "New Hampshire" },
                    { 71, 36, "Port Maritza", "Niger", "8469 Kaylah Glens", null, "Suite 662", "payment Rustic Wooden Chips", "28432", "Rhode Island" },
                    { 72, 36, "New Amariton", "Cayman Islands", "773 Aimee Overpass", "Suite 309", "Suite 509", "French Guiana forecast", "34304-3101", "New Jersey" },
                    { 73, 37, "Ashleebury", "Benin", "43339 Mills Route", null, "Suite 351", "Auto Loan Account distributed", "61973-9763", "North Dakota" },
                    { 74, 37, "Bayleeburgh", "British Indian Ocean Territory (Chagos Archipelago)", "2164 Crooks Lodge", "Suite 581", "Suite 868", "Chief Isle", "99053-6278", "Louisiana" },
                    { 75, 38, "East Jakob", "Palestinian Territory", "248 Rohan Circle", null, "Suite 728", "Shoes Chief", "28670-9571", "Florida" },
                    { 76, 38, "Trudiebury", "Burundi", "86811 Jared Expressway", "Suite 081", "Apt. 742", "Dynamic Security", "86138", "Minnesota" },
                    { 77, 39, "Yostbury", "Palestinian Territory", "64991 Alexa Isle", null, "Apt. 835", "gold Fantastic Fresh Computer", "53062", "Hawaii" },
                    { 78, 39, "Faheymouth", "Niger", "955 Flavie Freeway", "Suite 245", "Suite 797", "hack red", "58342", "Vermont" },
                    { 79, 40, "Gleasonburgh", "Costa Rica", "39347 Reece Bridge", null, "Suite 389", "Plaza Plains", "59529", "Tennessee" },
                    { 80, 40, "Vidafurt", "Greenland", "08311 Otto Meadows", null, "Apt. 563", "Personal Loan Account SCSI", "34045", "Maine" },
                    { 81, 41, "New Olaf", "Libyan Arab Jamahiriya", "09168 Jena Fall", "Suite 049", "Suite 952", "Rustic Views", "58868", "Kansas" },
                    { 82, 41, "Port Hassie", "Guam", "7895 Cary Mountains", "Suite 391", "Suite 358", "Money Market Account Fresh", "62148", "South Carolina" },
                    { 83, 42, "Reingerchester", "Jamaica", "46625 Heller Roads", "Suite 579", "Apt. 978", "Soft scalable", "36401-5460", "South Carolina" },
                    { 84, 42, "Lake Hayleefort", "Marshall Islands", "504 Carey Flat", null, "Suite 717", "hard drive Devolved", "19397-4086", "Hawaii" },
                    { 85, 43, "East Declanland", "Somalia", "76715 Rhoda Locks", "Suite 872", "Apt. 706", "Accounts payment", "55789-9519", "Oregon" },
                    { 86, 43, "Feeneyton", "Palestinian Territory", "9773 Sanford Well", null, "Suite 293", "engage synergize", "50252", "Wisconsin" },
                    { 87, 44, "Jaskolskiport", "Sri Lanka", "7140 Percy Tunnel", "Apt. 856", "Suite 046", "Rustic Metal Bike Russian Ruble", "62572-6183", "Hawaii" },
                    { 88, 44, "East Mabelberg", "Algeria", "4179 Koss Forks", null, "Apt. 829", "generate orange", "49918", "Minnesota" },
                    { 89, 45, "East Darianville", "Libyan Arab Jamahiriya", "883 Emmerich Run", null, "Suite 457", "user-centric lime", "85261", "Mississippi" },
                    { 90, 45, "Barrowsport", "Denmark", "323 Nikita Motorway", "Apt. 504", "Apt. 622", "Avon Berkshire", "73152", "Montana" },
                    { 91, 46, "South Rhea", "Belize", "7689 Fay Bypass", "Suite 913", "Apt. 982", "Berkshire Practical", "53370", "Texas" },
                    { 92, 46, "Lake Addieshire", "South Africa", "311 Adolfo Drives", null, "Apt. 533", "override Solutions", "66398", "New Jersey" },
                    { 93, 47, "North Brownhaven", "Central African Republic", "322 Adalberto Crescent", null, "Suite 751", "Handmade Concrete Gloves navigate", "92274-8376", "Maine" },
                    { 94, 47, "New Alexane", "Denmark", "634 Halvorson Harbor", "Suite 115", "Apt. 362", "Mountain Small", "48065", "Vermont" },
                    { 95, 48, "South Wilfordfort", "Malta", "5714 Estevan Stream", null, "Apt. 399", "Gorgeous turquoise", "42072", "North Dakota" },
                    { 96, 48, "New Lorenzton", "Romania", "74529 Garrett Landing", null, "Apt. 171", "Chief Cotton", "63202", "Maine" },
                    { 97, 49, "West Dylantown", "Gambia", "4574 Eleanora Burg", null, "Apt. 171", "Somoni withdrawal", "03270", "Colorado" },
                    { 98, 49, "New Torrance", "Switzerland", "698 Collier Trace", null, "Suite 546", "Handcrafted Fresh Tuna Oregon", "02031-3073", "Hawaii" },
                    { 99, 50, "New Shaunfort", "Saint Barthelemy", "1216 Bosco Ferry", null, "Suite 398", "web services salmon", "58087-6039", "Indiana" },
                    { 100, 50, "West Marcella", "Netherlands Antilles", "237 Nienow Landing", "Apt. 808", "Apt. 199", "Buckinghamshire Buckinghamshire", "28913", "Iowa" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_BookEntryId",
                table: "Addresses",
                column: "BookEntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "BookEntries");
        }
    }
}
