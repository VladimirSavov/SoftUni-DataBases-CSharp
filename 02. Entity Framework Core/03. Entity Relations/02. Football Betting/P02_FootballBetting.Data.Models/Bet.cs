﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Models
{
    public class Bet
    {
        public int BetId { get; set; }
        public decimal Amount { get; set; }
        public virtual Prediction Prediction { get; set; }
        public virtual DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int GameId { get; set; }
        public virtual Game? Game { get; set; }
    }
}
