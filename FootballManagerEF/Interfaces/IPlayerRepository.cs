using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerRepository : IDisposable
    {
        ObservableCollection<Player> GetAllPlayers();
        ObservableCollection<Player> GetActivePlayers();
        Player GetPlayerByID(int playerId);
        bool InsertPlayers(ObservableCollection<Player> players);
        List<string> GetEmailAddresses(List<int?> playerIds);
        void Save();
    }
}
