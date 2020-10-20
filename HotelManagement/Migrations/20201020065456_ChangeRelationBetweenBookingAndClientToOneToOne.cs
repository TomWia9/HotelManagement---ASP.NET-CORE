using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelManagement.Migrations
{
    public partial class ChangeRelationBetweenBookingAndClientToOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Bookings_BookingId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_BookingId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClientId",
                table: "Bookings",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Clients_ClientId",
                table: "Bookings",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Clients_ClientId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ClientId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BookingId",
                table: "Clients",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Bookings_BookingId",
                table: "Clients",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
