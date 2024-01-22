using FootballManagerEF.Extensions;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManagerEF.Helpers
{
    public class PlayerStatsMailHelper : IMailHelper
    {
        private List<PlayerStatisticsData> _playerStatisticsData;
        private string _toAddress;

        public PlayerStatsMailHelper(List<PlayerStatisticsData> playerStatisticsData, string toAddress)
        {
            _playerStatisticsData = playerStatisticsData;
            _toAddress = toAddress;
        }

        public string GetFromAddress()
        {
            return _toAddress;
        }

        public List<string> GetToAddresses()
        {
            return new List<string>() { _toAddress };
        }

        public string GetSubject()
        {
            return DateTime.Now.ToString("yyyy-MM-dd") + " - Thursday Football Stats";
        }

        public string GetBody()
        {
            var body = new StringBuilder();
            body.AppendLine("---------------------------------");
            body.AppendLine("   The Octopus v2.0              (.  .)  ");
            body.AppendLine("                                               ((( )))");
            body.AppendLine("---------------------------------");
            body.AppendLine("Name - Wins - Win% - Average For(Total) - Average Against(Total)");

            foreach (var playerStatistics in _playerStatisticsData)
                body.AppendLine(WritePlayerStat(playerStatistics));

            ////AppendAnnualStatistics(body, _playerStatisticsData, 8 );
            
            return body.ToString();
        }

        private void AppendAnnualStatistics(StringBuilder body, List<PlayerStatisticsData> playerStatisticsData, int minimumMatchWinsForReport)
        {
            var playerStatsForAnnualReport = playerStatisticsData.Where(x => x.TotalMatchWins > minimumMatchWinsForReport);
            body.AppendLine($"Season Winner: {playerStatsForAnnualReport.OrderByDescending(x => x.TotalMatchWins).First().PlayerName} ({playerStatsForAnnualReport.Max(x => x.TotalMatchWins)})");
            body.AppendLine($"Best Striker: {playerStatsForAnnualReport.OrderByDescending(x => x.AverageGoalsForPerGame).First().PlayerName} ({playerStatsForAnnualReport.Max(x => x.AverageGoalsForPerGame)})");
            body.AppendLine($"Best Defender: {playerStatsForAnnualReport.OrderBy(x => x.AverageGoalsAgainstPerGame).First().PlayerName} ({playerStatsForAnnualReport.Min(x => x.AverageGoalsAgainstPerGame)})");
        }

        private static string WritePlayerStat(PlayerStatisticsData playerStatisticsData)
        {
            var winRatio = playerStatisticsData.WinRatio.Round();
            var goalsFor = $"{playerStatisticsData.AverageGoalsForPerGame.Round()}({playerStatisticsData.GoalsFor})";
            var goalsAgainst = $"{playerStatisticsData.AverageGoalsAgainstPerGame.Round()}({playerStatisticsData.GoalsAgainst})";

            var playerStatsLine = $"{playerStatisticsData.PlayerName} - {playerStatisticsData.TotalMatchWins} - {winRatio}% - {goalsFor} - {goalsAgainst}";

            return playerStatsLine;
        }
    }
}
