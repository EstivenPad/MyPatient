using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPatient.Models
{
    public class Patient
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "El Número de Record es requerido.")]
        public string Record { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Identificación es requerida.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[- ]?([0-9]{7})[- ]?([0-9]{1})$", ErrorMessage = "La Identificación no tiene el formato válido (000-0000000-0).")]
        [MaxLength(13, ErrorMessage = "La Identificación debe tener 11 digitos.")]
        public string Identification { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Name { get; set; } = string.Empty;
        
        public double? Weight { get; set; }

        public int? Age { get; set; }
        
        public bool Sex { get; set; } = false;
        
        public bool IsInsured { get; set; }
        
        public string? ARS { get; set; }
        
        public string? NSS { get; set; }

        // **********MA***********
        [Required(ErrorMessage = "Debe seleccionar un MA.")]
        [ForeignKey("MA")]
        public int MAId { get; set; }

        public Doctor? MA { get; set; }
    }
}
