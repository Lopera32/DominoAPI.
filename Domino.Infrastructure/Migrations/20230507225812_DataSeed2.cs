using Microsoft.EntityFrameworkCore.Migrations;

namespace Domino.Infrastructure.Migrations
{
    public partial class DataSeed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Lastname", "Name", "Password" },
                values: new object[] { 1, "jsloperagdv@gmail.com", "Lopera", "Sebastian ", "holamundo" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Lastname", "Name", "Password" },
                values: new object[] { 2, "soledadgallegodm@gmail.com", "Gallego", "Soledad ", "holamundo2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
