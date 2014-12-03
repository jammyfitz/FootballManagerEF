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
            // TODO
            return true;
        }

        public bool SendErrorToUser()
        {
            ErrorMessage = GetErrorMessageOnUpdate();
            return _dialogService.ShowMessageBox(ErrorMessage);
        }

        public string GetErrorMessageOnUpdate()
        {
            // TODO
            return "";
        }
    }
}
