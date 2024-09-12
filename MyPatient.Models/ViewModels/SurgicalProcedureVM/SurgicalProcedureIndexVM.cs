using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models.ViewModels.SurgicalProcedureVM
{
    public class SurgicalProcedureIndexVM
    {
        public PaginatedList<SurgicalProcedure> SurgicalProcedures { get; set; }
        public IEnumerable<SelectListItem> FilterOptions { get; set; }
    }
}
