using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgames.DataProcessor.ImportDto
{
    public class ImportSellersDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Address { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [EmailAddress]
        public string Website { get; set; }
        public ImportSellersDto[] Boardgames { get; set; }
    }
    public class ImportSellersDto
    {
        [JsonProperty("Id")]
        public int BoardgameId { get; set; }
    }
}
