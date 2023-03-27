using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class Puesto
    {
        public Puesto()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdPuesto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string NivelRiesgo { get; set; } = null!;
        [Required]
        public decimal SalarioMinimo { get; set; }
        [Required]
        public decimal SalarioMaximo { get; set; }
        public bool IsActivo { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
