using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Core.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoDoNomeDoCampoDataDaUltimaModificacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataDaUltimoModificacao",
                table: "Arquivo",
                newName: "DataDaUltimaModificacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataDaUltimaModificacao",
                table: "Arquivo",
                newName: "DataDaUltimoModificacao");
        }
    }
}
