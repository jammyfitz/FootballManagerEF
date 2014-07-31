using FootballManagerEF.EFModel;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Repositories
{
    public class FakeMatchRepository : IMatchRepository
    {
        public List<Match> GetMatches()
        {
           return new List<Match> 
           { 
              new Match
              { 
                  MatchID = 1,
                  MatchDate = DateTime.Today.AddDays(-14)
              },
              new Match
              { 
                  MatchID = 2,
                  MatchDate = DateTime.Today.AddDays(-7)
              }
           };
        }

        public Match GetMatchByID(int id)
        {
            return new Match
              {
                  MatchID = 1,
                  MatchDate = DateTime.Today.AddDays(-14)
              };
        }

        public void InsertMatch(Match match)
        {
            throw new NotImplementedException();
        }

        public void DeleteMatch(int matchID)
        {
            throw new NotImplementedException();
        }

        public void UpdateMatch(Match match)
        {
            throw new NotImplementedException();
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
