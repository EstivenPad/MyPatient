﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyPatient.Models.ViewModels.MedicalOrderVM
{
    public class MedicalOrderVM
    {
        public MedicalOrder MedicalOrder { get; set; }

        [ValidateNever]
        public string MA { get; set; }
    }
}
