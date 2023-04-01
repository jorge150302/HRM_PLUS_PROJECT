using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class TipoDeduccion
    {
        public TipoDeduccion()
        {
            Nominas = new HashSet<Nomina>();
        }

        [Key]
        [Required]
        public int IdDeduccion { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Debe ingresar una descripción")]
        [StringLength(50)]
        public string Descripcion { get; set; } = null!;

        [Display(Name = "Monto")]
        [Required(ErrorMessage = "Debe ingresar un monto")]
        [Range(1, 9999999, ErrorMessage = "Ingresar un monto entre 1 y 9999999")]
        public decimal Monto { get; set; }

        [Display(Name = "¿Son todos los empleados?")]
        public bool IsTodoEmpleado { get; set; }

        [Display(Name = "Mínimo de rango")]
        [Required(ErrorMessage = "Debe ingresar un mínimo de rango")]
        [Range(1, 9999999, ErrorMessage = "Ingresar un monto entre 1 y 9999999")]
        public decimal? MinimoRango { get; set; }

        [Display(Name = "Máximo de rango")]
        [Required(ErrorMessage = "Debe ingresar un máximo de rango")]
        [Range(1, 9999999, ErrorMessage = "Ingresar un monto entre 1 y 9999999")]
        public decimal? MaximoRango { get; set; }

        [Display(Name = "Estado")]
        public bool IsActivo { get; set; }

        public virtual ICollection<Nomina> Nominas { get; set; }
    }
}
