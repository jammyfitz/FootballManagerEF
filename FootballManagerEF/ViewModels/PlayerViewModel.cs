using FootballManagerEF.Handlers;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
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
        private List<Player> _players;
        private IPlayerValidatorService _playerValidatorService;

        public List<Player> Players
        {
            get { return _players; }

            set
            {
                _players = value;
                RaisePropertyChanged("Players");
            }
        }

        public PlayerViewModel(IFootballRepository footballRepository, IPlayerValidatorService playerValidatorService)
        {
            _footballRepository = footballRepository;
            _players = GetAllPlayers();
            _playerValidatorService = playerValidatorService;
            _playerValidatorService.Players = _players;
            _canExecute = true;
        }

        public List<Player> GetAllPlayers()
        {
            return _footballRepository.GetAllPlayers();
        }

        public void UpdateButtonClicked()
        {
            if (_playerValidatorService.DataGridIsValid())
            {
                SaveDataGrid();
                //_playerValidatorService.Players = _footballRepository.GetActivePlayers();
            }
            else
                _playerValidatorService.SendErrorToUser();
        }

        private void SaveDataGrid()
        {
            List<Player> playersToInsert = GetPlayersToInsert();

            _footballRepository.InsertPlayers(playersToInsert);
            _footballRepository.Save();
        }

        public List<Player> GetPlayersToInsert()
        {
            return Players.Where(x => x.PlayerID == 0).ToList();
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
