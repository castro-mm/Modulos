using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemocaoDoCampoStatusDaConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "RegistroDaConta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "RegistroDaConta",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
