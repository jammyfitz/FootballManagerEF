using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerMatchRepository : IDisposable
    {
        List<PlayerMatch> GetPlayerMatches(int matchId);
        List<PlayerMatch> GetTenPlayerMatches(int matchId);
        List<PlayerMatch> GetFiveFilledAndFiveEmptyPlayerMatches();
        bool InsertPlayerMatches(List<PlayerMatch> playerMatches, int matchId);
        void Save();
    }
}
