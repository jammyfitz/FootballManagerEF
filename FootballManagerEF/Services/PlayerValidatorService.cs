using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManagerEF.Services
{
    public class PlayerValidatorService : IPlayerValidatorService
    {
        private IDialogService _dialogService;

        public string ErrorMessage { get; set; }
        public List<Player> Players { get; set; }

        public PlayerValidatorService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public bool DataGridIsValid()
        {
            if (GridRowIncomplete())
                return false;

            if (PlayerAppearsMoreThanOnce())
                return false;

            if (PlayersHaveNonAlphaCharacters())
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
            string errorMessage = string.Empty;

            if (GridRowIncomplete())
                errorMessage = "Either the player or the active field is missing for one of the entries.";

            if (PlayerAppearsMoreThanOnce())
                errorMessage = "One of the players with that name already exists.";

            if (PlayersHaveNonAlphaCharacters())
                errorMessage = "One of the players has Non-Alphabetic characters.";

            return errorMessage;
        }

        private bool GridRowIncomplete()
        {
            if (RowsHavePlayerButNoActiveFlag())
                return true;

            if (RowsHaveActiveFlagButNoPlayer())
                return true;

            return false;
        }

        private bool RowsHavePlayerButNoActiveFlag()
        {
            if (Players.Where(x => x.PlayerName != null && x.Active == null).Count() > 0)
                return true;

            return false;
        }

        private bool RowsHaveActiveFlagButNoPlayer()
        {
            if (Players.Where(x => x.PlayerName == null && x.Active != null).Count() > 0)
                return true;

            return false;
        }

        private bool PlayersHaveNonAlphaCharacters()
        {
            var invalidPlayers = from x in Players
                                 where x.PlayerName.Any(c => WithoutLetterOrWhiteSpace(c))
                                 select x;

            if (invalidPlayers.Count() > 0)
                return true;

            return false;
        }

        private bool WithoutLetterOrWhiteSpace(char c)
        {
            if (IsLetter(c))
                return false;

            if (IsWhiteSpace(c))
                return false;

            return true;
        }

        private bool IsLetter(char c)
        {
            return char.IsLetter(c);
        }

        private bool IsWhiteSpace(char c)
        {
            return char.IsWhiteSpace(c);
        }

        private bool PlayerAppearsMoreThanOnce()
        {
            var duplicatePlayers = from x in Players
                                   where x.PlayerName != null
                                   group x by x.PlayerName into grouped
                                   where grouped.Count() > 1
                                   select grouped.Key;

            if (duplicatePlayers.Count() > 0)
                return true;

            return false;
        }
    }
}
