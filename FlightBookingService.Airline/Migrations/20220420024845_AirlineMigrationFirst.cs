using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightBookingService.Airline.Migrations
{
    public partial class AirlineMigrationFirst : Migration
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

            migrationBuilder.CreateTable(
                name: "FlightBookingDetails",
                columns: table => new
                {
                    FlightBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Journey = table.Column<int>(type: "int", nullable: false),
                    OneWayCost = table.Column<double>(type: "float", nullable: false),
                    TwoWayCost = table.Column<double>(type: "float", nullable: false),
                    TotalBookSeats = table.Column<int>(type: "int", nullable: false),
                    PNR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Flag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightBookingDetails", x => x.FlightBookingId);
                });

            migrationBuilder.CreateTable(
                name: "UserBookingDetails",
                columns: table => new
                {
                    UserBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightBookingId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Meal = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Flag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookingDetails", x => x.UserBookingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirlineFlightDetails");

            migrationBuilder.DropTable(
                name: "FlightBookingDetails");

            migrationBuilder.DropTable(
                name: "UserBookingDetails");
        }
    }
}
