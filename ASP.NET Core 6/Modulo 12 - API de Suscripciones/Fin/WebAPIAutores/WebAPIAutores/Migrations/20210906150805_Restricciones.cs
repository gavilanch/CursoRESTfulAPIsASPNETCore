using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPIAutores.Migrations
{
    public partial class Restricciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestriccionesDominio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LlaveId = table.Column<int>(type: "int", nullable: false),
                    Dominio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestriccionesDominio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestriccionesDominio_LlavesAPI_LlaveId",
                        column: x => x.LlaveId,
                        principalTable: "LlavesAPI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestriccionesIP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LlaveId = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestriccionesIP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestriccionesIP_LlavesAPI_LlaveId",
                        column: x => x.LlaveId,
                        principalTable: "LlavesAPI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestriccionesDominio_LlaveId",
                table: "RestriccionesDominio",
                column: "LlaveId");

            migrationBuilder.CreateIndex(
                name: "IX_RestriccionesIP_LlaveId",
                table: "RestriccionesIP",
                column: "LlaveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestriccionesDominio");

            migrationBuilder.DropTable(
                name: "RestriccionesIP");
        }
    }
}
