using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FootballManagerEF.Interfaces
{
    public interface IMatchValidatorService : IValidatorService
    {
        ObservableCollection<PlayerMatch> PlayerMatches { get; set; }
    }
}
