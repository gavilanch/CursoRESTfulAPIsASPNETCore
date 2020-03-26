using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiModulo10.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    Identificacion = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Libro",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(nullable: true),
                    AutorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Libro_Autores_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "Id", "FechaNacimiento", "Identificacion", "Nombre" },
                values: new object[] { 1, new DateTime(1900, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Felipe Gavilán" });

            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "Id", "FechaNacimiento", "Identificacion", "Nombre" },
                values: new object[] { 2, new DateTime(1905, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Claudia Rodríguez" });

            migrationBuilder.InsertData(
                table: "Libro",
                columns: new[] { "Id", "AutorId", "Titulo" },
                values: new object[] { 1, 1, "Entity Framework Core 2.1 - De verdad" });

            migrationBuilder.InsertData(
                table: "Libro",
                columns: new[] { "Id", "AutorId", "Titulo" },
                values: new object[] { 2, 2, "Construyendo Web APIs con ASP.NET Core 2.2" });

            migrationBuilder.InsertData(
                table: "Libro",
                columns: new[] { "Id", "AutorId", "Titulo" },
                values: new object[] { 3, 2, "Libro de prueba" });

            migrationBuilder.CreateIndex(
                name: "IX_Libro_AutorId",
                table: "Libro",
                column: "AutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Libro");

            migrationBuilder.DropTable(
                name: "Autores");
        }
    }
}
