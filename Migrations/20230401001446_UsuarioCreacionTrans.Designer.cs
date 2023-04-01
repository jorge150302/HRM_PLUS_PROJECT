﻿// <auto-generated />
using System;
using HRM_PLUS_PROJECT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HRM_PLUS_PROJECT.Migrations
{
    [DbContext(typeof(HRMPlusContext))]
    [Migration("20230401001446_UsuarioCreacionTrans")]
    partial class UsuarioCreacionTrans
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Departamento", b =>
                {
                    b.Property<int>("IdDepartamento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idDepartamento");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDepartamento"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActivo")
                        .HasColumnType("bit")
                        .HasColumnName("isActivo");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UbicacionFisica")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdDepartamento")
                        .HasName("PK__Departam__C225F98D25F0E63B");

                    b.ToTable("Departamento", (string)null);
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Empleado", b =>
                {
                    b.Property<int>("IdEmpleado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idEmpleado");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEmpleado"), 1L, 1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime?>("FechaRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaRegistro")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("IdDepartamento")
                        .HasColumnType("int")
                        .HasColumnName("idDepartamento");

                    b.Property<int>("IdPuesto")
                        .HasColumnType("int")
                        .HasColumnName("idPuesto");

                    b.Property<bool>("IsActivo")
                        .HasColumnType("bit")
                        .HasColumnName("isActivo");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("SalarioMensual")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UsuarioCreacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEmpleado")
                        .HasName("PK__Empleado__5295297CB133B83E");

                    b.HasIndex("IdDepartamento");

                    b.HasIndex("IdPuesto");

                    b.ToTable("Empleado", (string)null);
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Nomina", b =>
                {
                    b.Property<int>("IdNomina")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idNomina");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNomina"), 1L, 1);

                    b.Property<int>("IdDeduccion")
                        .HasColumnType("int")
                        .HasColumnName("idDeduccion");

                    b.Property<int>("IdEmpleado")
                        .HasColumnType("int")
                        .HasColumnName("idEmpleado");

                    b.Property<int>("IdTransaccion")
                        .HasColumnType("int")
                        .HasColumnName("idTransaccion");

                    b.Property<decimal?>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdNomina")
                        .HasName("PK__Nomina__BB6DB6731029EBE3");

                    b.HasIndex("IdDeduccion");

                    b.HasIndex("IdEmpleado");

                    b.HasIndex("IdTransaccion");

                    b.ToTable("Nomina", (string)null);
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Puesto", b =>
                {
                    b.Property<int>("IdPuesto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idPuesto");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPuesto"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActivo")
                        .HasColumnType("bit")
                        .HasColumnName("isActivo");

                    b.Property<string>("NivelRiesgo")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("SalarioMaximo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SalarioMinimo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdPuesto")
                        .HasName("PK__Puesto__ADF48F19C5E13AAC");

                    b.ToTable("Puesto", (string)null);
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.TipoDeduccion", b =>
                {
                    b.Property<int>("IdDeduccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idDeduccion");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDeduccion"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActivo")
                        .HasColumnType("bit")
                        .HasColumnName("isActivo");

                    b.Property<bool>("IsTodoEmpleado")
                        .HasColumnType("bit")
                        .HasColumnName("isTodoEmpleado");

                    b.Property<decimal?>("MaximoRango")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("MinimoRango")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdDeduccion")
                        .HasName("PK__TipoDedu__67750278BF205588");

                    b.ToTable("TipoDeduccion", (string)null);
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.TipoTransaccion", b =>
                {
                    b.Property<int>("IdTipoTransaccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idTipoTransaccion");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTipoTransaccion"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActivo")
                        .HasColumnType("bit")
                        .HasColumnName("isActivo");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdTipoTransaccion")
                        .HasName("PK__TipoTran__8F0EF7B6055FE518");

                    b.ToTable("TipoTransaccion", (string)null);
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Transaccion", b =>
                {
                    b.Property<int>("IdTransaccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idTransaccion");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTransaccion"), 1L, 1);

                    b.Property<DateTime?>("FechaRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaRegistro")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("IdEmpleado")
                        .HasColumnType("int")
                        .HasColumnName("idEmpleado");

                    b.Property<int>("IdTipoTransaccion")
                        .HasColumnType("int")
                        .HasColumnName("idTipoTransaccion");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UsuarioCreacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTransaccion")
                        .HasName("PK__Transacc__5B8761F0641FF522");

                    b.HasIndex("IdEmpleado");

                    b.HasIndex("IdTipoTransaccion");

                    b.ToTable("Transaccion", (string)null);
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Empleado", b =>
                {
                    b.HasOne("HRM_PLUS_PROJECT.Models.Departamento", "IdDepartamentoNavigation")
                        .WithMany("Empleados")
                        .HasForeignKey("IdDepartamento")
                        .IsRequired()
                        .HasConstraintName("FK__Empleado__idDepa__76969D2E");

                    b.HasOne("HRM_PLUS_PROJECT.Models.Puesto", "IdPuestoNavigation")
                        .WithMany("Empleados")
                        .HasForeignKey("IdPuesto")
                        .IsRequired()
                        .HasConstraintName("FK__Empleado__idPues__778AC167");

                    b.Navigation("IdDepartamentoNavigation");

                    b.Navigation("IdPuestoNavigation");
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Nomina", b =>
                {
                    b.HasOne("HRM_PLUS_PROJECT.Models.TipoDeduccion", "IdDeduccionNavigation")
                        .WithMany("Nominas")
                        .HasForeignKey("IdDeduccion")
                        .IsRequired()
                        .HasConstraintName("FK__Nomina__idDeducc__04E4BC85");

                    b.HasOne("HRM_PLUS_PROJECT.Models.Empleado", "IdEmpleadoNavigation")
                        .WithMany("Nominas")
                        .HasForeignKey("IdEmpleado")
                        .IsRequired()
                        .HasConstraintName("FK__Nomina__idEmplea__03F0984C");

                    b.HasOne("HRM_PLUS_PROJECT.Models.Transaccion", "IdTransaccionNavigation")
                        .WithMany("Nominas")
                        .HasForeignKey("IdTransaccion")
                        .IsRequired()
                        .HasConstraintName("FK__Nomina__idTransa__05D8E0BE");

                    b.Navigation("IdDeduccionNavigation");

                    b.Navigation("IdEmpleadoNavigation");

                    b.Navigation("IdTransaccionNavigation");
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Transaccion", b =>
                {
                    b.HasOne("HRM_PLUS_PROJECT.Models.Empleado", "IdEmpleadoNavigation")
                        .WithMany("Transaccions")
                        .HasForeignKey("IdEmpleado")
                        .IsRequired()
                        .HasConstraintName("FK__Transacci__idEmp__7F2BE32F");

                    b.HasOne("HRM_PLUS_PROJECT.Models.TipoTransaccion", "IdTipoTransaccionNavigation")
                        .WithMany("Transaccions")
                        .HasForeignKey("IdTipoTransaccion")
                        .IsRequired()
                        .HasConstraintName("FK__Transacci__idTip__00200768");

                    b.Navigation("IdEmpleadoNavigation");

                    b.Navigation("IdTipoTransaccionNavigation");
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Departamento", b =>
                {
                    b.Navigation("Empleados");
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Empleado", b =>
                {
                    b.Navigation("Nominas");

                    b.Navigation("Transaccions");
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Puesto", b =>
                {
                    b.Navigation("Empleados");
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.TipoDeduccion", b =>
                {
                    b.Navigation("Nominas");
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.TipoTransaccion", b =>
                {
                    b.Navigation("Transaccions");
                });

            modelBuilder.Entity("HRM_PLUS_PROJECT.Models.Transaccion", b =>
                {
                    b.Navigation("Nominas");
                });
#pragma warning restore 612, 618
        }
    }
}
