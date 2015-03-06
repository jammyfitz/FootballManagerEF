using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Helpers
{
    public class TeamsMailHelper : IMailHelper
    {
        private ObservableCollection<PlayerMatch> _playerMatches;
        private ObservableCollection<Player> _players;
        private ObservableCollection<Team> _teams;
        private IFootballRepository _footballRepository;
        private string _toAddress;

        public TeamsMailHelper(ObservableCollection<PlayerMatch> playerMatches, IFootballRepository footballRepository, string toAddress)
        {
            _playerMatches = playerMatches;
            _toAddress = toAddress;
            _footballRepository = footballRepository;
            _players = _footballRepository.GetAllPlayers();
            _teams = _footballRepository.GetTeams();
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
            return DateTime.Now.ToString("yyyy-MM-dd") + " - Octopus Teams";
        }

        public string GetBody()
        {
            StringBuilder body = new StringBuilder("***OctopusInTheBarn v1.0***\n");

            foreach (PlayerMatch playerMatch in _playerMatches)
            {
                string teamName = _teams.Single(x => x.TeamID == playerMatch.TeamID).TeamName;
                string playerName = _players.Single(x => x.PlayerID == playerMatch.PlayerID).PlayerName;
                string teamLine = WriteTeamLine(playerName, teamName);
                body.Append(teamLine);
            }

            return body.ToString();
        }

        private string WriteTeamLine(string playerName, string teamName)
        {
            return string.Format("{0} : {1}\n", playerName, _teams.Single(x => x.TeamName == teamName).TeamName);
        }
    }
}
