using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPatient.Models
{
    public class MedicalOrderDetail
    {
        [Key]
        public long MedicalOrderDetailId { get; set; }

        [Required(ErrorMessage = "El Nombre de la Solución es requerida.")]
        public string SolutionName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Dosis es requerida.")]
        [StringLength(50, ErrorMessage = "Longitud máxima de (50) caracteres.")]
        public string Dose { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Frecuencia es requerida.")]
        [StringLength(50, ErrorMessage = "Longitud máxima de (50) caracteres.")]
        public string Frecuency { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Via es requerida.")]
        [StringLength(10, ErrorMessage = "Longitud máxima de (10) caracteres.")]
        public string Via { get; set; } = string.Empty;
        
        [ForeignKey("MedicalOrder")]
        public long MedicalOrderId { get; set; }
        
        [NotMapped]
        public bool IsDeleted { get; set; } = false;
        
        public MedicalOrder? MedicalOrder { get; set; }
    }
}
