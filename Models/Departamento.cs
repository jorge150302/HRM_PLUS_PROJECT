using System;
using System.Collections.Generic;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdDepartamento { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string UbicacionFisica { get; set; } = null!;
        public bool? IsActivo { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
