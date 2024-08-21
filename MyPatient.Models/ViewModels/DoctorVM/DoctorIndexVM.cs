using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models.ViewModels.DoctorVM
{
    public class DoctorIndexVM
    {
        public PaginatedList<Doctor> Doctors { get; set; }
        public IEnumerable<SelectListItem> FilterOptions { get; set; }
    }
}
