using System;
using System.Collections.Generic;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Nominas = new HashSet<Nomina>();
            Transaccions = new HashSet<Transaccion>();
        }

        public int IdEmpleado { get; set; }
        public string Cedula { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int IdDepartamento { get; set; }
        public int IdPuesto { get; set; }
        public decimal SalarioMensual { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool IsActivo { get; set; }

        public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
        public virtual Puesto IdPuestoNavigation { get; set; } = null!;
        public virtual ICollection<Nomina> Nominas { get; set; }
        public virtual ICollection<Transaccion> Transaccions { get; set; }
    }
}
