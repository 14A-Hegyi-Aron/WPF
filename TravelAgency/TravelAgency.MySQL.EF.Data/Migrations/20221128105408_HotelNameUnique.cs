using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.MySQL.EF.Data.Migrations
{
    public partial class HotelNameUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_hotels_Name",
                table: "hotels",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_hotels_Name",
                table: "hotels");
        }
    }
}
