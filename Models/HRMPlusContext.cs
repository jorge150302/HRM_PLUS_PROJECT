using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HRM_PLUS_PROJECT.Models;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class HRMPlusContext : DbContext
    {
        public HRMPlusContext()
        {
        }

        public HRMPlusContext(DbContextOptions<HRMPlusContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Nomina> Nominas { get; set; } = null!;
        public virtual DbSet<Puesto> Puestos { get; set; } = null!;
        public virtual DbSet<TipoDeduccion> TipoDeduccions { get; set; } = null!;
        public virtual DbSet<TipoTransaccion> TipoTransaccions { get; set; } = null!;
        public virtual DbSet<Transaccion> Transaccions { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HRMPlus;Integrated Security=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("PK__Departam__C225F98D25F0E63B");

                entity.ToTable("Departamento");

                entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.IsActivo).HasColumnName("isActivo");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.UbicacionFisica).HasMaxLength(50);
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__Empleado__5295297CB133B83E");

                entity.ToTable("Empleado");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.Apellido).HasMaxLength(50);

                entity.Property(e => e.Cedula).HasMaxLength(11);

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");

                entity.Property(e => e.IdPuesto).HasColumnName("idPuesto");

                entity.Property(e => e.IsActivo).HasColumnName("isActivo");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.SalarioMensual).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Empleado__idDepa__76969D2E");

                entity.HasOne(d => d.IdPuestoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdPuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Empleado__idPues__778AC167");
            });

            modelBuilder.Entity<Nomina>(entity =>
            {
                entity.HasKey(e => e.IdNomina)
                    .HasName("PK__Nomina__BB6DB6731029EBE3");

                entity.ToTable("Nomina");

                entity.Property(e => e.IdNomina).HasColumnName("idNomina");

                entity.Property(e => e.IdDeduccion).HasColumnName("idDeduccion");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.IdTransaccion).HasColumnName("idTransaccion");

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdDeduccionNavigation)
                    .WithMany(p => p.Nominas)
                    .HasForeignKey(d => d.IdDeduccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Nomina__idDeducc__04E4BC85");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Nominas)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Nomina__idEmplea__03F0984C");

                entity.HasOne(d => d.IdTransaccionNavigation)
                    .WithMany(p => p.Nominas)
                    .HasForeignKey(d => d.IdTransaccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Nomina__idTransa__05D8E0BE");
            });

            modelBuilder.Entity<Puesto>(entity =>
            {
                entity.HasKey(e => e.IdPuesto)
                    .HasName("PK__Puesto__ADF48F19C5E13AAC");

                entity.ToTable("Puesto");

                entity.Property(e => e.IdPuesto).HasColumnName("idPuesto");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.IsActivo).HasColumnName("isActivo");

                entity.Property(e => e.NivelRiesgo).HasMaxLength(5);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.SalarioMaximo).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SalarioMinimo).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<TipoDeduccion>(entity =>
            {
                entity.HasKey(e => e.IdDeduccion)
                    .HasName("PK__TipoDedu__67750278BF205588");

                entity.ToTable("TipoDeduccion");

                entity.Property(e => e.IdDeduccion).HasColumnName("idDeduccion");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.IsActivo).HasColumnName("isActivo");

                entity.Property(e => e.IsTodoEmpleado).HasColumnName("isTodoEmpleado");

                entity.Property(e => e.MaximoRango).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinimoRango).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoTransaccion>(entity =>
            {
                entity.HasKey(e => e.IdTipoTransaccion)
                    .HasName("PK__TipoTran__8F0EF7B6055FE518");

                entity.ToTable("TipoTransaccion");

                entity.Property(e => e.IdTipoTransaccion).HasColumnName("idTipoTransaccion");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.IsActivo).HasColumnName("isActivo");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.HasKey(e => e.IdTransaccion)
                    .HasName("PK__Transacc__5B8761F0641FF522");

                entity.ToTable("Transaccion");

                entity.Property(e => e.IdTransaccion).HasColumnName("idTransaccion");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.IdTipoTransaccion).HasColumnName("idTipoTransaccion");

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Transaccions)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacci__idEmp__7F2BE32F");

                entity.HasOne(d => d.IdTipoTransaccionNavigation)
                    .WithMany(p => p.Transaccions)
                    .HasForeignKey(d => d.IdTipoTransaccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacci__idTip__00200768");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<HRM_PLUS_PROJECT.Models.Usuario>? Usuario { get; set; }

       
    }
}
