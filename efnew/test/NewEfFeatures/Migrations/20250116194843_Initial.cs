using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NewEfFeatures.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureAirport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationAirport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Boarded = table.Column<bool>(type: "bit", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passengers_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "Departure", "DepartureAirport", "DestinationAirport", "FlightTime", "Number" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 1, 15, 10, 0, 0, DateTimeKind.Unspecified), "ATL", "AMS", new TimeSpan(0, 7, 30, 0, 0), "SW0074" },
                    { 2, new DateTime(2025, 2, 7, 15, 10, 0, 0, DateTimeKind.Unspecified), "AMS", "ATL", new TimeSpan(0, 8, 30, 0, 0), "SW0075" }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "Boarded", "FirstName", "FlightId", "LastName", "MiddleName", "SeatNumber" },
                values: new object[,]
                {
                    { 1, true, "Shawn", 1, "Wildermuth", null, "11B" },
                    { 2, true, "Jim", 1, "Smith", null, "31D" },
                    { 3, true, "Rachel", 1, "Moore", null, "19A" },
                    { 4, false, "Shawn", 2, "Wildermuth", null, "15F" },
                    { 5, false, "Kelley", 2, "Montegue", null, "2A" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_FlightId",
                table: "Passengers",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
