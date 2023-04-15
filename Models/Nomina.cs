using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class Nomina
    {
        public int IdNomina { get; set; }
        [Display(Name = "Empleado")]
        public int IdEmpleado { get; set; }
        [Display(Name = "Deducción")]
        public int IdDeduccion { get; set; }
        [Display(Name = "Transacción")]
        public int IdTransaccion { get; set; }
        [Required(ErrorMessage = "Monto es obligatorio")]
        [Range(1, 9999999, ErrorMessage = "Ingresar un monto entre 1 y 9999999")]
        public decimal Monto { get; set; }
        public DateTime? FechaRegistro { get; set; }
        [Display(Name = "Deducción")]
        public virtual TipoDeduccion? IdDeduccionNavigation { get; set; } = null!;
        [Display(Name = "Empleado")]
        public virtual Empleado? IdEmpleadoNavigation { get; set; } = null!;
        [Display(Name = "Transacción")]
        public virtual Transaccion? IdTransaccionNavigation { get; set; } = null!;
    }
}
