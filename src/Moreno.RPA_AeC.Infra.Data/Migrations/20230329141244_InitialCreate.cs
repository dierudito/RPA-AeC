using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moreno.RPA_AeC.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pesquisas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Termo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pesquisas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pesquisas_Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PesquisaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pesquisas_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pesquisas_Logs_Pesquisas_PesquisaId",
                        column: x => x.PesquisaId,
                        principalTable: "Pesquisas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pesquisas_Resultados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PesquisaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Area = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Autor = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    DataPublicacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CapturadoTotalmente = table.Column<bool>(type: "bit", nullable: false),
                    AoMenosUmCapturado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pesquisas_Resultados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pesquisas_Resultados_Pesquisas_PesquisaId",
                        column: x => x.PesquisaId,
                        principalTable: "Pesquisas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pesquisas_Logs_PesquisaId",
                table: "Pesquisas_Logs",
                column: "PesquisaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pesquisas_Resultados_PesquisaId",
                table: "Pesquisas_Resultados",
                column: "PesquisaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pesquisas_Logs");

            migrationBuilder.DropTable(
                name: "Pesquisas_Resultados");

            migrationBuilder.DropTable(
                name: "Pesquisas");
        }
    }
}
