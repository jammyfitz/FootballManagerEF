using FootballManagerEF.EFModel;
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
        void Save();
    }
}
