using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DefinicaoDasTabelasDeTrilhaDeAuditoriaELogDeErros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogDeErro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mensagem = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    Detalhes = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    Metodo = table.Column<string>(type: "VARCHAR(max)", nullable: false),
                    Caminho = table.Column<string>(type: "VARCHAR(max)", nullable: false),
                    Ip = table.Column<string>(type: "VARCHAR(45)", nullable: false),
                    Navegador = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    Usuario = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    DataDeCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataDeAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogDeErro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrilhaDeAuditoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entidade = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Metodo = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Caminho = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    Operacao = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    ValoresAntigos = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    ValoresNovos = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    Ip = table.Column<string>(type: "VARCHAR(45)", nullable: false),
                    Navegador = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    DataDeCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrilhaDeAuditoria", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogDeErro");

            migrationBuilder.DropTable(
                name: "TrilhaDeAuditoria");
        }
    }
}
