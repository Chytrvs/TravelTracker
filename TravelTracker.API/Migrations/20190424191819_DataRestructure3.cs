using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelTracker.API.Migrations
{
    public partial class DataRestructure3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Users_UserId",
                table: "Flights");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Flights",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Users_UserId",
                table: "Flights",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Users_UserId",
                table: "Flights");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Flights",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Users_UserId",
                table: "Flights",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
