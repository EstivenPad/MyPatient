using MyPatient.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models.ViewModels.MedicalOrderVM
{
    [NotMapped]
    public class MedicalOrderReportVM
    {
        // MEDICAL ORDER INFO
        public long Id { get; set; }
        public TypeMedicalOrder Type { get; set; }
        public string Service { get; set; }
        public string Room { get; set; }
        public DateOnly CreatedDate { get; set; }
        public TimeOnly CreatedTime { get; set; }
        public string Diagnostic { get; set; }
        public string GeneralMeasures { get; set; }
        public string Diet { get; set; }
        public string Cures { get; set; }
        public string Position { get; set; }
        public string SpecialControls { get; set; }
        public string? DREN { get; set; }
        public string Alergies { get; set; }
        public string? Enterconsult { get; set; }
        public string? Labs { get; set; }
        public string? CountDays { get; set; }
        public long? PatientId { get; set; }
        public long? MAId { get; set; }

        // DETAIL INFO
        public long MedicalOrderDetailId { get; set; }
        public string SolutionName { get; set; }
        public string Dose { get; set; }
        public string Frecuency { get; set; }
        public string Via { get; set; }
        public long MedicalOrderId { get; set; }

        // PATIENT INFO
        public long Id_Patient { get; set; }
        public string Record { get; set; }
        public string Name_Patient { get; set; }
        public int? Age { get; set; }
        public float? Weight { get; set; }
        public string? ARS { get; set; }
        public string? NSS { get; set; }
        public string Sex_Patient { get; set; }

        // MA INFO
        public string Name_MA { get; set; }
    }
}
