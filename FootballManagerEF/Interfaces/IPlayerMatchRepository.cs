using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerMatchRepository : IDisposable
    {
        ObservableCollection<PlayerMatch> GetPlayerMatches(int matchId);
        ObservableCollection<PlayerMatch> GetTenPlayerMatches(int matchId);
        ObservableCollection<PlayerMatch> GetFiveFilledAndFiveEmptyPlayerMatches();
        bool InsertPlayerMatches(ObservableCollection<PlayerMatch> playerMatches, int matchId);
        void Save();
    }
}
