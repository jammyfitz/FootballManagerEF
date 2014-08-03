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
    public class PlayerMatchRepository : IPlayerMatchRepository, IDisposable
    {
        private FootballEntities context;

        public PlayerMatchRepository(FootballEntities context)
        {
            this.context = context;
        }

        public List<PlayerMatch> GetPlayerMatches(int matchId)
        {
            var result = from playerMatches in context.PlayerMatches
                         where (playerMatches.MatchID == matchId)
                         orderby playerMatches.Team ascending
                         select playerMatches;

            return result.ToList();
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
