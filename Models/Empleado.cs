using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Nominas = new HashSet<Nomina>();
            Transaccions = new HashSet<Transaccion>();
        }

        [Key]
        [Required]
        public int IdEmpleado { get; set; }
        [Display(Name = "Cédula")]
        [StringLength(11, ErrorMessage = "Minimo permitido de 11 carácteres", MinimumLength = 11)]
        public string Cedula { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        //[Required]
        [Display(Name = "Teléfono")]
        [StringLength(10, ErrorMessage = "Minimo permitido de 10 carácteres", MinimumLength = 10)]
        public string Telefono { get; set; } = null!;
        [Display(Name = "Departamento")]
        public int IdDepartamento { get; set; }
        [Display(Name = "Puesto")]
        public int IdPuesto { get; set; }
        [Display(Name = "Salario Mensual")]
        public decimal SalarioMensual { get; set; }
        [Display(Name = "Fecha Registro")]
        public DateTime? FechaRegistro { get; set; }
        [Display(Name = "Usuario Creación")]
        public string? UsuarioCreacion { get; set; }
        [Display(Name = "Activo?")]
        public bool IsActivo { get; set; }

        [Display(Name = "Departamento")]
        public virtual Departamento? IdDepartamentoNavigation { get; set; }
        [Display(Name = "Puesto")]
        public virtual Puesto? IdPuestoNavigation { get; set; }
        public virtual ICollection<Nomina> Nominas { get; set; }
        public virtual ICollection<Transaccion> Transaccions { get; set; }

        [NotMapped]
        [StringLength(200)]
        [Display(Name = "Nombres y Apellidos")]
        public virtual string FullName { get { return Nombre + " "  + Apellido; } }
    }
}
