using FootballManagerEF.Handlers;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FootballManagerEF.ViewModels
{
    public class ButtonViewModel
    {
        private IFootballRepository _footballRepository;
        private PlayerMatchViewModel _playerMatchViewModel;

        public Match SelectedMatch { get; set; }

        public List<PlayerMatch> PlayerMatches
        {
            get { return _playerMatchViewModel.PlayerMatches; }
            set { _playerMatchViewModel.PlayerMatches = value; }
        }

        public ButtonViewModel(IFootballRepository footballRepository, PlayerMatchViewModel playerMatchViewModel)
        {
            _footballRepository = footballRepository;
            _playerMatchViewModel = playerMatchViewModel;
            _canExecute = true;
        }

        public void UpdateButtonClicked()
        {
            if (DataGridIsValid())
                SaveDataGrid();
            else
                SendErrorToUser();
        }

        private void SaveDataGrid()
        {
            List<PlayerMatch> playerMatchesToInsert = GetPlayerMatchesToInsert();

            _footballRepository.InsertPlayerMatches(playerMatchesToInsert, SelectedMatch.MatchID);
            _footballRepository.Save();
        }

        private void SendErrorToUser()
        {
            string errorMessage = GetErrorMessageOnUpdate();
            MessageBox.Show(errorMessage);
        }

        public string GetErrorMessageOnUpdate()
        {
            if (GridRowIncomplete())
                return "Either the team or the player is missing for one of the entries.";

            if (PlayerAppearsMoreThanOnce())
                return "One of the selected players appears more than once for this match.";

            if (MoreThanMaxPlayersInATeam())
                return "One of the teams has 6 players.";

            return string.Empty;
        }

        public List<PlayerMatch> GetPlayerMatchesToInsert()
        {
            return PlayerMatches.Where(x => (x.PlayerID & x.TeamID) != null).ToList();
        }

        private bool DataGridIsValid()
        {
            if (GridRowIncomplete())
                return false;

            if (PlayerAppearsMoreThanOnce())
                return false;

            if (MoreThanMaxPlayersInATeam())
                return false;

            return true;
        }

        private bool MoreThanMaxPlayersInATeam()
        {
            var tooManyPlayers = from x in PlayerMatches
                                   group x by x.TeamID into grouped
                                   where grouped.Count() > 5
                                   select grouped.Key;

            if (tooManyPlayers.Count() > 0)
                return true;

            return false;
        }

        private bool GridRowIncomplete()
        {
            if (RowsHaveTeamButNoPlayer())
                return true;

            if (RowsHavePlayerButNoTeam())
                return true;

            return false;
        }

        private bool RowsHavePlayerButNoTeam()
        {
            if (PlayerMatches.Where(x => x.PlayerID != null && x.TeamID == null).Count() > 0)
                return true;

            return false;
        }

        private bool RowsHaveTeamButNoPlayer()
        {
            if (PlayerMatches.Where(x => x.PlayerID == null && x.TeamID != null).Count() > 0)
                return true;

            return false;
        }

        private bool PlayerAppearsMoreThanOnce()
        {
            var duplicatePlayers = from x in PlayerMatches
                                 group x by x.PlayerID into grouped
                                 where grouped.Count() > 1
                                 select grouped.Key;

            if (duplicatePlayers.Count() > 1)
                return true;

            return false;
        }

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
        #endregion
    }
}
