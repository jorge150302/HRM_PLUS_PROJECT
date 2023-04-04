using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM_PLUS_PROJECT.Models
{
    public class Usuario
    {
      
        [Key]
        [Required]
        public int IdUsuario { get; set; }
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Correo")]
        public string Correo { get; set; }
        [Display(Name = "Clave")]
        public string Clave { get; set; }

        [Display(Name = "Rol")]
        public string[]? Roles { get; set; }
        
    }
}
