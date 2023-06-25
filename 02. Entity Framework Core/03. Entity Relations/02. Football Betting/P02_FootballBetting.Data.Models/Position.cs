using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Position
    {
        public Position()
        {
            this.Players = new List<Player>();
        }
        public int PositionId { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public string Name { get; set; }
    }
}
