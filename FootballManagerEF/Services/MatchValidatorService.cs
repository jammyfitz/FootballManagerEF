using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManagerEF.Services
{
    public class MatchValidatorService : IMatchValidatorService
    {
        private List<PlayerMatch> _playerMatches;
        private IDialogService _dialogService;

        public string ErrorMessage { get; set; }

        public MatchValidatorService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public bool DataGridIsValid(List<PlayerMatch> playerMatches)
        {
            _playerMatches = playerMatches;

            if (GridRowIncomplete())
                return false;

            if (PlayerAppearsMoreThanOnce())
                return false;

            if (MoreThanMaxPlayersInATeam())
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
                return "One of the teams has 6 players.";

            return string.Empty;
        }

        private bool MoreThanMaxPlayersInATeam()
        {
            var tooManyPlayers = from x in _playerMatches
                                 where x.PlayerID != null
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
            if (_playerMatches.Where(x => x.PlayerID != null && x.TeamID == null).Count() > 0)
                return true;

            return false;
        }

        private bool RowsHaveTeamButNoPlayer()
        {
            if (_playerMatches.Where(x => x.PlayerID == null && x.TeamID != null).Count() > 0)
                return true;

            return false;
        }

        private bool PlayerAppearsMoreThanOnce()
        {
            var duplicatePlayers = from x in _playerMatches
                                   group x by x.PlayerID into grouped
                                   where grouped.Count() > 1
                                   select grouped.Key;

            if (duplicatePlayers.Count() > 1)
                return true;

            return false;
        }
    }
}
