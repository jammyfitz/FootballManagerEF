using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IMatchRepository : IDisposable
    {
        ObservableCollection<Match> GetMatches();
        Match GetMatchByID(int matchId);
        Match InsertMatch(Match match);
        bool DeleteMatch(Match match);
        void Save();
    }
}
