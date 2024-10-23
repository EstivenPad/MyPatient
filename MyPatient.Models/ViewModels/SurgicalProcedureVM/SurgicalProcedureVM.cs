using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models.ViewModels.SurgicalProcedureVM
{
    public class SurgicalProcedureVM
    {
        public SurgicalProcedure SurgicalProcedure { get; set; }

        [ValidateNever]
        public string NameMA { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ResidentDroplist { get; set; }
    }
}
