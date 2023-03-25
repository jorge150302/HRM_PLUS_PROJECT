using System;
using System.Collections.Generic;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class Nomina
    {
        public int IdNomina { get; set; }
        public int IdEmpleado { get; set; }
        public int IdDeduccion { get; set; }
        public int IdTransaccion { get; set; }
        public decimal? Monto { get; set; }

        public virtual TipoDeduccion IdDeduccionNavigation { get; set; } = null!;
        public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual Transaccion IdTransaccionNavigation { get; set; } = null!;
    }
}
