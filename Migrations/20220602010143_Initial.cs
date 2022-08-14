using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCSandBox.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovResumoLancamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovResumoLancamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovDetalheLancamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    ResumoLancDiaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovDetalheLancamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovDetalheLancamentos_MovResumoLancamentos_ResumoLancDiaId",
                        column: x => x.ResumoLancDiaId,
                        principalTable: "MovResumoLancamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovDetalheLancamentos_ResumoLancDiaId",
                table: "MovDetalheLancamentos",
                column: "ResumoLancDiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovDetalheLancamentos");

            migrationBuilder.DropTable(
                name: "MovResumoLancamentos");
        }
    }
}
