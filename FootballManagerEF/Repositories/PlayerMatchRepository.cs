using FootballManagerEF.Models;
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
                         orderby playerMatches.TeamID ascending
                         select playerMatches;

            return result.ToList();
        }

        public List<PlayerMatch> GetTenPlayerMatches(int matchId)
        {
            var result = from playerMatches in context.PlayerMatches
                         where (playerMatches.MatchID == matchId)
                         orderby playerMatches.TeamID ascending
                         select playerMatches;

            int noOfBlankPlayersToAdd = 10 - result.Count();

            return AddBlankPlayerMatches(result.ToList(), noOfBlankPlayersToAdd);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #region Private Methods
        private List<PlayerMatch> AddBlankPlayerMatches(List<PlayerMatch> playerMatchList, int noOfBlankPlayersToAdd)
        {
            while (noOfBlankPlayersToAdd != 0)
            {
                playerMatchList.Add(new PlayerMatch());
                noOfBlankPlayersToAdd--;
            }

            return playerMatchList;
        }
        #endregion

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
