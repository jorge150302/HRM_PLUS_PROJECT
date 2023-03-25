using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRM_PLUS_PROJECT.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    idDepartamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UbicacionFisica = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    isActivo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departam__C225F98D25F0E63B", x => x.idDepartamento);
                });

            migrationBuilder.CreateTable(
                name: "Puesto",
                columns: table => new
                {
                    idPuesto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NivelRiesgo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    SalarioMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalarioMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Puesto__ADF48F19C5E13AAC", x => x.idPuesto);
                });

            migrationBuilder.CreateTable(
                name: "TipoDeduccion",
                columns: table => new
                {
                    idDeduccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isTodoEmpleado = table.Column<bool>(type: "bit", nullable: false),
                    MinimoRango = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaximoRango = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    isActivo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoDedu__67750278BF205588", x => x.idDeduccion);
                });

            migrationBuilder.CreateTable(
                name: "TipoTransaccion",
                columns: table => new
                {
                    idTipoTransaccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    isActivo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoTran__8F0EF7B6055FE518", x => x.idTipoTransaccion);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    idEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    idDepartamento = table.Column<int>(type: "int", nullable: false),
                    idPuesto = table.Column<int>(type: "int", nullable: false),
                    SalarioMensual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    isActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empleado__5295297CB133B83E", x => x.idEmpleado);
                    table.ForeignKey(
                        name: "FK__Empleado__idDepa__76969D2E",
                        column: x => x.idDepartamento,
                        principalTable: "Departamento",
                        principalColumn: "idDepartamento");
                    table.ForeignKey(
                        name: "FK__Empleado__idPues__778AC167",
                        column: x => x.idPuesto,
                        principalTable: "Puesto",
                        principalColumn: "idPuesto");
                });

            migrationBuilder.CreateTable(
                name: "Transaccion",
                columns: table => new
                {
                    idTransaccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpleado = table.Column<int>(type: "int", nullable: false),
                    idTipoTransaccion = table.Column<int>(type: "int", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transacc__5B8761F0641FF522", x => x.idTransaccion);
                    table.ForeignKey(
                        name: "FK__Transacci__idEmp__7F2BE32F",
                        column: x => x.idEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "idEmpleado");
                    table.ForeignKey(
                        name: "FK__Transacci__idTip__00200768",
                        column: x => x.idTipoTransaccion,
                        principalTable: "TipoTransaccion",
                        principalColumn: "idTipoTransaccion");
                });

            migrationBuilder.CreateTable(
                name: "Nomina",
                columns: table => new
                {
                    idNomina = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEmpleado = table.Column<int>(type: "int", nullable: false),
                    idDeduccion = table.Column<int>(type: "int", nullable: false),
                    idTransaccion = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Nomina__BB6DB6731029EBE3", x => x.idNomina);
                    table.ForeignKey(
                        name: "FK__Nomina__idDeducc__04E4BC85",
                        column: x => x.idDeduccion,
                        principalTable: "TipoDeduccion",
                        principalColumn: "idDeduccion");
                    table.ForeignKey(
                        name: "FK__Nomina__idEmplea__03F0984C",
                        column: x => x.idEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "idEmpleado");
                    table.ForeignKey(
                        name: "FK__Nomina__idTransa__05D8E0BE",
                        column: x => x.idTransaccion,
                        principalTable: "Transaccion",
                        principalColumn: "idTransaccion");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_idDepartamento",
                table: "Empleado",
                column: "idDepartamento");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_idPuesto",
                table: "Empleado",
                column: "idPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_Nomina_idDeduccion",
                table: "Nomina",
                column: "idDeduccion");

            migrationBuilder.CreateIndex(
                name: "IX_Nomina_idEmpleado",
                table: "Nomina",
                column: "idEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Nomina_idTransaccion",
                table: "Nomina",
                column: "idTransaccion");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_idEmpleado",
                table: "Transaccion",
                column: "idEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_idTipoTransaccion",
                table: "Transaccion",
                column: "idTipoTransaccion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nomina");

            migrationBuilder.DropTable(
                name: "TipoDeduccion");

            migrationBuilder.DropTable(
                name: "Transaccion");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "TipoTransaccion");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "Puesto");
        }
    }
}
