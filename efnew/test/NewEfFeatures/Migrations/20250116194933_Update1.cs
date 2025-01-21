using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewEfFeatures.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Flights",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Gate",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1,
                column: "Gate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2,
                column: "Gate",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IDX_FLIGHT_NUMBER",
                table: "Flights",
                column: "Number")
                .Annotation("SqlServer:FillFactor", 80);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_FLIGHT_NUMBER",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Gate",
                table: "Flights");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
