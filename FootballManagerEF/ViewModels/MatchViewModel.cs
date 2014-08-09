using FootballManagerEF.EFModel;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Windows.Controls;

namespace FootballManagerEF.ViewModels
{
    public class MatchViewModel : INotifyPropertyChanged
    {
        private IMatchRepository _matchRepository;
        private IPlayerMatchRepository _playerMatchRepository;
        private List<Match> _matches;
        private Match _selectedMatch;
        private PlayerMatchViewModel _playerMatchViewModel;

        public List<Match> Matches
        {
            get { return _matches; }
            set { _matches = value; }
        }

        public Match SelectedMatch
        {
            get { return _selectedMatch; }

            set
            {
                _selectedMatch = value;
                RaisePropertyChanged("SelectedMatch");
                PlayerMatchVM.PlayerMatches = _playerMatchRepository.GetPlayerMatches(_selectedMatch.MatchID);
                PlayerMatchVM.PropertyChanged += new PropertyChangedEventHandler(PlayerMatchesChanged);
            }
        }

        public PlayerMatchViewModel PlayerMatchVM
        {
            get { return _playerMatchViewModel; }
            set
            {
                _playerMatchViewModel = value;
                RaisePropertyChanged("PlayerMatchViewModel");
            }
        }

        public MatchViewModel()
        {
            _matchRepository = new MatchRepository(new FootballEntities());
            _playerMatchRepository = new PlayerMatchRepository(new FootballEntities());
            _playerMatchViewModel = new PlayerMatchViewModel();
            _matches = GetMatches();
        }

        public MatchViewModel(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public List<Match> GetMatches()
        {
            return _matchRepository.GetMatches();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void PlayerMatchesChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("PlayerMatchesChanged has changed: " + e.PropertyName);
        }

        #endregion
    }
}
