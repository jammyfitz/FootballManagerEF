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
        private ObservableCollection<Match> _matches;
        private ObservableCollection<Team> _teams;
        private Match _selectedMatch;
        private IPlayerMatchViewModel _playerMatchViewModel;
        private IPlayerViewModel _playerViewModel;
        private IDialogService _dialogService;
        private IMatchValidatorService _matchValidatorService;
        private IPlayerValidatorService _playerValidatorService;
        private IMailerService _mailerService;
        private ObservableCollection<SelectionAlgorithm> _selectionAlgorithms;
        private SelectionAlgorithm _selectedAlgorithm;

        public ObservableCollection<Match> Matches
        {
            get { return _matches; }
            set
            {
                _matches = value;
                RaisePropertyChanged("Matches");
            }
        }

        public ObservableCollection<Team> Teams
        {
            get { return _teams; }
            set { _teams = value; }
        }

        public ObservableCollection<SelectionAlgorithm> SelectionAlgorithms
        {
            get { return _selectionAlgorithms; }
            set
            {
                _selectionAlgorithms = value;
                RaisePropertyChanged("SelectionAlgorithms");
            }
        }

        public SelectionAlgorithm SelectedAlgorithm
        {
            get { return _selectedAlgorithm; }
            set
            {
                _selectedAlgorithm = value;
                RaisePropertyChanged("SelectedAlgorithm");
                ButtonViewModel.SelectedAlgorithm = _selectedAlgorithm;
            }
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

        public MatchButtonViewModel MatchButtonViewModel { get; set; }

        public IPlayerViewModel PlayerViewModel
        {
            get { return _playerViewModel; }
            set
            {
                _playerViewModel = value;
                RaisePropertyChanged("PlayerViewModel");
            }
        }

        public MatchViewModel()
        {
            _footballRepository = new FootballRepository(new FootballEntities());
            _playerMatchViewModel = new PlayerMatchViewModel(_footballRepository);
            _dialogService = new DialogService();
            _playerValidatorService = new PlayerValidatorService(_dialogService);
            _matchValidatorService = new MatchValidatorService(_playerMatchViewModel, _dialogService);
            _mailerService = new MailerService(_playerMatchViewModel, _playerMatchViewModel.PlayerMatches, _teams);
            ButtonViewModel = new ButtonViewModel(_footballRepository, _playerMatchViewModel, _matchValidatorService, _mailerService);
            PlayerViewModel = new PlayerViewModel(_footballRepository, _playerMatchViewModel, _playerValidatorService);
            MatchButtonViewModel = new MatchButtonViewModel(_footballRepository, this);
            InitialiseMatchesAndTeams();
            _selectionAlgorithms = InitialiseSelectionAlgorithms();
            _selectedAlgorithm = _selectionAlgorithms.First();
            ButtonViewModel.SelectedAlgorithm = _selectedAlgorithm;
        }

        private void InitialiseMatchesAndTeams()
        {
            _matches = _footballRepository.GetMatches();
            _teams = _footballRepository.GetTeams();
        }

        private ObservableCollection<SelectionAlgorithm> InitialiseSelectionAlgorithms()
        {
            return new ObservableCollection<SelectionAlgorithm>(){
                new SelectionAlgorithm() { Name = "The Giant Killer", Class = new GiantKillerSelectorService(_footballRepository) },
                new SelectionAlgorithm() { Name = "The Proportioner", Class = new TheProportionerSelectorService(_footballRepository) },
                new SelectionAlgorithm() { Name = "The Porter", Class = new ThePorterSelectorService(_footballRepository) },
            };
        }

        public MatchViewModel(IFootballRepository footballRepository)
        {
            _footballRepository = footballRepository;
            InitialiseMatchesAndTeams();
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
