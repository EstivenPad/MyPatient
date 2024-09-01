using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models.ViewModels.PatientVM
{
    public class PatientUpsertVM
    {
        public Patient Patient { get; set; }

        //For the Create MA modal in Patient View
        public Doctor? MA { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> MADropList { get; set; }
    }
}
