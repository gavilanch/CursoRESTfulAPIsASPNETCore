using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiModulo5.Migrations
{
    public partial class DatosLibros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Id", "AutorId", "Titulo" },
                values: new object[] { 1, 1, "Entity Framework Core 2.1 - De verdad" });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Id", "AutorId", "Titulo" },
                values: new object[] { 2, 2, "Construyendo Web APIs con ASP.NET Core 2.2" });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Id", "AutorId", "Titulo" },
                values: new object[] { 3, 2, "Libro de prueba" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
