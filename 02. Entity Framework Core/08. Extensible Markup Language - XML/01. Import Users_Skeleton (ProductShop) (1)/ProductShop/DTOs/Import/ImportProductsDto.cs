using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Import
{
    [XmlType("Products")]
    public class ImportProductsDto
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
        [XmlElement("sellerId")]
        public int sellerId { get; set; }
        [XmlElement("buyerId")]
        public int buyerId { get; set; }
    }
}
//< Product >
//       < name > Care One Hemorrhoidal</name>
//       <price>932.18</price>
//       <sellerId>25</sellerId>
//       <buyerId>24</buyerId>
//   </Product>