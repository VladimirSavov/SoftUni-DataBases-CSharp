using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_HospitalDatabase.Data.Models.Models
{
    public class PatientMedicament
    {
        [Key]
        public int PAtientId1 { get; set; }
        public Patient Patient { get; set; }
        public Medicament Medicament { get; set; }
    }
}
