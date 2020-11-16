using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelManagement.Migrations
{
    public partial class ChangeBalconyColumnToHasBalconyInRoomTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balcony",
                table: "Rooms");

            migrationBuilder.AddColumn<bool>(
                name: "HasBalcony",
                table: "Rooms",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBalcony",
                table: "Rooms");

            migrationBuilder.AddColumn<bool>(
                name: "Balcony",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
