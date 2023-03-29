using System;
using System.Collections.Generic;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class TipoTransaccion
    {
        public TipoTransaccion()
        {
            Transaccions = new HashSet<Transaccion>();
        }

        public int IdTipoTransaccion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool IsActivo { get; set; }

        public virtual ICollection<Transaccion> Transaccions { get; set; }
    }
}
