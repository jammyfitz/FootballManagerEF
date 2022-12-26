using FootballManagerEF.Extensions;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
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

            return body.ToString();
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
