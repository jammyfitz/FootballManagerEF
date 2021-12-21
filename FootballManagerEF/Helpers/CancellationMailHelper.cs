using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;

namespace FootballManagerEF.Helpers
{
    public class CancellationMailHelper : IMailHelper
    {
        private ObservableCollection<PlayerMatch> _playerMatches;
        private ObservableCollection<Player> _players;

        private IFootballRepository _footballRepository;
        private string _fromAddress;
        private List<string> _toAddresses;

        public CancellationMailHelper(ObservableCollection<PlayerMatch> playerMatches, IFootballRepository footballRepository, string fromAddress)
        {
            _playerMatches = playerMatches;
            _fromAddress = fromAddress;
            _footballRepository = footballRepository;
            _toAddresses = GetEmailAddresses();
            _players = _footballRepository.GetAllPlayers();
        }

        public string GetFromAddress()
        {
            return _fromAddress;
        }

        public List<string> GetToAddresses()
        {
            return _toAddresses;
        }

        public string GetSubject()
        {
            return DateTime.Now.ToString("yyyy-MM-dd") + " - Thursday Football Cancelled";
        }

        public string GetBody()
        {
            StringBuilder body = new StringBuilder("***The Octopus v1.6***\n\n");

            body.AppendLine("Unfortunately football has been cancelled this week.");

            return body.ToString();
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
