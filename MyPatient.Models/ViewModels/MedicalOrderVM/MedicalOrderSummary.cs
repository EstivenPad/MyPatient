using MyPatient.Web.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPatient.Models.ViewModels.MedicalOrderVM
{
    [NotMapped]
    public class MedicalOrderSummary
    {
        public long Id { get; set; }
        public MedicalOrderInfo MedicalOrder { get; set; }
        public PatientInfo Patient { get; set; }
        public List<SolutionInfo> Solutions { get; set; }
    }

    public class MedicalOrderInfo
    {
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
    }

    public class SolutionInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Dose { get; set; }
        public string Frecuency { get; set; }
        public string Via { get; set; }
    }

    public class PatientInfo
    {
        public long Id_Patient { get; set; }
        public string Record { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public float? Weight { get; set; }
        public string? ARS { get; set; }
        public string? NSS { get; set; }
        public string Sex { get; set; }

        public string MA { get; set; }
    }
}
