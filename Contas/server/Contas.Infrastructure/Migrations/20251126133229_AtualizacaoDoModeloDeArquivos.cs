using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoDoModeloDeArquivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arquivo_RegistroDaConta_RegistroDaContaId",
                table: "Arquivo");

            migrationBuilder.DropForeignKey(
                name: "FK_Credor_SegmentoDoCredor_SegmentoDoCredorId",
                table: "Credor");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroDaConta_Credor_CredorId",
                table: "RegistroDaConta");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroDaConta_Pagador_PagadorId",
                table: "RegistroDaConta");

            migrationBuilder.DropIndex(
                name: "IX_Arquivo_RegistroDaContaId",
                table: "Arquivo");

            migrationBuilder.DropColumn(
                name: "Conteudo",
                table: "Arquivo");

            migrationBuilder.DropColumn(
                name: "RegistroDaContaId",
                table: "Arquivo");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Arquivo");

            migrationBuilder.AlterColumn<long>(
                name: "Tamanho",
                table: "Arquivo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "BIGINT");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Arquivo",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Extensao",
                table: "Arquivo",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<byte[]>(
                name: "Dados",
                table: "Arquivo",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "ArquivoDoRegistroDaConta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistroDaContaId = table.Column<int>(type: "int", nullable: false),
                    ArquivoId = table.Column<int>(type: "int", nullable: false),
                    ModalidadeDoArquivo = table.Column<int>(type: "int", nullable: false),
                    DataDeCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDeAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoDoRegistroDaConta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivoDoRegistroDaConta_Arquivo_ArquivoId",
                        column: x => x.ArquivoId,
                        principalTable: "Arquivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArquivoDoRegistroDaConta_RegistroDaConta_RegistroDaContaId",
                        column: x => x.RegistroDaContaId,
                        principalTable: "RegistroDaConta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoDoRegistroDaConta_ArquivoId",
                table: "ArquivoDoRegistroDaConta",
                column: "ArquivoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoDoRegistroDaConta_RegistroDaContaId",
                table: "ArquivoDoRegistroDaConta",
                column: "RegistroDaContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credor_SegmentoDoCredor_SegmentoDoCredorId",
                table: "Credor",
                column: "SegmentoDoCredorId",
                principalTable: "SegmentoDoCredor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroDaConta_Credor_CredorId",
                table: "RegistroDaConta",
                column: "CredorId",
                principalTable: "Credor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroDaConta_Pagador_PagadorId",
                table: "RegistroDaConta",
                column: "PagadorId",
                principalTable: "Pagador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credor_SegmentoDoCredor_SegmentoDoCredorId",
                table: "Credor");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroDaConta_Credor_CredorId",
                table: "RegistroDaConta");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroDaConta_Pagador_PagadorId",
                table: "RegistroDaConta");

            migrationBuilder.DropTable(
                name: "ArquivoDoRegistroDaConta");

            migrationBuilder.DropColumn(
                name: "Dados",
                table: "Arquivo");

            migrationBuilder.AlterColumn<long>(
                name: "Tamanho",
                table: "Arquivo",
                type: "BIGINT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Arquivo",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Extensao",
                table: "Arquivo",
                type: "VARCHAR(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<byte[]>(
                name: "Conteudo",
                table: "Arquivo",
                type: "VARBINARY(MAX)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "RegistroDaContaId",
                table: "Arquivo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Arquivo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Arquivo_RegistroDaContaId",
                table: "Arquivo",
                column: "RegistroDaContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivo_RegistroDaConta_RegistroDaContaId",
                table: "Arquivo",
                column: "RegistroDaContaId",
                principalTable: "RegistroDaConta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Credor_SegmentoDoCredor_SegmentoDoCredorId",
                table: "Credor",
                column: "SegmentoDoCredorId",
                principalTable: "SegmentoDoCredor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroDaConta_Credor_CredorId",
                table: "RegistroDaConta",
                column: "CredorId",
                principalTable: "Credor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroDaConta_Pagador_PagadorId",
                table: "RegistroDaConta",
                column: "PagadorId",
                principalTable: "Pagador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
