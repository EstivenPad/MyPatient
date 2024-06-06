using System.ComponentModel.DataAnnotations;

namespace MyPatient.Web.Models
{
    public class Patient
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "El Número de Record es requerido.")]
        public string Record { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Cédula es requerida.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[- ]?([0-9]{7})[- ]?([0-9]{1})$", ErrorMessage = "La Cédula no es válida.")]
        [MaxLength(13, ErrorMessage = "La Cédula debe tener 11 digitos.")]
        public string Identification { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Name { get; set; } = string.Empty;
        
        public double? Weight { get; set; }
        
        public int? Age { get; set; }
        
        public bool Sex { get; set; } = true;
        
        public bool IsInsured { get; set; } = false;
        
        public string? ARS { get; set; }
        
        public string? NSS { get; set; }
    }
}
