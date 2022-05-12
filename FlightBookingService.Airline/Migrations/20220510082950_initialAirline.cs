using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightBookingService.Airline.Migrations
{
    public partial class initialAirline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirlineDetails",
                columns: table => new
                {
                    AirlineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineNmae = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineDetails", x => x.AirlineId);
                });

            migrationBuilder.CreateTable(
                name: "airlineFlightDetailsRawQueryModels",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromPlaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToPlaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightToDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalBusinessSeats = table.Column<int>(type: "int", nullable: false),
                    TotalNonBusinessSeats = table.Column<int>(type: "int", nullable: false),
                    TicketCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClassStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountCost = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
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
                    ClassStatus = table.Column<int>(type: "int", nullable: false),
                    TotalCosts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBookSeats = table.Column<int>(type: "int", nullable: false),
                    PNR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "AirlineFlightDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirlineId = table.Column<int>(type: "int", nullable: false),
                    FromPlaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToPlaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightToDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalBusinessSeats = table.Column<int>(type: "int", nullable: false),
                    TotalNonBusinessSeats = table.Column<int>(type: "int", nullable: false),
                    BusTicketCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NonBusTicketCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FlightSeatRow = table.Column<int>(type: "int", nullable: false),
                    Meal = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineFlightDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirlineFlightDetails_AirlineDetails_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "AirlineDetails",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirlineFlightDetails_AirlineId",
                table: "AirlineFlightDetails",
                column: "AirlineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirlineFlightDetails");

            migrationBuilder.DropTable(
                name: "airlineFlightDetailsRawQueryModels");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "FlightBookingDetails");

            migrationBuilder.DropTable(
                name: "UserBookingDetails");

            migrationBuilder.DropTable(
                name: "AirlineDetails");
        }
    }
}
