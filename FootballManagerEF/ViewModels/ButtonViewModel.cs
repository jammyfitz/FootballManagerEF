﻿using FootballManagerEF.Handlers;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Repositories;
using FootballManagerEF.Services;
using System;
using System.Collections.Generic;
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

        public List<PlayerMatch> PlayerMatches
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

        private void SaveDataGrid()
        {
            List<PlayerMatch> playerMatchesToInsert = GetPlayerMatchesToInsert();

            _footballRepository.InsertPlayerMatches(playerMatchesToInsert, SelectedMatch.MatchID);
            _footballRepository.Save();
        }

        public List<PlayerMatch> GetPlayerMatchesToInsert()
        {
            return PlayerMatches.Where(x => (x.PlayerID & x.TeamID) != null).ToList();
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
        #endregion
    }
}
