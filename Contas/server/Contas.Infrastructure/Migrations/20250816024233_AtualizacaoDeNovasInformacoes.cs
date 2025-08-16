using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoDeNovasInformacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrosDaConta_Credor_CredorId",
                table: "RegistrosDaConta");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrosDaConta_Pagador_PagadorId",
                table: "RegistrosDaConta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistrosDaConta",
                table: "RegistrosDaConta");

            migrationBuilder.RenameTable(
                name: "RegistrosDaConta",
                newName: "RegistroDaConta");

            migrationBuilder.RenameIndex(
                name: "IX_RegistrosDaConta_PagadorId",
                table: "RegistroDaConta",
                newName: "IX_RegistroDaConta_PagadorId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistrosDaConta_CredorId",
                table: "RegistroDaConta",
                newName: "IX_RegistroDaConta_CredorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistroDaConta",
                table: "RegistroDaConta",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroDaConta_Credor_CredorId",
                table: "RegistroDaConta");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroDaConta_Pagador_PagadorId",
                table: "RegistroDaConta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistroDaConta",
                table: "RegistroDaConta");

            migrationBuilder.RenameTable(
                name: "RegistroDaConta",
                newName: "RegistrosDaConta");

            migrationBuilder.RenameIndex(
                name: "IX_RegistroDaConta_PagadorId",
                table: "RegistrosDaConta",
                newName: "IX_RegistrosDaConta_PagadorId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistroDaConta_CredorId",
                table: "RegistrosDaConta",
                newName: "IX_RegistrosDaConta_CredorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistrosDaConta",
                table: "RegistrosDaConta",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrosDaConta_Credor_CredorId",
                table: "RegistrosDaConta",
                column: "CredorId",
                principalTable: "Credor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrosDaConta_Pagador_PagadorId",
                table: "RegistrosDaConta",
                column: "PagadorId",
                principalTable: "Pagador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
