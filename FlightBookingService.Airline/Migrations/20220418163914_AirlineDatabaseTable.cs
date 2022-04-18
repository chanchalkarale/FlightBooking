using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightBookingService.Airline.Migrations
{
    public partial class AirlineDatabaseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirlineFlightDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromPlaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToPlaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightToDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalBusinessSeats = table.Column<int>(type: "int", nullable: false),
                    TotalNonBusinessSeats = table.Column<int>(type: "int", nullable: false),
                    TicketCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FlightSeatRow = table.Column<int>(type: "int", nullable: false),
                    Meal = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Flag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineFlightDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirlineFlightDetails");
        }
    }
}
