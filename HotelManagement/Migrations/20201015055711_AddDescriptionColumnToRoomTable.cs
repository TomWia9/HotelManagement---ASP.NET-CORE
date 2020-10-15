using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelManagement.Migrations
{
    public partial class AddDescriptionColumnToRoomTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rooms",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Vacancy",
                table: "Rooms",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Vacancy",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
