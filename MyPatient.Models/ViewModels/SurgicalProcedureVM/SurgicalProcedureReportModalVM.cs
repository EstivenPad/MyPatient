using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPatient.Models.ViewModels.SurgicalProcedureVM
{
    public class SurgicalProcedureReportModalVM
    {
        public DateOnly FromDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly ToDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public IEnumerable<SelectListItem> Doctors { get; set; }
        public long SelectedDoctor { get; set; }
    }
}
