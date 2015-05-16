using FootballManagerEF.Handlers;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Repositories;
using FootballManagerEF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FootballManagerEF.ViewModels
{
    public class ButtonViewModel : INotifyPropertyChanged
    {
        private IFootballRepository _footballRepository;
        private IPlayerMatchViewModel _playerMatchViewModel;
        private IMatchValidatorService _matchValidatorService;
        private IMailerService _mailerService;
        private Match _selectedMatch;
        private SelectionAlgorithm _selectedAlgorithm;

        public Match SelectedMatch
        {
            get { return _selectedMatch; }
            set { _selectedMatch = value; RaisePropertyChanged("SelectedMatch"); }
        }

        public SelectionAlgorithm SelectedAlgorithm
        {
            get { return _selectedAlgorithm; }
            set { _selectedAlgorithm = value; RaisePropertyChanged("SelectedAlgorithm"); }
        }

        public ObservableCollection<PlayerMatch> PlayerMatches
        {
            get { return _playerMatchViewModel.PlayerMatches; }
            set { _playerMatchViewModel.PlayerMatches = value; RaisePropertyChanged("PlayerMatches"); }
        }

        public ButtonViewModel(IFootballRepository footballRepository, IPlayerMatchViewModel playerMatchViewModel, IMatchValidatorService matchValidatorService, IMailerService mailerService)
        {
            _footballRepository = footballRepository;
            _playerMatchViewModel = playerMatchViewModel;
            _matchValidatorService = matchValidatorService;
            _matchValidatorService.PlayerMatches = _playerMatchViewModel.PlayerMatches;
            _selectedMatch = new Match();
            _mailerService = mailerService;
            _canExecute = true;
        }

        public void UpdateButtonClicked()
        {
            if (_matchValidatorService.DataGridIsValid())
                SaveDataGrid();
            else
                _matchValidatorService.SendErrorToUser();
        }

        public void EmailStatsButtonClicked()
        {
            if (_mailerService.SendStats())
                _mailerService.SendOKToUser();
        }

        public void EmailTeamsButtonClicked()
        {
            if (_matchValidatorService.DataGridIsComplete())
            { 
                _mailerService.SendTeams();
                _mailerService.SendOKToUser();
            } else
                _matchValidatorService.SendErrorToUser();
        }

        public void AutoPickButtonClicked()
        {
            if (_matchValidatorService.DataGridIsComplete())
                ApplyPickingAlgorithm();
            else
                _matchValidatorService.SendErrorToUser();
        }

        private void ApplyPickingAlgorithm()
        {
            PlayerMatches = SelectedAlgorithm.Class.ApplyAlgorithm(PlayerMatches);
        }

        private void SaveDataGrid()
        {
            ObservableCollection<PlayerMatch> playerMatchesToInsert = GetPlayerMatchesToInsert();

            _footballRepository.InsertPlayerMatches(playerMatchesToInsert, SelectedMatch.MatchID);
            _footballRepository.Save();
        }

        public ObservableCollection<PlayerMatch> GetPlayerMatchesToInsert()
        {
            return new ObservableCollection<PlayerMatch>(PlayerMatches.Where(x => (x.PlayerID & x.TeamID) != null));
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

        #region ICommand Members
        private bool _canExecute;

        public ICommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new CommandHandler(() => UpdateButtonClicked(), _canExecute));
            }
        }
        private ICommand _updateCommand;

        public ICommand EmailStatsCommand
        {
            get
            {
                return _emailStatsCommand ?? (_emailStatsCommand = new CommandHandler(() => EmailStatsButtonClicked(), _canExecute));
            }
        }
        private ICommand _emailStatsCommand;

        public ICommand EmailTeamsCommand
        {
            get
            {
                return _emailTeamsCommand ?? (_emailTeamsCommand = new CommandHandler(() => EmailTeamsButtonClicked(), _canExecute));
            }
        }
        private ICommand _emailTeamsCommand;

        public ICommand AutoPickCommand
        {
            get
            {
                return _autoPickCommand ?? (_autoPickCommand = new CommandHandler(() => AutoPickButtonClicked(), _canExecute));
            }
        }
        private ICommand _autoPickCommand;
        #endregion
    }
}
