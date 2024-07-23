using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models.ViewModels
{
    public class PatientUpsertVM
    {
        public Patient Patient { get; set; }

        public MA? MA { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> MAs { get; set; }
    }
}
