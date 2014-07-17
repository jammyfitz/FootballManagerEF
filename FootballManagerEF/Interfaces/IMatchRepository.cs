using FootballManagerEF.EFModel;
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
        void InsertMatch(Match match);
        void DeleteMatch(int matchID);
        void UpdateMatch(Match match);
        void Save();
    }
}
