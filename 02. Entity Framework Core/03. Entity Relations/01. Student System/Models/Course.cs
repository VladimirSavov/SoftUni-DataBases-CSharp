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
    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
            this.Resourses = new HashSet<Resourse>();
            this.Homeworks = new HashSet<Homework>();
            this.StudentCourses = new HashSet<StudentCourse>();
        }
        public int CourseId { get; set; }
        [MaxLength(80), Unicode(true)]
        public string Name { get; set; }
        [Unicode(true), AllowNull]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Resourse> Resourses { get; set; }
        public ICollection<Homework> Homeworks { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
