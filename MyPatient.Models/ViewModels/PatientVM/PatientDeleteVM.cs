using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models.ViewModels.PatientVM
{
    public class PatientDeleteVM
    {
        public Patient Patient { get; set; }

        [ValidateNever]
        public string MA { get; set; }
    }
}
