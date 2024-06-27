using System.ComponentModel.DataAnnotations;

namespace MyPatient.Models
{
    public class Patient
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "El Número de Record es requerido.")]
        public string Record { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Identificación es requerida.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[- ]?([0-9]{7})[- ]?([0-9]{1})$", ErrorMessage = "La Identificación no es válida.")]
        [MaxLength(13, ErrorMessage = "La Identificación debe tener 11 digitos.")]
        public string Identification { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Name { get; set; } = string.Empty;
        
        public double? Weight { get; set; }

        public int? Age { get; set; }
        
        public bool Sex { get; set; }
        
        public bool IsInsured { get; set; }
        
        public string? ARS { get; set; }
        
        public string? NSS { get; set; }

        // **********MA REFERENCE***********
        [Required(ErrorMessage = "El MA es requerido.")]
        public int MAId { get; set; }
        
        public MA? MA { get; set; }
    }
}
