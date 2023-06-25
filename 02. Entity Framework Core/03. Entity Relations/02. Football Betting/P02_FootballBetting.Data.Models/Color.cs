using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Color
    {
        public Color()
        {
            this.PrimaryKitTeams = new HashSet<Team>();
            this.SecondaryKitTeams = new HashSet<Team>();
        }
        public int ColorId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Team> PrimaryKitTeams { get; set; }
        public virtual ICollection<Team> SecondaryKitTeams { get; set; }
    }
}
