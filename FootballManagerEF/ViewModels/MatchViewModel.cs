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
using System.Web.Mvc;
using System.Windows.Controls;

namespace FootballManagerEF.ViewModels
{
    public class MatchViewModel : INotifyPropertyChanged
    {
        private IFootballRepository _footballRepository;
        private List<Match> _matches;
        private Match _selectedMatch;
        private IPlayerMatchViewModel _playerMatchViewModel;

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
                PlayerMatchViewModel.PlayerMatches = _footballRepository.GetTenPlayerMatches(_selectedMatch.MatchID);
                ButtonViewModel.SelectedMatch = _selectedMatch;
            }
        }

        public IPlayerMatchViewModel PlayerMatchViewModel
        {
            get { return _playerMatchViewModel; }
            set
            {
                _playerMatchViewModel = value;
                RaisePropertyChanged("PlayerMatchViewModel");
            }
        }

        public ButtonViewModel ButtonViewModel { get; set; }

        public MatchViewModel()
        {
            _footballRepository = new FootballRepository(new FootballEntities());
            _playerMatchViewModel = new PlayerMatchViewModel(_footballRepository);
            ButtonViewModel = new ButtonViewModel(_footballRepository, _playerMatchViewModel, null);
            _matches = GetMatches();
        }

        public MatchViewModel(IFootballRepository footballRepository)
        {
            _footballRepository = footballRepository;
        }

        public List<Match> GetMatches()
        {
            return _footballRepository.GetMatches();
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

        /*private void PlayerMatchesChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }*/

        #endregion
    }
}
