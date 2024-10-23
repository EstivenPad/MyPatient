using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models
{
    public class Doctor_SurgicalProcedure
    {
        public long DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public long SurgicalProdecureId { get; set; }
        public SurgicalProcedure? SurgicalProcedure { get; set; }
    }
}
