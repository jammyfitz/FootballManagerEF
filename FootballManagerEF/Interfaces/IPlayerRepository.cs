using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerRepository : IDisposable
    {
        List<Player> GetAllPlayers();
        List<Player> GetActivePlayers();
        Player GetPlayerByID(int playerId);
        bool InsertPlayers(List<Player> players);
        void Save();
    }
}
