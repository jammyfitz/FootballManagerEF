using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballManagerEF.Interfaces
{
    public interface IMatchValidatorService : IValidatorService
    {
        List<PlayerMatch> PlayerMatches { get; set; }
    }
}
