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
            var body = new StringBuilder("***The Octopus v1.6***\n");
            //body.AppendLine("Name - Wins - Win% - F - A");

            foreach (var playerStatistics in _playerStatisticsData)
                body.AppendLine(WritePlayerStat(playerStatistics.PlayerName, playerStatistics.TotalMatchWins.ToString(), playerStatistics.WinRatio));

            return body.ToString();
        }

        private static string WritePlayerStat(string playerName, string matchWins, decimal winRatio)
        {
            return string.Format("{0} - {1} - {2}%", playerName, matchWins, Math.Round(winRatio, 0, MidpointRounding.AwayFromZero));
        }

        private static string WritePlayerStatLine2(string playerName, string matchWins, decimal winRatio)
        {
            return string.Format("{0} - {1} - {2}%\n", playerName, matchWins, Math.Round(winRatio, 0, MidpointRounding.AwayFromZero));
        }
    }
}
