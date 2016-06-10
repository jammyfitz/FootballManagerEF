using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FootballManagerEF.Events;

namespace FootballManagerEF.ViewModels
{
    public class PlayerMatchViewModel : IPlayerMatchViewModel, INotifyPropertyChanged
    {
        private IFootballRepository _footballRepository;
        private ObservableCollection<PlayerMatch> _playerMatches;
        private ObservableCollection<Team> _teams;
        private ObservableCollection<Player> _players;

        public ObservableCollection<PlayerMatch> PlayerMatches
        {
            get { return _playerMatches; }

            set
            {
                _playerMatches = value;
                RaisePropertyChanged("PlayerMatches");
            }
        }

        public ObservableCollection<Team> Teams
        {
            get { return _teams; }

            set
            {
                _teams = value;
                RaisePropertyChanged("Teams");
            }
        }     

        public ObservableCollection<Player> Players
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
            _playerMatches = new ObservableCollection<PlayerMatch>();
            _players = GetActivePlayers();
            _teams = GetTeams();
            EventManager<object>.AddEvent("UpdateButtonClicked", UpdateButtonClickedHandler);
        }

        public void UpdateButtonClickedHandler(object sender, GenericEventArgs<object> args)
        {
            _players = GetActivePlayers();
        }

        public ObservableCollection<PlayerMatch> GetPlayerMatches(int matchId)
        {
            return _footballRepository.GetPlayerMatches(matchId);
        }

        public ObservableCollection<Player> GetActivePlayers()
        {
            return _footballRepository.GetActivePlayers();
        }

        public ObservableCollection<Team> GetTeams()
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
