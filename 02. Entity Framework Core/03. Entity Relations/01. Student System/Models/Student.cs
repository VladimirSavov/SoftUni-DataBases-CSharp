using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Relations.Models
{
    public class Student
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
            this.Homeworks = new HashSet<Homework>();
            this.StudentCourses = new HashSet<StudentCourse>();
        }
        public int StudentId { get; set; }
        [MaxLength(100), Unicode(true)]
        public string? Name { get; set; }
        [StringLength(10), MaxLength(10)]
        public string PhoneNumber { get; set; }
        public string? RegisteredOn { get; set; }
        [AllowNull]
        public DateTime Birthday { get; set; }
        public ICollection<Course> Courses{ get; set; }
        public ICollection<Homework> Homeworks { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
