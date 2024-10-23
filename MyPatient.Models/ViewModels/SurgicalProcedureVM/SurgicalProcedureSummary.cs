using MyPatient.Models.Enums;

namespace MyPatient.Models.ViewModels.SurgicalProcedureVM
{
    public class SurgicalProcedureSummary
    {
        public long Id { get; set; }
        public SurgicalProcedureInfo SurgicalProcedure { get; set; }
        public List<DiscoveryInfo> Discoveries { get; set; }
        public List<ResidentInfo> Residents { get; set; }
    }

    public class SurgicalProcedureInfo
    {
        public DateOnly CreatedDate { get; set; }
        public string Name_Patient { get; set; }
        public int Age_Patient { get; set; }
        public string Diagnostic { get; set; }
        public string Procedure { get; set; }
        public string Name_MA { get; set; }
        public TypeDoctor Level_MA { get; set; }
    }

    public class DiscoveryInfo
    {
        public long ID { get; set; }
        public string Description { get; set; }
    }

    public class ResidentInfo
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public TypeDoctor Level { get; set; }
    }
}
