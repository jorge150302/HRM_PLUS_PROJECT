using System;
using System.Collections.Generic;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class TipoDeduccion
    {
        public TipoDeduccion()
        {
            Nominas = new HashSet<Nomina>();
        }

        public int IdDeduccion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Monto { get; set; }
        public bool IsTodoEmpleado { get; set; }
        public decimal? MinimoRango { get; set; }
        public decimal? MaximoRango { get; set; }
        public bool? IsActivo { get; set; }

        public virtual ICollection<Nomina> Nominas { get; set; }
    }
}
