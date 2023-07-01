using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType("User")]
    public class ExportSoldProducts
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }
        [XmlElement("lastName")]
        public string LastName { get; set; }
        [XmlElement("soldProducts")]
        public UserProducts[] SoldProducts { get; set; }
    }
    [XmlType("Product")]
    public class UserProducts
    {
        [XmlElement("name")]
        public string Name { get; set;}
        [XmlElement("price")]
        public decimal price { get; set; }
    }

}
