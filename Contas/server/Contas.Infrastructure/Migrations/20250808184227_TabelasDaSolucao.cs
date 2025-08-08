using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TabelasDaSolucao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    CPF = table.Column<long>(type: "BIGINT", nullable: false),
                    DataDeCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDeAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SegmentoDoCredor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    DataDeCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDeAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentoDoCredor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SegmentoDoCredorId = table.Column<int>(type: "int", nullable: false),
                    RazaoSocial = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    NomeFantasia = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    CNPJ = table.Column<long>(type: "BIGINT", nullable: false),
                    DataDeCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDeAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credor_SegmentoDoCredor_SegmentoDoCredorId",
                        column: x => x.SegmentoDoCredorId,
                        principalTable: "SegmentoDoCredor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosDaConta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CredorId = table.Column<int>(type: "int", nullable: false),
                    PagadorId = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    DataDeVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoDeBarras = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    DataDePagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDosJuros = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDoDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoletoBancario = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false),
                    ComprovanteDePagamento = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: true),
                    DataDeCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDeAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosDaConta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosDaConta_Credor_CredorId",
                        column: x => x.CredorId,
                        principalTable: "Credor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrosDaConta_Pagador_PagadorId",
                        column: x => x.PagadorId,
                        principalTable: "Pagador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credor_SegmentoDoCredorId",
                table: "Credor",
                column: "SegmentoDoCredorId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosDaConta_CredorId",
                table: "RegistrosDaConta",
                column: "CredorId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosDaConta_PagadorId",
                table: "RegistrosDaConta",
                column: "PagadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosDaConta");

            migrationBuilder.DropTable(
                name: "Credor");

            migrationBuilder.DropTable(
                name: "Pagador");

            migrationBuilder.DropTable(
                name: "SegmentoDoCredor");
        }
    }
}
