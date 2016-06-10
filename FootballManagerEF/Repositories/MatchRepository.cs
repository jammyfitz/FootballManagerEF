using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FootballManagerEF.Repositories
{
    public class MatchRepository : IMatchRepository, IDisposable
    {
        private FootballEntities context;

        public MatchRepository(FootballEntities context)
        {
            this.context = context;
        }

        public ObservableCollection<Match> GetMatches()
        {
            return GetMatchesByDateAscNotOlderThanThreeWeeks();
        }

        public ObservableCollection<Match> GetMatchesByDateAsc()
        {
            return new ObservableCollection<Match>(context.Matches.OrderBy(x => x.MatchDate).ToList());
        }

        public ObservableCollection<Match> GetMatchesByDateAscNotOlderThanThreeWeeks()
        {
            DateTime threeWeeksAgo = Utils.ThreeWeeksAgo();

            var result = from matches in context.Matches
                         where (matches.MatchDate > threeWeeksAgo)
                         orderby matches.MatchDate ascending
                         select matches;

            return new ObservableCollection<Match>(result.ToList());
        }

        public Match GetMatchByID(int id)
        {
            return context.Matches.Find(id);
        }

        public Match InsertMatch(Match match)
        {
            context.Matches.Add(match);
            Save();
            return match;
        }

        public bool DeleteMatch(Match match)
        {
            context.Matches.Remove(match);
            Save();
            return true;
        }

        public void UpdateMatch(Match match)
        {
            context.Entry(match).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #region IDisposable Members
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
