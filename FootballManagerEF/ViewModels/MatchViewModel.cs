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
using System.Windows.Controls;
using FootballManagerEF.Services;

namespace FootballManagerEF.ViewModels
{
    public class MatchViewModel : INotifyPropertyChanged
    {
        private IFootballRepository _footballRepository;
        private List<Match> _matches;
        private List<Team> _teams;
        private Match _selectedMatch;
        private IPlayerMatchViewModel _playerMatchViewModel;
        private IPlayerViewModel _playerViewModel;
        private IDialogService _dialogService;
        private IMatchValidatorService _matchValidatorService;
        private IPlayerValidatorService _playerValidatorService;
        private IMailerService _mailerService;

        public List<Match> Matches
        {
            get { return _matches; }
            set { _matches = value; }
        }

        public List<Team> Teams
        {
            get { return _teams; }
            set { _teams = value; }
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

        public IPlayerViewModel PlayerViewModel
        {
            get { return _playerViewModel; }
            set
            {
                _playerViewModel = value;
                RaisePropertyChanged("PlayerViewModel");
            }
        }

        public ButtonViewModel ButtonViewModel { get; set; }

        public MatchViewModel()
        {
            _footballRepository = new FootballRepository(new FootballEntities());
            _playerMatchViewModel = new PlayerMatchViewModel(_footballRepository);
            _dialogService = new DialogService();
            _playerValidatorService = new PlayerValidatorService(_dialogService);
            _matchValidatorService = new MatchValidatorService(_playerMatchViewModel, _dialogService);
            _mailerService = new MailerService();
            ButtonViewModel = new ButtonViewModel(_footballRepository, _playerMatchViewModel, _matchValidatorService, _mailerService);
            PlayerViewModel = new PlayerViewModel(_footballRepository, _playerValidatorService);
            InitialiseMatchesAndTeams();
        }

        private void InitialiseMatchesAndTeams()
        {
            Matches = GetMatches();
            Teams = GetTeams();
        }

        public MatchViewModel(IFootballRepository footballRepository)
        {
            _footballRepository = footballRepository;
            InitialiseMatchesAndTeams();
        }

        public List<Match> GetMatches()
        {
            return _footballRepository.GetMatches();
        }

        public List<Team> GetTeams()
        {
            return _footballRepository.GetTeams();
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
        #endregion
    }
}
