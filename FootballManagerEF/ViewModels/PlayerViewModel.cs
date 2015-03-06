using FootballManagerEF.Handlers;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FootballManagerEF.ViewModels
{
    public class PlayerViewModel : IPlayerViewModel, INotifyPropertyChanged
    {
        private IFootballRepository _footballRepository;
        private ObservableCollection<Player> _players;
        private IPlayerValidatorService _playerValidatorService;
        private IPlayerMatchViewModel _playerMatchViewModel;

        public ObservableCollection<Player> Players
        {
            get { return _players; }

            set
            {
                _players = value;
                RaisePropertyChanged("Players");
            }
        }

        public PlayerViewModel(IFootballRepository footballRepository, IPlayerMatchViewModel playerMatchViewModel, IPlayerValidatorService playerValidatorService)
        {
            _footballRepository = footballRepository;
            _playerMatchViewModel = playerMatchViewModel;
            _players = GetAllPlayers();
            _playerValidatorService = playerValidatorService;
            _playerValidatorService.Players = _players;
            _canExecute = true;
        }

        public ObservableCollection<Player> GetAllPlayers()
        {
            return _footballRepository.GetAllPlayers();
        }

        public void UpdateButtonClicked()
        {
            if (_playerValidatorService.DataGridIsValid())
            {
                SaveDataGrid();
                RefreshPlayersInView();
            }
            else
                _playerValidatorService.SendErrorToUser();
        }

        private void RefreshPlayersInView()
        {
            _playerMatchViewModel.Players = _footballRepository.GetActivePlayers();
        }

        private void SaveDataGrid()
        {
            ObservableCollection<Player> playersToInsert = GetPlayersToInsert();

            _footballRepository.InsertPlayers(playersToInsert);
            _footballRepository.Save();
        }

        public ObservableCollection<Player> GetPlayersToInsert()
        {
            return new ObservableCollection<Player>(Players.Where(x => x.PlayerID == 0).ToList());
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

        #region ICommand Members
        private bool _canExecute;

        public ICommand UpdatePlayersCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new CommandHandler(() => UpdateButtonClicked(), _canExecute));
            }
        }
        private ICommand _updateCommand;
        #endregion
    }
}
