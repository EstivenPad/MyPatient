using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models.ViewModels.MedicalOrderVM
{
    public class MedicalOrderIndexVM
    {
        public Patient Patient { get; set; }
        public IEnumerable<Models.MedicalOrder>? Income { get; set; }
        public IEnumerable<Models.MedicalOrder>? Daily { get; set; }
        public IEnumerable<Models.MedicalOrder>? PostOperative { get; set; }
    }
}
