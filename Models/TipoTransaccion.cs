using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRM_PLUS_PROJECT.Models
{
    public partial class TipoTransaccion
    {
        public TipoTransaccion()
        {
            Transaccions = new HashSet<Transaccion>();
        }

        [Key]
        [Required]

        public int IdTipoTransaccion { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar una nombre")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Debe ingresar una descripción")]
        [StringLength(50)]
        public string Descripcion { get; set; } = null!;

        [Display(Name = "Estado")]
        public bool IsActivo { get; set; }

        public virtual ICollection<Transaccion> Transaccions { get; set; }
    }
}
