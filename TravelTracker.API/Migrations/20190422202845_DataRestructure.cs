using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelTracker.API.Migrations
{
    public partial class DataRestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airports_FlightEndingPointId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airports_FlightStartingPointId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Trips_TripId",
                table: "Flight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flight",
                table: "Flight");

            migrationBuilder.RenameTable(
                name: "Flight",
                newName: "Flights");

            migrationBuilder.RenameColumn(
                name: "FlightStartingPointId",
                table: "Flights",
                newName: "FlightDestinationAirportId");

            migrationBuilder.RenameColumn(
                name: "FlightEndingPointId",
                table: "Flights",
                newName: "FlightDepartureAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_TripId",
                table: "Flights",
                newName: "IX_Flights_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_FlightStartingPointId",
                table: "Flights",
                newName: "IX_Flights_FlightDestinationAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_FlightEndingPointId",
                table: "Flights",
                newName: "IX_Flights_FlightDepartureAirportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flights",
                table: "Flights",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_FlightDepartureAirportId",
                table: "Flights",
                column: "FlightDepartureAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_FlightDestinationAirportId",
                table: "Flights",
                column: "FlightDestinationAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Trips_TripId",
                table: "Flights",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_FlightDepartureAirportId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_FlightDestinationAirportId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Trips_TripId",
                table: "Flights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flights",
                table: "Flights");

            migrationBuilder.RenameTable(
                name: "Flights",
                newName: "Flight");

            migrationBuilder.RenameColumn(
                name: "FlightDestinationAirportId",
                table: "Flight",
                newName: "FlightStartingPointId");

            migrationBuilder.RenameColumn(
                name: "FlightDepartureAirportId",
                table: "Flight",
                newName: "FlightEndingPointId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_TripId",
                table: "Flight",
                newName: "IX_Flight_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_FlightDestinationAirportId",
                table: "Flight",
                newName: "IX_Flight_FlightStartingPointId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_FlightDepartureAirportId",
                table: "Flight",
                newName: "IX_Flight_FlightEndingPointId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flight",
                table: "Flight",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airports_FlightEndingPointId",
                table: "Flight",
                column: "FlightEndingPointId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airports_FlightStartingPointId",
                table: "Flight",
                column: "FlightStartingPointId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Trips_TripId",
                table: "Flight",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
