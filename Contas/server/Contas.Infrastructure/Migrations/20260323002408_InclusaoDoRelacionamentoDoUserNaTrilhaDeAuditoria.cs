using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Core.Migrations
{
    /// <inheritdoc />
    public partial class InclusaoDoRelacionamentoDoUserNaTrilhaDeAuditoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrilhaDeAuditoria_AspNetUsers_UserId",
                table: "TrilhaDeAuditoria");

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
                name: "FK_TrilhaDeAuditoria_AspNetUsers_UserId",
                table: "TrilhaDeAuditoria");

            migrationBuilder.AddForeignKey(
                name: "FK_TrilhaDeAuditoria_AspNetUsers_UserId",
                table: "TrilhaDeAuditoria",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
