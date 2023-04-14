using System.ComponentModel.DataAnnotations;

namespace HRM_PLUS_PROJECT.Models
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
      
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (HRMPlusContext)validationContext.GetService(typeof(HRMPlusContext));

            var Correo = (string)value;

            var usuario = dbContext.Usuarios.FirstOrDefault(u => u.Correo == Correo);

            if (usuario != null)
            {
                return new ValidationResult(ErrorMessage);
               
            }

            return ValidationResult.Success;
        }
    }
}