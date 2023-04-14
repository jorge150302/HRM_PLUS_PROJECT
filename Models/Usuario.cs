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
        
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Debe ingresar un correo")]
        [UniqueEmail(ErrorMessage = "Este correo electrónico ya está en uso")]
        [EmailAddress (ErrorMessage = "Formato de correo requerido")]
        public string Correo { get; set; }

        [Display(Name = "Clave")]
        [Required(ErrorMessage = "La clave debe tener más de 8 caracteres")]
        [StringLength(9999999, MinimumLength = 8, ErrorMessage = "La clave debe tener más de 8 caracteres")]
        public string Clave { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Debe ingresar un rol")]
        public string Roles { get; set; }
        
    }
}
