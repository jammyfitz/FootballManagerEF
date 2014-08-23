using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManagerEF.ViewModels
{
    public class FakeMatchValidatorService : IMatchValidatorService
    {
        private bool _validationResult;

        public FakeMatchValidatorService(bool validationResult)
        {
            _validationResult = validationResult;
        }

        public bool DataGridIsValid(List <PlayerMatch> playerMatches)
        {
            return _validationResult;
        }

        public bool SendErrorToUser()
        {
            return _validationResult;
        }
    }
}
