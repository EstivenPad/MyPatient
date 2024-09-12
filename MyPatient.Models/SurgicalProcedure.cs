using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models
{
    public class SurgicalProcedure
    {
        [Key]
        public long Id { get; set; }
        
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required(ErrorMessage = "El Diagnostico es requerido.")]
        public string Diagnostic { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Procedimiento es requerido.")]
        public string Procedure { get; set; } = string.Empty;

        public List<SurgicalProcedureDiscoveries>? Discoveries { get; set; }


        [ForeignKey("Patient")]
        public long? PatientId { get; set; }

        public Patient? Patient { get; set; }


        public List<Doctor_SurgicalProcedure>? DoctorSurgicalProcedures { get; set; }
    }
}
