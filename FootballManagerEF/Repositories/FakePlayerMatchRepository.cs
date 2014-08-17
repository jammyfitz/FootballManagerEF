using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Repositories
{
    public class FakePlayerMatchRepository : IPlayerMatchRepository
    {
        public List<PlayerMatch> GetPlayerMatches(int matchId)
        {
            return AddFourPlayerMatches();
        }

        public List<PlayerMatch> GetTenPlayerMatches(int matchId)
        {
            List<PlayerMatch> tenPlayerMatches = AddFourPlayerMatches();
            tenPlayerMatches.Add(new PlayerMatch());
            tenPlayerMatches.Add(new PlayerMatch());
            tenPlayerMatches.Add(new PlayerMatch());
            tenPlayerMatches.Add(new PlayerMatch());
            tenPlayerMatches.Add(new PlayerMatch());
            tenPlayerMatches.Add(new PlayerMatch());
            return tenPlayerMatches;
        }

        private static List<PlayerMatch> AddFourPlayerMatches()
        {
            return new List<PlayerMatch> 
           { 
              new PlayerMatch
              { 
                  PlayerMatchID = 1,
                  PlayerID = 1,
                  MatchID = 1,
                  TeamID = 1
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 2,
                  PlayerID = 2,
                  MatchID = 1,
                  TeamID = 2
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 3,
                  PlayerID = 3,
                  MatchID = 1,
                  TeamID = 1
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 4,
                  PlayerID = 4,
                  MatchID = 1,
                  TeamID = 2
              }
           };
        }

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
    }
}
