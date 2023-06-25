using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Player
    {
        public Player()
        {
            this.PlayerStatistics = new HashSet<PlayerStatistic>();
        }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public byte SquadNumber { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public int PositionId { get; set;}
        public virtual Position Position { get; set;}
        public bool IsInjured { get; set; }
        public virtual ICollection<PlayerStatistic> PlayerStatistics { get; set; }
    }
}
