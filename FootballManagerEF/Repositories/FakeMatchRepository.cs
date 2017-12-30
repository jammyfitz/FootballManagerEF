using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FootballManagerEF.Repositories
{
    public class FakeMatchRepository : IMatchRepository
    {
        public ObservableCollection<Match> GetMatches()
        {
            return new ObservableCollection<Match> 
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

        public ObservableCollection<Match> GetTwoMatches()
        {
            return new ObservableCollection<Match>(
               new List<Match> 
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
               }
            );
        }

        public ObservableCollection<Match> GetNoMatches()
        {
            return new ObservableCollection<Match>();
        }

        public Match GetMatchByID(int id)
        {
            return new Match
            {
                MatchID = 1,
                MatchDate = DateTime.Today.AddDays(-14)
            };
        }

        public Match InsertMatch(Match match)
        {
            return new Match
            {
                MatchID = 1,
                MatchDate = DateTime.Today
            };
        }

        public bool DeleteMatch(Match match)
        {
            return true;
        }

        public void Save()
        {
            throw new NotImplementedException();
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
