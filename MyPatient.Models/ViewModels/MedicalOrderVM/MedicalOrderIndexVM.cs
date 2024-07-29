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
        public IEnumerable<MedicalOrder>? Income { get; set; }
        public IEnumerable<MedicalOrder>? Daily { get; set; }
        public IEnumerable<MedicalOrder>? PostOperative { get; set; }
    }
}
