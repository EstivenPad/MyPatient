using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models.ViewModels.MedicalOrderVM
{
    public class MedicalOrderVM
    {
        public MedicalOrder MedicalOrder { get; set; }

        [ValidateNever]
        public string MA { get; set; }
    }
}
