using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Helpers
{
    public class PlayerStatsMailHelper : IMailHelper
    {
        private List<PlayerStat> _playerStats;
        private string _toAddress;

        public PlayerStatsMailHelper(List<PlayerStat> playerStats, string toAddress)
        {
            _playerStats = playerStats;
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
            StringBuilder body = new StringBuilder("***MoleInTheBarn v1.3***\n");

            foreach (PlayerStat playerStat in _playerStats)
                body.Append(WritePlayerStatLine(playerStat.PlayerName, playerStat.MatchWins.ToString()));

            return body.ToString();
        }

        private static string WritePlayerStatLine(string playerName, string matchWins)
        {
            return string.Format("{0} : {1}\n", playerName, matchWins);
        }
    }
}
