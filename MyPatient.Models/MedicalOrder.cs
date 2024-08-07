using MyPatient.Web.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPatient.Models
{
    public class MedicalOrder
    {
        [Key]
        public long Id { get; set; }
        
        public TypeMedicalOrder Type { get; set; } = TypeMedicalOrder.Ingreso;
        
        [Required(ErrorMessage = "El Servicio es requerido.")]
        public string Service { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La Sala es requerida.")]
        public string Room { get; set; } = string.Empty;
        
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        
        public TimeOnly CreatedTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
        
        [Required(ErrorMessage = "El Diagnostico es requerido.")]
        public string Diagnostic { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Las Medidas Generales son requeridas.")]
        public string GeneralMeasures { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La Dieta es requerido.")]
        public string Diet { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Las Curas son requeridas.")]
        public string Cures { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La Postura es requerida.")]
        public string Position { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Los Controles Especiales son requeridos.")]
        public string SpecialControls { get; set; } = string.Empty;
        
        public string? DREN { get; set; }
        
        public string? Alergies { get; set; }
        
        public string? Enterconsult { get; set; }
        
        public string? Labs { get; set; }
        
        public string? CountDays { get; set; }
        
        // **********PATIENT***********
        [ForeignKey("Patient")]
        public long? PatientId { get; set; }
        
        public Patient? Patient { get; set; }
        
        // **********MA***********
        [ForeignKey("MA")]
        public int? MAId { get; set; }
       
        public MA? MA { get; set; }
        
        // **********MEDICA ORDER DETAIL***********
        public List<MedicalOrderDetail>? Solutions { get; set; }
    }
}
