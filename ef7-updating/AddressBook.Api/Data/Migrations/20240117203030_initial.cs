using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AddressBook.Api.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
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
                    { 101, null, "Ullrich, Rowe and Waters", new DateTime(1994, 3, 25, 19, 42, 3, 197, DateTimeKind.Local).AddTicks(4600), "Deion_Goodwin@gmail.com", "Deion", null, null, "Goodwin", "Clemmie", null, null },
                    { 102, "908-755-2931", "Lebsack, Pfannerstill and Dicki", new DateTime(1978, 6, 8, 6, 46, 52, 673, DateTimeKind.Local).AddTicks(2127), "Minerva_Rowe@hotmail.com", "Minerva", null, null, "Rowe", "Erwin", null, "887-902-4954" },
                    { 103, null, "Skiles, Wehner and Quigley", new DateTime(2002, 7, 27, 11, 1, 39, 888, DateTimeKind.Local).AddTicks(2509), "Marty_Ritchie18@hotmail.com", "Marty", null, null, "Ritchie", "Gracie", null, "941-263-3754" },
                    { 104, "709-581-1775", "Hegmann Group", new DateTime(1980, 2, 18, 15, 47, 24, 325, DateTimeKind.Local).AddTicks(2339), "Erin.Ziemann@hotmail.com", "Erin", "Male", null, "Ziemann", "Janet", null, "336-604-8959" },
                    { 105, "713-832-4613", "Connelly, Skiles and Hyatt", new DateTime(2000, 10, 17, 5, 50, 18, 430, DateTimeKind.Local).AddTicks(8822), "Jerry93@hotmail.com", "Jerry", null, null, "Howell", "Ellen", null, "896-702-7135" },
                    { 106, null, "Lockman, Padberg and O'Kon", new DateTime(1956, 9, 26, 15, 30, 45, 437, DateTimeKind.Local).AddTicks(2962), "Rocky_Ankunding@gmail.com", "Rocky", "Female", "840-611-7073", "Ankunding", "Sheldon", null, "515-461-0390" },
                    { 107, null, "Parisian, Rohan and Friesen", new DateTime(1987, 5, 29, 5, 7, 20, 415, DateTimeKind.Local).AddTicks(6752), "Charlotte_Sawayn@yahoo.com", "Charlotte", null, null, "Sawayn", "Rocky", null, "585-676-6280" },
                    { 108, "904-401-4522", "Weimann and Sons", new DateTime(1969, 1, 14, 22, 48, 24, 122, DateTimeKind.Local).AddTicks(6960), "Stanford.Murray12@gmail.com", "Stanford", null, "713-847-5422", "Murray", "Donny", null, null },
                    { 109, null, "Brekke Group", new DateTime(2001, 4, 18, 5, 55, 31, 564, DateTimeKind.Local).AddTicks(5673), "Kiera_Ortiz@hotmail.com", "Kiera", null, "801-876-1113", "Ortiz", "Elmore", null, null },
                    { 110, "927-748-5153", "Torp - Larson", new DateTime(1958, 7, 3, 20, 32, 56, 610, DateTimeKind.Local).AddTicks(5096), "Freeda.Schmeler@gmail.com", "Freeda", "Male", "239-856-6524", "Schmeler", "Hoyt", null, "333-411-5856" },
                    { 111, null, "Muller Inc", new DateTime(1995, 8, 20, 8, 10, 36, 322, DateTimeKind.Local).AddTicks(4203), "Angelica.Turner@gmail.com", "Angelica", null, null, "Turner", "Macey", null, "236-469-7755" },
                    { 112, "908-943-1439", "Fisher LLC", new DateTime(1974, 10, 21, 15, 30, 35, 489, DateTimeKind.Local).AddTicks(4482), "Rey81@gmail.com", "Rey", "Male", null, "Rowe", "Cyrus", null, "960-552-6814" },
                    { 113, "266-660-2918", "Gerhold, Willms and Daniel", new DateTime(1998, 4, 18, 5, 47, 37, 559, DateTimeKind.Local).AddTicks(3901), "Emmanuelle76@hotmail.com", "Emmanuelle", null, "304-839-1601", "Gislason", "Davon", null, null },
                    { 114, null, "Bailey and Sons", new DateTime(1962, 2, 12, 4, 39, 2, 533, DateTimeKind.Local).AddTicks(9146), "Zaria99@gmail.com", "Zaria", "Male", "536-563-0228", "Rutherford", "Ronaldo", null, "324-892-2128" },
                    { 115, "487-505-7203", "Lesch Group", new DateTime(1977, 12, 16, 21, 5, 46, 191, DateTimeKind.Local).AddTicks(4655), "King_Pagac@yahoo.com", "King", null, null, "Pagac", "Adolf", null, null },
                    { 116, null, "Koch - Klein", new DateTime(2003, 3, 17, 11, 11, 52, 230, DateTimeKind.Local).AddTicks(361), "Oswald_Olson38@yahoo.com", "Oswald", "Female", null, "Olson", "Mathias", null, "556-938-1157" },
                    { 117, "708-298-4936", "Kshlerin - Kutch", new DateTime(1958, 4, 16, 5, 23, 9, 434, DateTimeKind.Local).AddTicks(2053), "Esta_Torphy3@yahoo.com", "Esta", null, null, "Torphy", "Raul", null, "515-225-2069" },
                    { 118, "445-376-9536", "Stokes - Volkman", new DateTime(1958, 3, 18, 18, 44, 33, 924, DateTimeKind.Local).AddTicks(1059), "Sebastian.Bartell2@gmail.com", "Sebastian", "Female", null, "Bartell", "Eileen", null, null },
                    { 119, null, "Keeling - Quigley", new DateTime(1977, 10, 1, 10, 37, 12, 974, DateTimeKind.Local).AddTicks(2155), "Julio_Leffler@gmail.com", "Julio", "Female", null, "Leffler", "Gustave", null, "536-796-7294" },
                    { 120, "545-381-1242", "Metz, Wyman and Leannon", new DateTime(2001, 7, 23, 18, 11, 28, 939, DateTimeKind.Local).AddTicks(2572), "Rosetta_Graham7@hotmail.com", "Rosetta", "Male", null, "Graham", "Haskell", null, "428-232-2622" },
                    { 121, null, "Pfannerstill LLC", new DateTime(1971, 4, 5, 12, 51, 4, 744, DateTimeKind.Local).AddTicks(9213), "Orie.Veum@gmail.com", "Orie", "Female", "570-687-2924", "Veum", "Nico", null, "354-894-8321" },
                    { 122, null, "Johnston, Hand and Gibson", new DateTime(1991, 7, 10, 18, 11, 34, 383, DateTimeKind.Local).AddTicks(2236), "Enid90@hotmail.com", "Enid", "Female", null, "Stokes", "Colleen", null, null },
                    { 123, null, "Larson LLC", new DateTime(1991, 11, 2, 3, 7, 4, 635, DateTimeKind.Local).AddTicks(4810), "Jakayla.Yundt@hotmail.com", "Jakayla", "Male", "440-333-3046", "Yundt", "Katelin", null, "698-917-8957" },
                    { 124, null, "Lubowitz - Rolfson", new DateTime(1976, 2, 12, 14, 17, 25, 895, DateTimeKind.Local).AddTicks(9561), "Athena_King70@gmail.com", "Athena", null, null, "King", "Travon", null, null },
                    { 125, null, "McLaughlin LLC", new DateTime(1967, 10, 18, 15, 4, 45, 481, DateTimeKind.Local).AddTicks(2960), "Rodrigo_Kassulke@hotmail.com", "Rodrigo", null, null, "Kassulke", "Lina", null, "326-877-1572" },
                    { 126, "873-599-0475", "Dach, Harvey and Kirlin", new DateTime(1974, 2, 28, 4, 40, 48, 565, DateTimeKind.Local).AddTicks(9530), "Marcus_Goodwin@yahoo.com", "Marcus", "Male", "781-807-6411", "Goodwin", "Domenic", null, "490-687-5505" },
                    { 127, null, "Morissette, Wunsch and Hartmann", new DateTime(1981, 8, 21, 17, 36, 10, 659, DateTimeKind.Local).AddTicks(6867), "Paul_Olson@yahoo.com", "Paul", null, "988-294-2230", "Olson", "Eldora", null, null },
                    { 128, "925-408-2706", "Runte LLC", new DateTime(1986, 4, 6, 20, 4, 43, 33, DateTimeKind.Local).AddTicks(9794), "Frances19@hotmail.com", "Frances", "Male", "400-827-7535", "Von", "Alexis", null, "663-952-2445" },
                    { 129, null, "Brakus and Sons", new DateTime(2002, 2, 2, 13, 45, 53, 228, DateTimeKind.Local).AddTicks(7195), "Nikolas61@yahoo.com", "Nikolas", null, "339-677-5182", "Frami", "Myrl", null, "599-624-3116" },
                    { 130, null, "Nolan - Toy", new DateTime(1966, 2, 20, 12, 16, 20, 129, DateTimeKind.Local).AddTicks(39), "Katelin9@yahoo.com", "Katelin", "Female", "629-959-8722", "Wisozk", "Don", null, "527-328-0097" },
                    { 131, null, "Flatley and Sons", new DateTime(1961, 4, 12, 9, 16, 53, 514, DateTimeKind.Local).AddTicks(8621), "Reuben_Moore19@yahoo.com", "Reuben", "Male", null, "Moore", "Catalina", null, null },
                    { 132, null, "Mertz Group", new DateTime(1989, 12, 9, 14, 59, 25, 539, DateTimeKind.Local).AddTicks(3501), "Mikayla_Keeling82@yahoo.com", "Mikayla", "Female", null, "Keeling", "Abbie", null, "407-812-7951" },
                    { 133, "422-468-8115", "Casper, Bode and Hettinger", new DateTime(1975, 2, 18, 4, 45, 51, 673, DateTimeKind.Local).AddTicks(1578), "Torrance_Hamill25@hotmail.com", "Torrance", null, null, "Hamill", "Juwan", null, null },
                    { 134, null, "Konopelski - Satterfield", new DateTime(1954, 9, 25, 22, 51, 11, 201, DateTimeKind.Local).AddTicks(7782), "Jayme.Jaskolski60@hotmail.com", "Jayme", "Male", null, "Jaskolski", "Eva", null, "510-770-2969" },
                    { 135, null, "Cartwright and Sons", new DateTime(1962, 4, 8, 19, 32, 23, 953, DateTimeKind.Local).AddTicks(9381), "Aleen73@hotmail.com", "Aleen", null, "982-693-1909", "Block", "Jamar", null, null },
                    { 136, "824-709-3197", "Ziemann, Rohan and Mills", new DateTime(1968, 11, 24, 0, 11, 26, 232, DateTimeKind.Local).AddTicks(4286), "Christiana.Feil@gmail.com", "Christiana", "Female", null, "Feil", "Germaine", null, null },
                    { 137, null, "Hyatt - DuBuque", new DateTime(1991, 11, 3, 16, 36, 30, 774, DateTimeKind.Local).AddTicks(7296), "Herta.Lind73@hotmail.com", "Herta", "Female", "712-549-8074", "Lind", "Magnus", null, "772-533-8938" },
                    { 138, "909-792-0010", "King, Littel and Pouros", new DateTime(1965, 5, 29, 19, 27, 10, 479, DateTimeKind.Local).AddTicks(522), "Brayan.Legros21@gmail.com", "Brayan", null, "248-583-9375", "Legros", "Celine", null, null },
                    { 139, "336-217-9915", "Batz - Emard", new DateTime(1980, 8, 19, 6, 48, 54, 380, DateTimeKind.Local).AddTicks(1273), "Amir.Berge82@hotmail.com", "Amir", null, "392-247-5995", "Berge", "Libbie", null, null },
                    { 140, "268-827-1770", "Rosenbaum, Fritsch and Parisian", new DateTime(1995, 2, 8, 14, 19, 15, 574, DateTimeKind.Local).AddTicks(6050), "Ole_Mosciski6@hotmail.com", "Ole", "Female", null, "Mosciski", "Marco", null, "598-611-2652" },
                    { 141, "212-350-3782", "Ritchie, Walter and Kuhlman", new DateTime(1993, 7, 29, 7, 51, 38, 635, DateTimeKind.Local).AddTicks(2118), "Demetris_Corwin@gmail.com", "Demetris", null, null, "Corwin", "Savannah", null, "526-689-1181" },
                    { 142, null, "Hackett LLC", new DateTime(1974, 1, 17, 22, 36, 21, 772, DateTimeKind.Local).AddTicks(3850), "Aliyah.Lowe81@hotmail.com", "Aliyah", "Male", "386-756-2996", "Lowe", "Alena", null, null },
                    { 143, "755-419-3867", "Howe, Hammes and O'Kon", new DateTime(1990, 3, 11, 18, 46, 16, 247, DateTimeKind.Local).AddTicks(252), "Adelia89@yahoo.com", "Adelia", "Male", null, "Kertzmann", "Margarete", null, "433-733-0126" },
                    { 144, null, "Hirthe, Sipes and Orn", new DateTime(1987, 10, 11, 0, 23, 25, 471, DateTimeKind.Local).AddTicks(5388), "Reese_Bashirian72@gmail.com", "Reese", "Female", null, "Bashirian", "Elian", null, null },
                    { 145, null, "Schmeler Inc", new DateTime(1963, 7, 2, 18, 35, 27, 480, DateTimeKind.Local).AddTicks(7262), "Nella_Senger@hotmail.com", "Nella", "Male", null, "Senger", "Jordi", null, null },
                    { 146, "213-402-2897", "Marvin LLC", new DateTime(1973, 9, 6, 23, 16, 2, 356, DateTimeKind.Local).AddTicks(3215), "Miguel12@yahoo.com", "Miguel", null, "303-726-4565", "Kub", "Tiara", null, "201-579-6354" },
                    { 147, null, "Schuppe Group", new DateTime(1990, 6, 27, 11, 57, 30, 222, DateTimeKind.Local).AddTicks(7204), "Boyd.Price3@gmail.com", "Boyd", "Female", "871-952-3330", "Price", "Nathen", null, "453-348-9672" },
                    { 148, null, "Zboncak Inc", new DateTime(1972, 1, 4, 8, 30, 34, 516, DateTimeKind.Local).AddTicks(4154), "Harold_Bartell71@hotmail.com", "Harold", "Male", "340-848-7435", "Bartell", "Ellen", null, "800-252-4077" },
                    { 149, null, "Rempel Inc", new DateTime(1959, 9, 28, 14, 16, 8, 37, DateTimeKind.Local).AddTicks(5298), "Kara9@gmail.com", "Kara", "Male", "210-856-2908", "Marquardt", "Maybell", null, "505-210-1191" },
                    { 150, null, "Howell, Hand and Paucek", new DateTime(1982, 3, 12, 4, 49, 40, 410, DateTimeKind.Local).AddTicks(30), "Jovan.Hackett@yahoo.com", "Jovan", null, "634-353-7239", "Hackett", "Michale", null, "576-304-7188" },
                    { 151, null, "Oberbrunner, Dicki and Ebert", new DateTime(1962, 6, 23, 4, 19, 40, 904, DateTimeKind.Local).AddTicks(3013), "Idella.Beatty@gmail.com", "Idella", "Female", null, "Beatty", "Aimee", null, null },
                    { 152, "249-629-5680", "Gleichner Group", new DateTime(1975, 12, 31, 0, 0, 21, 370, DateTimeKind.Local).AddTicks(1141), "Abbigail51@gmail.com", "Abbigail", "Female", "707-699-4986", "Huel", "Henry", null, "614-668-5162" },
                    { 153, null, "Monahan - Paucek", new DateTime(1983, 4, 3, 13, 30, 4, 583, DateTimeKind.Local).AddTicks(5501), "Kendall.Rath99@gmail.com", "Kendall", null, null, "Rath", "Lilyan", null, null },
                    { 154, null, "Dickinson, Ferry and Jacobs", new DateTime(1995, 4, 13, 5, 7, 54, 29, DateTimeKind.Local).AddTicks(8802), "Idella26@yahoo.com", "Idella", null, null, "Feil", "Roger", null, "794-709-5557" },
                    { 155, null, "Steuber - Schneider", new DateTime(1971, 4, 2, 13, 37, 19, 149, DateTimeKind.Local).AddTicks(4946), "Melvina_Friesen@gmail.com", "Melvina", null, null, "Friesen", "Harry", null, null },
                    { 156, null, "Auer - Friesen", new DateTime(1959, 9, 11, 20, 20, 10, 506, DateTimeKind.Local).AddTicks(9612), "Ron91@yahoo.com", "Ron", null, null, "Doyle", "Ezekiel", null, null },
                    { 157, null, "Jaskolski - Flatley", new DateTime(2002, 6, 23, 20, 39, 3, 149, DateTimeKind.Local).AddTicks(3426), "Colleen_Becker18@hotmail.com", "Colleen", "Male", "212-261-9875", "Becker", "Thaddeus", null, null },
                    { 158, "364-810-7514", "King, Marks and Wyman", new DateTime(1991, 5, 23, 1, 11, 48, 926, DateTimeKind.Local).AddTicks(4100), "Howard_Krajcik30@yahoo.com", "Howard", "Male", "261-920-1277", "Krajcik", "Willy", null, null },
                    { 159, "852-908-0525", "Lesch, Kessler and Hettinger", new DateTime(1980, 11, 1, 4, 45, 14, 899, DateTimeKind.Local).AddTicks(8580), "German.Conroy50@hotmail.com", "German", null, "511-331-7672", "Conroy", "Leon", null, null },
                    { 160, null, "Schimmel Inc", new DateTime(1982, 8, 22, 5, 27, 36, 988, DateTimeKind.Local).AddTicks(7535), "Chesley.Hyatt59@hotmail.com", "Chesley", null, "443-867-2091", "Hyatt", "Janae", null, null },
                    { 161, null, "Sipes, Dibbert and King", new DateTime(1976, 10, 24, 15, 13, 54, 563, DateTimeKind.Local).AddTicks(3714), "Jasen19@gmail.com", "Jasen", "Male", null, "Huels", "Craig", null, "462-472-7110" },
                    { 162, "912-543-1936", "Lemke - Schaefer", new DateTime(1995, 8, 17, 17, 31, 1, 621, DateTimeKind.Local).AddTicks(3900), "Jeffrey_Osinski@hotmail.com", "Jeffrey", "Male", null, "Osinski", "Darrick", null, null },
                    { 163, "451-536-3123", "Kulas Group", new DateTime(1959, 5, 28, 16, 46, 37, 883, DateTimeKind.Local).AddTicks(4368), "Jerod44@hotmail.com", "Jerod", null, null, "Hodkiewicz", "Shea", null, null },
                    { 164, null, "Rodriguez, Bosco and Kuhic", new DateTime(1987, 1, 14, 17, 6, 45, 569, DateTimeKind.Local).AddTicks(7625), "Joshua.Lang85@yahoo.com", "Joshua", "Female", null, "Lang", "Emile", null, "557-371-5927" },
                    { 165, null, "Stehr - Waters", new DateTime(1980, 9, 18, 22, 9, 55, 855, DateTimeKind.Local).AddTicks(1011), "Virgil9@hotmail.com", "Virgil", "Female", null, "Green", "Alanis", null, "973-809-4767" },
                    { 166, "544-274-7409", "Koss - Bashirian", new DateTime(1991, 7, 30, 20, 15, 40, 724, DateTimeKind.Local).AddTicks(1512), "Aimee26@yahoo.com", "Aimee", "Female", "422-835-1489", "Davis", "Pete", null, null },
                    { 167, "469-394-2797", "Dickinson - Wisoky", new DateTime(1973, 2, 11, 12, 6, 27, 780, DateTimeKind.Local).AddTicks(8038), "Anabel.Stiedemann@yahoo.com", "Anabel", "Female", "956-247-7129", "Stiedemann", "Hollis", null, "534-494-8475" },
                    { 168, "814-636-4733", "Hills, Glover and Hahn", new DateTime(1966, 8, 16, 13, 55, 50, 76, DateTimeKind.Local).AddTicks(4255), "Ryann_Bernhard@yahoo.com", "Ryann", "Female", "457-660-8636", "Bernhard", "Kiarra", null, "488-580-4890" },
                    { 169, "560-867-7500", "Keeling, Ward and Waelchi", new DateTime(2002, 7, 21, 13, 7, 27, 183, DateTimeKind.Local).AddTicks(3063), "Darby_Kemmer90@yahoo.com", "Darby", null, "869-580-5237", "Kemmer", "Pamela", null, null },
                    { 170, "650-649-3734", "Rau and Sons", new DateTime(1999, 3, 29, 16, 47, 38, 218, DateTimeKind.Local).AddTicks(2771), "Garfield_Pfeffer70@gmail.com", "Garfield", null, null, "Pfeffer", "Bert", null, "675-384-7473" },
                    { 171, "530-847-9335", "Fay, Roberts and Ledner", new DateTime(1975, 4, 2, 15, 34, 5, 988, DateTimeKind.Local).AddTicks(8665), "Shea.Abernathy@gmail.com", "Shea", null, null, "Abernathy", "Aletha", null, "898-636-4213" },
                    { 172, null, "Gleason, Nolan and Morar", new DateTime(2003, 7, 10, 6, 40, 46, 783, DateTimeKind.Local).AddTicks(2092), "Gust_Feest82@gmail.com", "Gust", null, null, "Feest", "Yvette", null, null },
                    { 173, null, "Medhurst - Armstrong", new DateTime(2003, 3, 25, 9, 38, 11, 400, DateTimeKind.Local).AddTicks(6025), "Beaulah_Schaefer23@gmail.com", "Beaulah", null, "245-355-9933", "Schaefer", "Orville", null, "807-619-5391" },
                    { 174, null, "Hermiston Group", new DateTime(1985, 6, 18, 19, 51, 54, 128, DateTimeKind.Local).AddTicks(4462), "Maxwell_Abbott@yahoo.com", "Maxwell", null, "601-294-4114", "Abbott", "Jimmy", null, null },
                    { 175, "532-355-7533", "Wuckert - Waelchi", new DateTime(2003, 12, 10, 0, 29, 6, 504, DateTimeKind.Local).AddTicks(7525), "Selmer38@hotmail.com", "Selmer", "Female", null, "Koss", "Gene", null, null },
                    { 176, "947-570-6853", "Stokes and Sons", new DateTime(1991, 2, 21, 13, 6, 21, 155, DateTimeKind.Local).AddTicks(4640), "Jaden46@yahoo.com", "Jaden", null, "557-577-9884", "Schroeder", "Catherine", null, null },
                    { 177, null, "Connelly - Howe", new DateTime(1956, 6, 8, 11, 14, 16, 137, DateTimeKind.Local).AddTicks(8700), "Nora.Prohaska@gmail.com", "Nora", "Male", null, "Prohaska", "Maeve", null, "789-576-1248" },
                    { 178, "275-661-8413", "Quigley - Haley", new DateTime(1977, 7, 6, 8, 53, 16, 39, DateTimeKind.Local).AddTicks(3595), "Lennie_Frami@gmail.com", "Lennie", "Female", null, "Frami", "Cleve", null, null },
                    { 179, "556-204-9513", "Goldner - Schulist", new DateTime(1991, 7, 25, 2, 29, 7, 347, DateTimeKind.Local).AddTicks(9201), "Sheridan_Wuckert5@gmail.com", "Sheridan", "Female", null, "Wuckert", "Fritz", null, "877-476-7007" },
                    { 180, null, "Koepp - Weber", new DateTime(1966, 4, 3, 9, 5, 41, 997, DateTimeKind.Local).AddTicks(8360), "Sherman.OConner32@gmail.com", "Sherman", "Male", null, "O'Conner", "Kianna", null, "479-982-0429" },
                    { 181, "430-531-2023", "Wiza, Becker and Hammes", new DateTime(1998, 2, 8, 9, 16, 48, 394, DateTimeKind.Local).AddTicks(7673), "Nicholaus56@hotmail.com", "Nicholaus", null, "253-895-4671", "Bernier", "Mertie", null, "797-670-9578" },
                    { 182, null, "Zboncak LLC", new DateTime(1961, 5, 13, 9, 47, 19, 724, DateTimeKind.Local).AddTicks(7526), "Crystel.McDermott26@hotmail.com", "Crystel", null, "622-743-1496", "McDermott", "Raphael", null, null },
                    { 183, null, "Kreiger - Grady", new DateTime(1969, 6, 21, 0, 25, 56, 740, DateTimeKind.Local).AddTicks(4337), "Salma.Parisian@yahoo.com", "Salma", null, null, "Parisian", "Kian", null, "939-611-9074" },
                    { 184, null, "Von, Cummings and Lebsack", new DateTime(1984, 8, 29, 4, 42, 44, 650, DateTimeKind.Local).AddTicks(6798), "Reina_Simonis78@gmail.com", "Reina", null, null, "Simonis", "Jedediah", null, null },
                    { 185, "553-287-0061", "Hessel LLC", new DateTime(1969, 8, 8, 19, 9, 18, 813, DateTimeKind.Local).AddTicks(3990), "Andy_Frami46@yahoo.com", "Andy", "Female", "745-864-2046", "Frami", "Arthur", null, null },
                    { 186, "350-659-2484", "Yundt Inc", new DateTime(1972, 8, 30, 19, 41, 44, 984, DateTimeKind.Local).AddTicks(988), "Dameon.Bergnaum43@hotmail.com", "Dameon", "Female", "272-264-5675", "Bergnaum", "Aleen", null, null },
                    { 187, "813-517-2417", "Friesen, Fritsch and Abbott", new DateTime(2002, 7, 2, 8, 1, 40, 268, DateTimeKind.Local).AddTicks(8102), "Pearline.Leuschke77@yahoo.com", "Pearline", null, "895-823-9922", "Leuschke", "Crystal", null, "415-690-9567" },
                    { 188, null, "Wuckert Group", new DateTime(1989, 1, 5, 14, 15, 30, 921, DateTimeKind.Local).AddTicks(3715), "Felix_Upton49@gmail.com", "Felix", null, null, "Upton", "Enos", null, "570-716-5921" },
                    { 189, "431-543-6587", "Rippin, Legros and Ratke", new DateTime(1991, 6, 4, 12, 30, 58, 69, DateTimeKind.Local).AddTicks(9813), "Lavina6@yahoo.com", "Lavina", "Male", null, "Rohan", "Dariana", null, null },
                    { 190, null, "O'Connell, Walsh and Stroman", new DateTime(1972, 10, 20, 12, 3, 15, 100, DateTimeKind.Local).AddTicks(3685), "Wade28@gmail.com", "Wade", "Female", null, "Flatley", "Jarret", null, null },
                    { 191, "682-802-2481", "Collins Group", new DateTime(1963, 11, 4, 1, 39, 20, 490, DateTimeKind.Local).AddTicks(453), "Helga_Mann@hotmail.com", "Helga", null, "912-971-9682", "Mann", "Price", null, "589-480-5668" },
                    { 192, "447-246-4241", "Ankunding - Veum", new DateTime(1970, 1, 30, 7, 59, 45, 61, DateTimeKind.Local).AddTicks(5719), "Kimberly.Medhurst0@hotmail.com", "Kimberly", "Male", "572-747-4828", "Medhurst", "Antwon", null, "901-513-4813" },
                    { 193, "691-982-3723", "Larson Group", new DateTime(1975, 8, 3, 16, 43, 9, 254, DateTimeKind.Local).AddTicks(435), "Olga89@yahoo.com", "Olga", null, null, "Runolfsson", "Adella", null, "816-813-9044" },
                    { 194, "955-953-9350", "Johnson, Grant and Prohaska", new DateTime(1956, 10, 6, 12, 22, 0, 819, DateTimeKind.Local).AddTicks(7887), "Dayna.Hoppe@yahoo.com", "Dayna", "Female", null, "Hoppe", "Albina", null, "628-511-5240" },
                    { 195, "494-602-2305", "Ward, Bins and Prosacco", new DateTime(1978, 1, 2, 17, 59, 0, 521, DateTimeKind.Local).AddTicks(8774), "Billy_Wunsch80@yahoo.com", "Billy", null, null, "Wunsch", "Geoffrey", null, null },
                    { 196, "847-833-5420", "Bailey - Ankunding", new DateTime(1969, 5, 15, 23, 1, 52, 440, DateTimeKind.Local).AddTicks(3439), "Stephan.Kulas@hotmail.com", "Stephan", null, null, "Kulas", "Tia", null, null },
                    { 197, null, "Fay Group", new DateTime(1975, 7, 14, 2, 51, 11, 413, DateTimeKind.Local).AddTicks(5290), "Celestino5@yahoo.com", "Celestino", null, null, "Wiegand", "Estell", null, null },
                    { 198, "657-652-5595", "Hegmann, Greenfelder and Greenholt", new DateTime(1971, 4, 4, 17, 35, 57, 694, DateTimeKind.Local).AddTicks(792), "Elfrieda24@yahoo.com", "Elfrieda", "Female", "497-620-5677", "Haley", "Bradley", null, null },
                    { 199, "970-891-3317", "Raynor - Schowalter", new DateTime(1961, 7, 25, 14, 3, 6, 832, DateTimeKind.Local).AddTicks(3017), "Gina36@yahoo.com", "Gina", null, null, "Pouros", "Leo", null, null },
                    { 200, null, "Predovic Group", new DateTime(1981, 2, 14, 23, 19, 56, 871, DateTimeKind.Local).AddTicks(2229), "Esther_Larson35@yahoo.com", "Esther", null, null, "Larson", "Juana", null, "914-302-7306" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "BookEntryId", "CityTown", "Country", "Line1", "Line2", "Line3", "Name", "PostalCode", "StateProvince" },
                values: new object[,]
                {
                    { 50, 103, "Dachbury", "Belize", "790 Luigi Port", null, "Suite 386", "Extended synergies", "93144", "Michigan" },
                    { 51, 109, "South Daniela", "Qatar", "906 Corene Lodge", "Suite 210", "Suite 163", "Auto Loan Account modular", "51243-5980", "New York" },
                    { 52, 110, "Alyceberg", "Reunion", "61298 Purdy Mountains", "Suite 387", "Apt. 740", "Credit Card Account yellow", "07402-9994", "Missouri" },
                    { 53, 111, "Kiehnside", "Virgin Islands, U.S.", "9972 Dayna Knoll", "Apt. 592", "Apt. 469", "Customer synthesize", "69633", "Oregon" },
                    { 54, 113, "Fritschtown", "Falkland Islands (Malvinas)", "5734 Quinton Stravenue", null, "Suite 019", "Personal Loan Account Developer", "64354", "Pennsylvania" },
                    { 55, 114, "South Augustusside", "Haiti", "8104 Pinkie Divide", null, "Suite 286", "alarm Designer", "31291", "Hawaii" },
                    { 56, 115, "South Reyna", "Albania", "515 Meaghan Drive", null, "Apt. 098", "PCI Generic Granite Chair", "79122-7917", "New Jersey" },
                    { 57, 116, "Noetown", "Isle of Man", "91117 Satterfield Rapids", null, "Apt. 027", "sky blue District", "73120-0914", "Indiana" },
                    { 58, 117, "Port Theodora", "Jamaica", "598 Schuppe Knolls", "Apt. 059", "Suite 986", "reboot Belize Dollar", "80782", "New Hampshire" },
                    { 59, 120, "Emmerichland", "Tunisia", "67998 Willy Views", null, "Apt. 675", "Incredible pink", "76663-3736", "New York" },
                    { 60, 122, "East Uriah", "Botswana", "9297 Wunsch Road", "Suite 073", "Suite 589", "synthesize USB", "54324", "Connecticut" },
                    { 61, 123, "Maymiebury", "Isle of Man", "64798 Feil Knoll", "Suite 509", "Apt. 080", "web-enabled back up", "26956", "North Dakota" },
                    { 62, 125, "South Carmelmouth", "Algeria", "839 Leffler Walk", null, "Apt. 305", "Burgs upward-trending", "10523-4057", "Florida" },
                    { 63, 129, "Raphaelborough", "New Zealand", "048 Meagan Creek", "Apt. 415", "Apt. 877", "Isle Groves", "16864", "Virginia" },
                    { 64, 131, "Muellershire", "Central African Republic", "6133 Evie Locks", "Suite 018", "Suite 692", "synergies Orchestrator", "00340", "Delaware" },
                    { 65, 132, "East Sophiaborough", "United States of America", "4902 Zachariah Lane", "Apt. 827", null, "indexing Unbranded Metal Bacon", "62205", "Maryland" },
                    { 66, 133, "Port Mariannachester", "Iran", "2920 Zetta Centers", null, "Suite 946", "SAS Intelligent Granite Sausages", "06094-6412", "Georgia" },
                    { 67, 136, "Mayertside", "Ukraine", "97339 Dena Avenue", null, "Apt. 169", "program Customer-focused", "08348-2024", "Illinois" },
                    { 68, 137, "Boyleburgh", "Nepal", "91287 Hessel Creek", "Apt. 218", "Apt. 049", "1080p mindshare", "78097", "Virginia" },
                    { 69, 139, "Lake Rhiannonhaven", "Micronesia", "58961 Pearlie Forge", null, "Suite 377", "next generation Quality", "02109", "Massachusetts" },
                    { 70, 140, "East Jeffrey", "Benin", "79678 Bernadine Plain", "Suite 824", "Apt. 544", "Intelligent Soft Salad generating", "62999-4407", "New Hampshire" },
                    { 71, 141, "Port Maritza", "Niger", "8469 Kaylah Glens", null, "Suite 662", "payment Rustic Wooden Chips", "28432", "Rhode Island" },
                    { 72, 143, "New Amariton", "Cayman Islands", "773 Aimee Overpass", "Suite 309", "Suite 509", "French Guiana forecast", "34304-3101", "New Jersey" },
                    { 73, 146, "Ashleebury", "Benin", "43339 Mills Route", null, "Suite 351", "Auto Loan Account distributed", "61973-9763", "North Dakota" },
                    { 74, 148, "Bayleeburgh", "British Indian Ocean Territory (Chagos Archipelago)", "2164 Crooks Lodge", "Suite 581", "Suite 868", "Chief Isle", "99053-6278", "Louisiana" },
                    { 75, 150, "East Jakob", "Palestinian Territory", "248 Rohan Circle", null, "Suite 728", "Shoes Chief", "28670-9571", "Florida" },
                    { 76, 151, "Trudiebury", "Burundi", "86811 Jared Expressway", "Suite 081", "Apt. 742", "Dynamic Security", "86138", "Minnesota" },
                    { 77, 152, "Yostbury", "Palestinian Territory", "64991 Alexa Isle", null, "Apt. 835", "gold Fantastic Fresh Computer", "53062", "Hawaii" },
                    { 78, 153, "Faheymouth", "Niger", "955 Flavie Freeway", "Suite 245", "Suite 797", "hack red", "58342", "Vermont" },
                    { 79, 154, "Gleasonburgh", "Costa Rica", "39347 Reece Bridge", null, "Suite 389", "Plaza Plains", "59529", "Tennessee" },
                    { 80, 156, "Vidafurt", "Greenland", "08311 Otto Meadows", null, "Apt. 563", "Personal Loan Account SCSI", "34045", "Maine" },
                    { 81, 158, "New Olaf", "Libyan Arab Jamahiriya", "09168 Jena Fall", "Suite 049", "Suite 952", "Rustic Views", "58868", "Kansas" },
                    { 82, 160, "Port Hassie", "Guam", "7895 Cary Mountains", "Suite 391", "Suite 358", "Money Market Account Fresh", "62148", "South Carolina" },
                    { 83, 161, "Reingerchester", "Jamaica", "46625 Heller Roads", "Suite 579", "Apt. 978", "Soft scalable", "36401-5460", "South Carolina" },
                    { 84, 163, "Lake Hayleefort", "Marshall Islands", "504 Carey Flat", null, "Suite 717", "hard drive Devolved", "19397-4086", "Hawaii" },
                    { 85, 170, "East Declanland", "Somalia", "76715 Rhoda Locks", "Suite 872", "Apt. 706", "Accounts payment", "55789-9519", "Oregon" },
                    { 86, 172, "Feeneyton", "Palestinian Territory", "9773 Sanford Well", null, "Suite 293", "engage synergize", "50252", "Wisconsin" },
                    { 87, 174, "Jaskolskiport", "Sri Lanka", "7140 Percy Tunnel", "Apt. 856", "Suite 046", "Rustic Metal Bike Russian Ruble", "62572-6183", "Hawaii" },
                    { 88, 175, "East Mabelberg", "Algeria", "4179 Koss Forks", null, "Apt. 829", "generate orange", "49918", "Minnesota" },
                    { 89, 177, "East Darianville", "Libyan Arab Jamahiriya", "883 Emmerich Run", null, "Suite 457", "user-centric lime", "85261", "Mississippi" },
                    { 90, 178, "Barrowsport", "Denmark", "323 Nikita Motorway", "Apt. 504", "Apt. 622", "Avon Berkshire", "73152", "Montana" },
                    { 91, 181, "South Rhea", "Belize", "7689 Fay Bypass", "Suite 913", "Apt. 982", "Berkshire Practical", "53370", "Texas" },
                    { 92, 182, "Lake Addieshire", "South Africa", "311 Adolfo Drives", null, "Apt. 533", "override Solutions", "66398", "New Jersey" },
                    { 93, 183, "North Brownhaven", "Central African Republic", "322 Adalberto Crescent", null, "Suite 751", "Handmade Concrete Gloves navigate", "92274-8376", "Maine" },
                    { 94, 186, "New Alexane", "Denmark", "634 Halvorson Harbor", "Suite 115", "Apt. 362", "Mountain Small", "48065", "Vermont" },
                    { 95, 187, "South Wilfordfort", "Malta", "5714 Estevan Stream", null, "Apt. 399", "Gorgeous turquoise", "42072", "North Dakota" },
                    { 96, 191, "New Lorenzton", "Romania", "74529 Garrett Landing", null, "Apt. 171", "Chief Cotton", "63202", "Maine" },
                    { 97, 193, "West Dylantown", "Gambia", "4574 Eleanora Burg", null, "Apt. 171", "Somoni withdrawal", "03270", "Colorado" },
                    { 98, 194, "New Torrance", "Switzerland", "698 Collier Trace", null, "Suite 546", "Handcrafted Fresh Tuna Oregon", "02031-3073", "Hawaii" },
                    { 99, 196, "New Shaunfort", "Saint Barthelemy", "1216 Bosco Ferry", null, "Suite 398", "web services salmon", "58087-6039", "Indiana" },
                    { 100, 199, "West Marcella", "Netherlands Antilles", "237 Nienow Landing", "Apt. 808", "Apt. 199", "Buckinghamshire Buckinghamshire", "28913", "Iowa" },
                    { 101, 200, "East Amiya", "Hungary", "3047 Mabelle Forge", null, "Suite 207", "executive Investment Account", "77410-1754", "South Carolina" }
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
