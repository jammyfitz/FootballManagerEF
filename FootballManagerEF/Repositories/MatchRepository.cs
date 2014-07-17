using FootballManagerEF.EFModel;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Repositories
{
    public class MatchRepository : IMatchRepository, IDisposable
    {
        private FootballEntities context;

        public MatchRepository(FootballEntities context)
        {
            this.context = context;
        }

        public List<Match> GetMatches()
        {
            return context.Matches.ToList();
        }

        public Match GetMatchByID(int id)
        {
            return context.Matches.Find(id);
        }

        public void InsertMatch(Match match)
        {
            context.Matches.Add(match);
        }

        public void DeleteMatch(int matchID)
        {
            Match match = context.Matches.Find(matchID);
            context.Matches.Remove(match);
        }

        public void UpdateMatch(Match match)
        {
            context.Entry(match).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

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
    }
}
