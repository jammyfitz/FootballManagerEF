using System.Collections.Generic;

namespace FootballManagerEF.Models
{
    public class PlayerCalculation
    {
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public int? TotalMatchWins { get; set; }
        public int? MatchesPlayed { get; set; }
        public decimal? WinRatio { get; set; }
        public int? RecentMatchWins { get; set; }
        public IEnumerable<PlayerMatch> RecentMatches { get; internal set; }
    }
}
