using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Models
{
    public class PlayerData
    {
        public PlayerMatch PlayerMatch { get; set; }
        public int? MatchWins { get; set; }
        public int? MatchesPlayed { get; set; }
        public decimal? WinRatio { get; set; }
    }
}
