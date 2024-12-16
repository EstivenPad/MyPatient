using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPatient.Models.ViewModels.MedicalOrderVM
{
    public class MedicalOrderIndexVM
    {
        public Patient Patient { get; set; }
        public PaginatedList<MedicalOrder>? MedicalOrders { get; set; }
        public IEnumerable<SelectListItem> FilterOptions { get; set; }
    }
}
