using System;
using System.Collections.Generic;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class Transaccion
    {
        public Transaccion()
        {
            Nominas = new HashSet<Nomina>();
        }

        public int IdTransaccion { get; set; }
        public int IdEmpleado { get; set; }
        public int IdTipoTransaccion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public decimal? Monto { get; set; }

        public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual TipoTransaccion IdTipoTransaccionNavigation { get; set; } = null!;
        public virtual ICollection<Nomina> Nominas { get; set; }
    }
}
