using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Core.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoDosCamposCriadoPorEDatasEmAspNetRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeAtualizacao",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeCriacao",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DataDeAtualizacao",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DataDeCriacao",
                table: "AspNetRoles");
        }
    }
}
