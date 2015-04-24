using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FootballManagerEF.Services
{
    public class PlayerValidatorService : IPlayerValidatorService
    {
        private IDialogService _dialogService;

        public string ErrorMessage { get; set; }
        public ObservableCollection<Player> Players { get; set; }

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

            if (PlayersHaveInvalidEmail())
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
                return "Either the player or the active field is missing for one of the entries.";

            if (PlayerAppearsMoreThanOnce())
                return "One of the players with that name already exists.";

            if (PlayersHaveNonAlphaCharacters())
                return "One of the players has Non-Alphabetic characters.";

            if (PlayersHaveInvalidEmail())
                return "One of the players has an invalid e-mail address.";

            return string.Empty;
        }

        private bool GridRowIncomplete()
        {
            if (RowsHavePlayerNameButNoActiveFlag())
                return true;

            if (RowsHaveActiveFlagButNoPlayerName())
                return true;

            return false;
        }

        private bool RowsHavePlayerNameButNoActiveFlag()
        {
            if (Players.Where(x => x.PlayerName != null && x.Active == null).Count() > 0)
                return true;

            return false;
        }

        private bool RowsHaveActiveFlagButNoPlayerName()
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

        private bool PlayersHaveInvalidEmail()
        {
            Regex isEmailAddress = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            var results = from player in Players.Where(x => x.EmailAddress != null)
                          where !isEmailAddress.IsMatch(player.EmailAddress)
                          select player;

            return results.Any();
        }
    }
}
