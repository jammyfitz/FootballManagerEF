using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Repositories
{
    public class FakePlayerStatsRepository : IPlayerStatsRepository, IDisposable
    {
        public List<PlayerStat> GetPlayerStats()
        {
           return new List<PlayerStat> 
           { 
              CreatePlayerStat(1, "Jamie", 10, 10),
              CreatePlayerStat(2, "Mike", 8, 10),
              CreatePlayerStat(3, "Caff", 7, 10),
              CreatePlayerStat(4, "Ant", 7, 10),
              CreatePlayerStat(5, "Croucho", 5, 15),
              CreatePlayerStat(6, "Greg", 4, 4),
              CreatePlayerStat(7, "Hugh", 4, 4),
              CreatePlayerStat(8, "Skip", 4, 12),
              CreatePlayerStat(9, "Imran", 2, 8),
              CreatePlayerStat(10, "John", 1, 8),
           };
        }

        public List<PlayerCalculation> GetPlayerCalculations()
        {
            return new List<PlayerCalculation>
           {
              CreatePlayerCalculation(1, "Jamie", 10, 10, 2),
              CreatePlayerCalculation(2, "Mike", 8, 10, 3),
              CreatePlayerCalculation(3, "Caff", 7, 10, 1),
              CreatePlayerCalculation(4, "Ant", 7, 10, 4),
              CreatePlayerCalculation(5, "Croucho", 5, 15, 5),
              CreatePlayerCalculation(6, "Greg", 4, 4, 5),
              CreatePlayerCalculation(7, "Hugh", 4, 4, 2),
              CreatePlayerCalculation(8, "Skip", 4, 12, 1),
              CreatePlayerCalculation(9, "Imran", 2, 8, 3),
              CreatePlayerCalculation(10, "John", 1, 8, 4),
           };
        }

        #region Private
        private PlayerStat CreatePlayerStat(int playerId, string playerName, int? matchWins, int? matchesPlayed)
        {
            return new PlayerStat
            {
                PlayerID = playerId,
                PlayerName = playerName,
                MatchWins = matchWins,
                MatchesPlayed = matchesPlayed
            };
        }

        private PlayerCalculation CreatePlayerCalculation(int playerId, string playerName, int? totalMatchWins, int? matchesPlayed, int? recentMatchWins)
        {
            return new PlayerCalculation
            {
                PlayerName = playerName,
                TotalMatchWins = totalMatchWins,
                MatchesPlayed = matchesPlayed,
                RecentMatchWins = recentMatchWins
            };
        }
        #endregion

        #region IDisposable Members
        public void Save()
        {
            throw new NotImplementedException();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
