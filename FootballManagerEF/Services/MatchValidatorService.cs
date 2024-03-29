﻿using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace FootballManagerEF.Services
{
    public class MatchValidatorService : IMatchValidatorService
    {
        private IPlayerMatchViewModel _playerMatchViewModel;
        private IDialogService _dialogService;

        public string ErrorMessage { get; set; }

        public ObservableCollection<PlayerMatch> PlayerMatches
        {
            get { return _playerMatchViewModel.PlayerMatches; }
            set { _playerMatchViewModel.PlayerMatches = value; }
        }

        public MatchValidatorService(IPlayerMatchViewModel playerMatchViewModel, IDialogService dialogService)
        {
            _playerMatchViewModel = playerMatchViewModel;
            _dialogService = dialogService;
        }

        public bool DataGridIsValid()
        {
            if (GridRowIncomplete())
                return false;

            if (PlayerAppearsMoreThanOnce())
                return false;

            if (MoreThanMaxPlayersInATeam())
                return false;

            return true;
        }

        public bool DataGridIsComplete()
        {
            if (!DataGridIsValid())
                return false;

            if (DataGridIsIncomplete())
                return false;

            return true;
        }

        public bool SendErrorToUser()
        {
            ErrorMessage = GetErrorMessageOnUpdate();
            return _dialogService.ShowMessageBox(ErrorMessage);
        }

        public string GetErrorMessageOnUpdate()
        {
            if (GridRowIncomplete())
                return "Either the team or the player is missing for one of the entries.";

            if (PlayerAppearsMoreThanOnce())
                return "One of the selected players appears more than once for this match.";

            if (MoreThanMaxPlayersInATeam())
                return "One of the teams has more than 5 players.";

            if (DataGridIsIncomplete())
                return "Please ensure that the maximum number of players and teams are entered.";

            return string.Empty;
        }

        private bool MoreThanMaxPlayersInATeam()
        {
            var tooManyPlayers = from x in PlayerMatches
                                 where x.PlayerID != null & x.TeamID != null
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

            return false;
        }

        private bool DataGridIsIncomplete()
        {
            if (GridHasMissingPlayers())
                return true;

            return false;
        }

        private bool RowsHaveTeamButNoPlayer()
        {
            if (PlayerMatches.Where(x => x.PlayerID == null && x.TeamID != null).Count() > 0)
                return true;

            return false;
        }

        private bool GridHasMissingPlayers()
        {
            if (PlayerMatches.Where(x => x.PlayerID == null).Count() > 0)
                return true;

            return false;
        }

        private bool PlayerAppearsMoreThanOnce()
        {
            var duplicatePlayers = from x in PlayerMatches where x.PlayerID != null
                                   group x by x.PlayerID into grouped
                                   where grouped.Count() > 1
                                   select grouped.Key;

            if (duplicatePlayers.Count() > 0)
                return true;

            return false;
        }
    }
}
