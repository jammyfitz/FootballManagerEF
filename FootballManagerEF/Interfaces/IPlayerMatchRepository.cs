using FootballManagerEF.Models;
using System;
using System.Collections.ObjectModel;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerMatchRepository : IDisposable
    {
        ObservableCollection<PlayerMatch> GetPlayerMatches(int matchId);
        ObservableCollection<PlayerMatch> GetTenPlayerMatches(int matchId);
        ObservableCollection<PlayerMatch> GetFiveFilledAndFiveEmptyPlayerMatches();
        bool InsertPlayerMatches(ObservableCollection<PlayerMatch> playerMatches, int matchId);
        bool DeletePlayerMatches(Match match);
        void Save();
    }
}
