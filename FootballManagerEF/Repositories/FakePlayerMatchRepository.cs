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
            return AddFivePlayerMatches();
        }

        public List<PlayerMatch> GetTenPlayerMatches(int matchId)
        {
            return GetFiveFilledAndFiveEmptyPlayerMatches();
        }

        public bool InsertPlayerMatches(List<PlayerMatch> playerMatches, int matchId)
        {
            return true;
        }

        public List<PlayerMatch> GetFiveFilledAndFiveEmptyPlayerMatches()
        {
            List<PlayerMatch> playerMatches = AddFivePlayerMatches();
            AddFiveEmptyPlayerMatches(playerMatches);
            return playerMatches;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        private static List<PlayerMatch> AddFivePlayerMatches()
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
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 5,
                  PlayerID = 5,
                  MatchID = 1,
                  TeamID = 2
              }
           };
        }

        private static List<PlayerMatch> AddFiveEmptyPlayerMatches(List<PlayerMatch> playerMatches)
        {
            playerMatches.Add(new PlayerMatch());
            playerMatches.Add(new PlayerMatch());
            playerMatches.Add(new PlayerMatch());
            playerMatches.Add(new PlayerMatch());
            playerMatches.Add(new PlayerMatch());
            return playerMatches;
        }

        #region IDisposable Members
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
