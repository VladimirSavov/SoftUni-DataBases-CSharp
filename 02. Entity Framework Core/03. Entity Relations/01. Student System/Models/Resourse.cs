using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Relations.Models
{
    public class Resourse
    {
        public int ResourseId { get; set; }
        [MaxLength(50), Unicode(true)]
        public string Name { get; set; }
        [Unicode(false)]
        public string Url { get; set; }
        public int MyProperty { get; set; }
        public ResourseType ResourseType { get; set; }

        public int CourseId { get; set; }
    }
}
