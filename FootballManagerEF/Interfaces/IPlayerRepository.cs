using FootballManagerEF.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerRepository : IDisposable
    {
        List<Player> GetPlayers();
        Player GetPlayerByID(int playerId);
        void Save();
    }
}
