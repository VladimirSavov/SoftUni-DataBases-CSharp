using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_HospitalDatabase.Data.Models.Models
{
    public class Patient
    {
        public Patient()
        {
            this.Prescriptions = new HashSet<PatientMedicament>();
        }
        public int PatientId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set;}
        [MaxLength(50)]
        public string LastName { get; set;}
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(80)]
        public string Email { get; set; }
        public string HasInsurance { get; set; }
        public virtual ICollection<PatientMedicament> Prescriptions { get; set; }
    }
}
