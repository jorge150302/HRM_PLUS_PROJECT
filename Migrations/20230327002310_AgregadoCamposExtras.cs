using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM_PLUS_PROJECT.Migrations
{
    public partial class AgregadoCamposExtras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Empleado",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "Empleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "Empleado");
        }
    }
}
