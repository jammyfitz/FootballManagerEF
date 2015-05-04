using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IMatchRepository : IDisposable
    {
        List<Match> GetMatches();
        Match GetMatchByID(int matchId);
        Match InsertMatch(Match match);
        void Save();
    }
}
