namespace FootballManagerEF.Models
{
    public class PlayerStatisticsData
    {
        public string PlayerName { get; set; }
        public int TotalMatchWins { get; set; }
        public decimal WinRatio { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
    }
}
