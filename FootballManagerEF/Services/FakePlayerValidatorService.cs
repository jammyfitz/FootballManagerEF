using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManagerEF.ViewModels
{
    public class FakePlayerValidatorService : IPlayerValidatorService
    {
        private bool _validationResult;

        public List<Player> Players { get; set; }

        public FakePlayerValidatorService(bool validationResult)
        {
            _validationResult = validationResult;
        }

        public bool DataGridIsValid()
        {
            return _validationResult;
        }

        public bool SendErrorToUser()
        {
            return _validationResult;
        }
    }
}
