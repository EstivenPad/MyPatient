using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Models
{
    public class SurgicalProcedureDiscoveries
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "La Descripción es requerida.")]
        public string Description { get; set; } = string.Empty;

        [ForeignKey("SurgicalProcedure")]
        public long SurgicalProcedureId { get; set; }
    }
}
