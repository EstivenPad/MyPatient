using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models.ViewModels
{
    public class PatientIndexVM
    {
        public List<Patient> Patients { get; set; }
        public IEnumerable<SelectListItem> Filters { get; set; }
        public string? SelectedFilter { get; set; }
        public string FilterCriteria { get; set; }
    }
}
