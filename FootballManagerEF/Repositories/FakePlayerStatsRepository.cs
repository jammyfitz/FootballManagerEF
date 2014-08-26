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
              new PlayerStat
              { 
                  PlayerID = 1,
                  PlayerName = "Jamie",
                  MatchWins = 2
              },
              new PlayerStat
              { 
                  PlayerID = 2,
                  PlayerName = "Mike",
                  MatchWins = 1
              },
              new PlayerStat
              { 
                  PlayerID = 3,
                  PlayerName = "Caff",
                  MatchWins = 1
              }
           };
        }

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
