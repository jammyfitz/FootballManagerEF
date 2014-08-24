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

        public List<PlayerMatch> PlayerMatches { get; set; }

        public FakeMatchValidatorService(bool validationResult)
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
