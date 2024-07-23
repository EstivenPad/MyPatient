using MyPatient.Web.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyPatient.Models
{
    public class MA
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Apellido es requerido.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Identificación es requerida.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[- ]?([0-9]{7})[- ]?([0-9]{1})$", ErrorMessage = "La Identificación no es válido.")]
        [Length(11,13,ErrorMessage = "La Identificación debe tener 11 digitos.")]
        public string? Identification { get; set; }

        public bool Sex { get; set; } = false;

        [Required(ErrorMessage = "El Exequatur es requerido.")]
        [RegularExpression(@"^([0-9]{3,4})[- ]?([0-9]{2})", ErrorMessage = "El Exequatur no es válido.")]
        [Length(5, 7, ErrorMessage = "El Exequatur debe tener de 5 a 6 digitos.")]
        public string Exequatur { get; set; } = string.Empty;
    }
}
