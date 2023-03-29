using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            Empleados = new HashSet<Empleado>();
        }


        [Key]
        [Required]
        public int IdDepartamento { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Debe ingresar una descripción")]
        [StringLength(50)]
        public string Descripcion { get; set; } = null!;

        [Display(Name = "Ubicación física")]
        [Required(ErrorMessage = "Debe ingresar una ubicación física")]
        [StringLength(50)]
        public string UbicacionFisica { get; set; } = null!;

        [Display(Name = "Estado")]
        public bool IsActivo { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
