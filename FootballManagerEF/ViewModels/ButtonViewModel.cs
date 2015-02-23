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

        public Match SelectedMatch
        {
            get { return _selectedMatch; }
            set { _selectedMatch = value; RaisePropertyChanged("SelectedMatch"); }
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

        public void SendEmailButtonClicked()
        {
            if (_mailerService.SendEmail())
                _mailerService.SendOKMessageToUser();
        }

        public void AutoPickButtonClicked()
        {
            //PlayerMatch temp1 = PlayerMatches.ElementAt(0);
            //PlayerMatch temp2 = PlayerMatches.ElementAt(1);

            PlayerMatches.Move(0, 4);
            PlayerMatches.Move(1, 5);

            //ObservableCollection<PlayerMatch> changedPlayerMatches = PlayerMatches;

            //PlayerMatches = changedPlayerMatches;
            //RaisePropertyChanged("PlayerMatches");
        }

        private void SaveDataGrid()
        {
            ObservableCollection<PlayerMatch> playerMatchesToInsert = GetPlayerMatchesToInsert();

            _footballRepository.InsertPlayerMatches(playerMatchesToInsert, SelectedMatch.MatchID);
            _footballRepository.Save();
        }

        public ObservableCollection<PlayerMatch> GetPlayerMatchesToInsert()
        {
            return new ObservableCollection<PlayerMatch>(PlayerMatches.Where(x => (x.PlayerID & x.TeamID) != null).ToList());
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

        public ICommand SendEmailCommand
        {
            get
            {
                return _sendEmailCommand ?? (_sendEmailCommand = new CommandHandler(() => SendEmailButtonClicked(), _canExecute));
            }
        }
        private ICommand _sendEmailCommand;

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
