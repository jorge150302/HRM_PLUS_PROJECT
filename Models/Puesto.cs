using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class Puesto
    {
        public Puesto()
        {
            Empleados = new HashSet<Empleado>();
        }

        [Key]
        [Required]
        public int IdPuesto { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar una nombre")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Debe ingresar una descripción")]
        [StringLength(50)]
        public string Descripcion { get; set; } = null!;

        [Display(Name = "Nivel de riesgo")]
        [Required(ErrorMessage = "Debe ingresar un nivel de riesgo")]
        public string NivelRiesgo { get; set; } = null!;

        [Display(Name = "Salario mínimo")]
        [Required(ErrorMessage = "Debe ingresar un salario mínimo")]
        [Range(1, 9999999, ErrorMessage = "Ingresar un monto entre 1 y 9999999")]
        public decimal SalarioMinimo { get; set; }
      
        [Display(Name = "Salario máximo")]
        [Required(ErrorMessage = "Debe ingresar un salario máximo")]
        [Range(1, 9999999, ErrorMessage = "Ingresar un monto entre 1 y 9999999")]
        public decimal SalarioMaximo { get; set; }

        [Display(Name = "Estado")]
        public bool IsActivo { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
