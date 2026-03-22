using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Core.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoDoUseridNasTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TrilhaDeAuditoria",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SegmentoDoCredor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "RegistroDaConta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Pagador",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "LogDeErro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Credor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ArquivoDoRegistroDaConta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Arquivo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TrilhaDeAuditoria_UserId",
                table: "TrilhaDeAuditoria",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentoDoCredor_UserId",
                table: "SegmentoDoCredor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroDaConta_UserId",
                table: "RegistroDaConta",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagador_UserId",
                table: "Pagador",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LogDeErro_UserId",
                table: "LogDeErro",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Credor_UserId",
                table: "Credor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoDoRegistroDaConta_UserId",
                table: "ArquivoDoRegistroDaConta",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Arquivo_UserId",
                table: "Arquivo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivo_AspNetUsers_UserId",
                table: "Arquivo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArquivoDoRegistroDaConta_AspNetUsers_UserId",
                table: "ArquivoDoRegistroDaConta",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Credor_AspNetUsers_UserId",
                table: "Credor",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LogDeErro_AspNetUsers_UserId",
                table: "LogDeErro",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagador_AspNetUsers_UserId",
                table: "Pagador",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroDaConta_AspNetUsers_UserId",
                table: "RegistroDaConta",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SegmentoDoCredor_AspNetUsers_UserId",
                table: "SegmentoDoCredor",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrilhaDeAuditoria_AspNetUsers_UserId",
                table: "TrilhaDeAuditoria",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arquivo_AspNetUsers_UserId",
                table: "Arquivo");

            migrationBuilder.DropForeignKey(
                name: "FK_ArquivoDoRegistroDaConta_AspNetUsers_UserId",
                table: "ArquivoDoRegistroDaConta");

            migrationBuilder.DropForeignKey(
                name: "FK_Credor_AspNetUsers_UserId",
                table: "Credor");

            migrationBuilder.DropForeignKey(
                name: "FK_LogDeErro_AspNetUsers_UserId",
                table: "LogDeErro");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagador_AspNetUsers_UserId",
                table: "Pagador");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroDaConta_AspNetUsers_UserId",
                table: "RegistroDaConta");

            migrationBuilder.DropForeignKey(
                name: "FK_SegmentoDoCredor_AspNetUsers_UserId",
                table: "SegmentoDoCredor");

            migrationBuilder.DropForeignKey(
                name: "FK_TrilhaDeAuditoria_AspNetUsers_UserId",
                table: "TrilhaDeAuditoria");

            migrationBuilder.DropIndex(
                name: "IX_TrilhaDeAuditoria_UserId",
                table: "TrilhaDeAuditoria");

            migrationBuilder.DropIndex(
                name: "IX_SegmentoDoCredor_UserId",
                table: "SegmentoDoCredor");

            migrationBuilder.DropIndex(
                name: "IX_RegistroDaConta_UserId",
                table: "RegistroDaConta");

            migrationBuilder.DropIndex(
                name: "IX_Pagador_UserId",
                table: "Pagador");

            migrationBuilder.DropIndex(
                name: "IX_LogDeErro_UserId",
                table: "LogDeErro");

            migrationBuilder.DropIndex(
                name: "IX_Credor_UserId",
                table: "Credor");

            migrationBuilder.DropIndex(
                name: "IX_ArquivoDoRegistroDaConta_UserId",
                table: "ArquivoDoRegistroDaConta");

            migrationBuilder.DropIndex(
                name: "IX_Arquivo_UserId",
                table: "Arquivo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TrilhaDeAuditoria");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SegmentoDoCredor");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RegistroDaConta");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pagador");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LogDeErro");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Credor");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ArquivoDoRegistroDaConta");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Arquivo");
        }
    }
}
