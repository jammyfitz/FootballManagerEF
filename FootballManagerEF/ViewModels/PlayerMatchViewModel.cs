using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.ViewModels
{
    public class PlayerMatchViewModel : IPlayerMatchViewModel, INotifyPropertyChanged
    {
        private IFootballRepository _footballRepository;
        private List<PlayerMatch> _playerMatches;
        private List<Team> _teams;
        private List<Player> _players;

        public List<PlayerMatch> PlayerMatches
        {
            get { return _playerMatches; }

            set
            {
                _playerMatches = value;
                RaisePropertyChanged("PlayerMatches");
            }
        }

        public List<Team> Teams
        {
            get { return _teams; }

            set
            {
                _teams = value;
                RaisePropertyChanged("Teams");
            }
        }     

        public List<Player> Players
        {
            get { return _players; }

            set
            {
                _players = value;
                RaisePropertyChanged("Players");
            }
        }

        public PlayerMatchViewModel(IFootballRepository footballRepository)
        {
            _footballRepository = footballRepository;
            _playerMatches = new List<PlayerMatch>();
            _players = GetActivePlayers();
            _teams = GetTeams();
        }

        public List<PlayerMatch> GetPlayerMatches(int matchId)
        {
            return _footballRepository.GetPlayerMatches(matchId);
        }

        public List<Player> GetActivePlayers()
        {
            return _footballRepository.GetActivePlayers();
        }

        public List<Team> GetTeams()
        {
            return _footballRepository.GetTeams();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
