using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Core.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoDoCampoFotoUrlEmAspNetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "AspNetUsers");
        }
    }
}
