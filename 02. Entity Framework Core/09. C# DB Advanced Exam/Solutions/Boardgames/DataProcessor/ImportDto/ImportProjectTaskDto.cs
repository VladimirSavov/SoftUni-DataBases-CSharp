using Boardgames.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Creator")]
    public class ImportProjectTaskDto
    {
        [XmlElement("FirstName")]
        [Required]
        [MinLength(2)]
        [MaxLength(7)]
        public string? FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(7)]
        [XmlElement("LastName")]
        public string? LastName { get; set; }
        [XmlArray("Boardgames")]
        public Boardgame[] Boardgames { get; set; }
    }
    [XmlType("Boardgame")]
    public class Boardgame
    {
        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        [XmlElement("Name")]
        public string? Name { get; set; }
        [Required]
        [Range(1, 10)]
        [XmlElement("Rating")]
        public double Rating { get; set; }
        [XmlElement("YearPublished")]
        public int YearPublished { get; set; }
        [XmlElement("CategoryType")]
        public CategoryType CategoryType { get; set; }
        [XmlElement("Mechanics")]
        public string Mechanics { get; set; }
    }
}
