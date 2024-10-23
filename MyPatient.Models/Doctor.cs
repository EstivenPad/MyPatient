using MyPatient.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyPatient.Models
{
    public class Doctor
    {
        [Key]
        public long Id { get; set; }

        public TypeDoctor Type { get; set; } = TypeDoctor.Residente;

        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Apellido es requerido.")]
        public string LastName { get; set; } = string.Empty;

        [RegularExpression(@"^\(?([0-9]{3})\)?[- ]?([0-9]{7})[- ]?([0-9]{1})$", ErrorMessage = "La Identificación no tiene el formato válido (000-0000000-0).")]
        [Length(11,13,ErrorMessage = "La Identificación debe tener 11 digitos.")]
        public string? Identification { get; set; }

        public bool Sex { get; set; } = false;

        [Required(ErrorMessage = "El Exequatur es requerido.")]
        [RegularExpression(@"^([0-9]{3,4})[- ]?([0-9]{2})", ErrorMessage = "El Exequatur no tiene el formato válido (000-00)/(0000-00).")]
        [Length(5, 7, ErrorMessage = "El Exequatur debe tener de 5 a 6 digitos.")]
        public string Exequatur { get; set; } = string.Empty;
        
        public IList<Doctor_SurgicalProcedure>? DoctorSurgicalProcedures { get; set; }
    }
}
