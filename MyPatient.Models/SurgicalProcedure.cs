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

        public string Diagnostic { get; set; } = string.Empty;

        public string Procedure { get; set; } = string.Empty;


        // **********PATIENT***********
        [ForeignKey("Patient")]
        public long? PatientId { get; set; }

        public Patient? Patient { get; set; }

        // ***********Many To Many************
        public ICollection<Doctor_SurgicalProcedure> DoctorSurgicalProcedures { get; set; }
    }
}
