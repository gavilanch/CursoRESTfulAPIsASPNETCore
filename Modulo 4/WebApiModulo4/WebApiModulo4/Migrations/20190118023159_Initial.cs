using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiModulo4.Migrations
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
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 1, "Felipe Gavilán" });

            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 2, "Claudia Rodríguez" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autores");
        }
    }
}
