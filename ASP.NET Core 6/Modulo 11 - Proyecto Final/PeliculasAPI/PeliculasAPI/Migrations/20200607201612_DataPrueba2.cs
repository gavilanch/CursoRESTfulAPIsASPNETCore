using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliculasAPI.Migrations
{
    public partial class DataPrueba2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Nombre",
                value: "Suspenso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Nombre",
                value: "Drama");
        }
    }
}
