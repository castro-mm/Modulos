using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Core.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoDoCampoValorPagoParaValorTotalNaTabelaRegistroDaConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorPago",
                table: "RegistroDaConta",
                newName: "ValorTotal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorTotal",
                table: "RegistroDaConta",
                newName: "ValorPago");
        }
    }
}
