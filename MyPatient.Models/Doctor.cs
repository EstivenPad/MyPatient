using MyPatient.Web.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyPatient.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Apellido es requerido.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Cédula es requerida.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[- ]?([0-9]{7})[- ]?([0-9]{1})$", ErrorMessage = "La Cédula no tiene el formato válido.")]
        [Length(11,13,ErrorMessage = "La Cédula debe tener 11 digitos.")]
        public string? Identification { get; set; }
        
        public bool Sexo { get; set; }

        public MedicalLevel Level { get; set; } = MedicalLevel.R1;

        [Required(ErrorMessage = "La Cédula es requerida.")]
        [RegularExpression(@"^([0-9]{3,4})[- ]?([0-9]{2})", ErrorMessage = "El Exequatur no tiene el formato válido.")]
        [Length(6, 7, ErrorMessage = "El Exequatur debe tener de 5 a 6 digitos.")]
        public string Exequatur { get; set; }
    }
}
