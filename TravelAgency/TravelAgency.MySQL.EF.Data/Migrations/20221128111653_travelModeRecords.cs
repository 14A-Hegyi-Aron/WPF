using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.MySQL.EF.Data.Migrations
{
    public partial class travelModeRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "travelModes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "busz" });

            migrationBuilder.InsertData(
                table: "travelModes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "repülő" });

            migrationBuilder.InsertData(
                table: "travelModes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "hajó" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "travelModes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "travelModes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "travelModes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
