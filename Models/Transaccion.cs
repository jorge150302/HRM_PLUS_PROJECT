using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class Transaccion
    {
        public Transaccion()
        {
            Nominas = new HashSet<Nomina>();
        }

        [Key]
        [Required]
        public int IdTransaccion { get; set; }
        [Display(Name = "Empleado")]
        public int IdEmpleado { get; set; }
        [Display(Name = "Tipo Transacción")]
        public int IdTipoTransaccion { get; set; }
        [Display(Name = "Fecha Registro")]
        public DateTime? FechaRegistro { get; set; }
        [Range(1, 9999999, ErrorMessage = "Ingresar un monto entre 1 y 9999999")]
        [Required(ErrorMessage = "Monto es obligatorio")]
        public decimal Monto { get; set; }
        [Display(Name = "Usuario Creación")]
        public string? UsuarioCreacion { get; set; }

        [Display(Name = "Empleado")]
        public virtual Empleado? IdEmpleadoNavigation { get; set; } = null!;
        [Display(Name = "Tipo Transacción")]
        public virtual TipoTransaccion? IdTipoTransaccionNavigation { get; set; } = null!;
        public virtual ICollection<Nomina> Nominas { get; set; }
    }
}
