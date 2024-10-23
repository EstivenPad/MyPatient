using MyPatient.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPatient.Models.ViewModels.SurgicalProcedureVM
{
    [NotMapped]
    public class SurgicalProcedureReportVM
    {
        public long SurgicalProcedureID { get; set; }
        public DateOnly CreatedDate { get; set; }

        public string Name_Patient { get; set; }
        public int Age_Patient { get; set; }

        public string Diagnostic { get; set; }
        public string Procedure { get; set; }

        public long ID_Resident { get; set; }
        public string Name_Resident { get; set; }
        public TypeDoctor Level_Resident { get; set; }

        public long ID_Discovery { get; set; }
        public string Discovery { get; set; }

        public string Name_MA { get; set; }
        public TypeDoctor Level_MA { get; set; }

    }
}
