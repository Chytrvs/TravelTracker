using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelTracker.API.Migrations
{
    public partial class AirportRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Points_FlightEndingPointId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Points_FlightStartingPointId",
                table: "Flight");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Acronym = table.Column<string>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airports_FlightEndingPointId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airports_FlightStartingPointId",
                table: "Flight");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Points_FlightEndingPointId",
                table: "Flight",
                column: "FlightEndingPointId",
                principalTable: "Points",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Points_FlightStartingPointId",
                table: "Flight",
                column: "FlightStartingPointId",
                principalTable: "Points",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
