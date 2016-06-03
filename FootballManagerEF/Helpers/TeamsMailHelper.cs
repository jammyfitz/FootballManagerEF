using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
        private string _fromAddress;
        private List<string> _toAddresses;

        public TeamsMailHelper(ObservableCollection<PlayerMatch> playerMatches, IFootballRepository footballRepository, string fromAddress)
        {
            _playerMatches = playerMatches;
            _fromAddress = fromAddress;
            _footballRepository = footballRepository;
            _toAddresses = GetEmailAddresses();
            _players = _footballRepository.GetAllPlayers();
            _teams = _footballRepository.GetTeams();
        }

        public string GetFromAddress()
        {
            return _fromAddress;
        }

        public List<string> GetToAddresses()
        {
            return  _toAddresses;
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

        private List<string> GetEmailAddresses()
        {
            var organiserEmail = ConfigurationManager.AppSettings["OrganiserEmail"];
            var toAddresses = _footballRepository.GetEmailAddresses(_playerMatches.Select(x => x.PlayerID).ToList());

            if (!toAddresses.Contains(organiserEmail))
                toAddresses.Add(organiserEmail);

            return toAddresses;
        }
    }
}
