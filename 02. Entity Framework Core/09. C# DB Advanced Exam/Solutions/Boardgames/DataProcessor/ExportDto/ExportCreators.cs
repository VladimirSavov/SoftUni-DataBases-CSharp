using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ExportDto
{
    [XmlType("Creator")]
    public class ExportCreators
    {
        [XmlElement("CreatorName")]
        public string CreatorName { get; set; }
        [XmlElement("BoardGames")]
        public BoardGame[] BoardGames { get; set; }
    }
    [XmlType("BoardGame")]
    public class BoardGame 
    {
        [XmlElement("BoardgameName")]
        public string BoardgameName { get; set; }
        [XmlElement("BoardgameYearPublished")]
        public int BoardgameYearPublished { get; set; }
    }

}
