using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM_PLUS_PROJECT.Migrations
{
    public partial class FechaResgitroNomina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "Nomina",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "Nomina");
        }
    }
}
