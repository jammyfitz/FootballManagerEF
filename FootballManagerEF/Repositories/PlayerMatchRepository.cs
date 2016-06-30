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
    public class PlayerMatchRepository : IPlayerMatchRepository, IDisposable
    {
        private FootballEntities context;

        public PlayerMatchRepository(FootballEntities context)
        {
            this.context = context;
        }

        public ObservableCollection<PlayerMatch> GetPlayerMatches(int matchId)
        {
            var result = from playerMatches in context.PlayerMatches
                         where (playerMatches.MatchID == matchId)
                         orderby playerMatches.TeamID ascending
                         select playerMatches;

            return new ObservableCollection<PlayerMatch>(result.ToList());
        }

        public ObservableCollection<PlayerMatch> GetTenPlayerMatches(int matchId)
        {
            var result = from playerMatches in context.PlayerMatches
                         where (playerMatches.MatchID == matchId)
                         orderby playerMatches.TeamID ascending
                         select playerMatches;

            int noOfBlankPlayersToAdd = 10 - result.Count();

            return new ObservableCollection<PlayerMatch>(AddBlankPlayerMatches(new ObservableCollection<PlayerMatch>(result.ToList()), noOfBlankPlayersToAdd));
        }

        public bool InsertPlayerMatches(ObservableCollection<PlayerMatch> playerMatches, int matchId)
        {
            foreach (PlayerMatch playerMatch in playerMatches.Where(x => x.PlayerMatchID == 0))
            {
                playerMatch.MatchID = matchId;
                context.PlayerMatches.Add(playerMatch);
            }

           return true;
        }

        public bool DeletePlayerMatches(Match match)
        {
            var playerMatchesToDelete = context.PlayerMatches.Where(x => x.MatchID == match.MatchID);
            context.PlayerMatches.RemoveRange(playerMatchesToDelete);
            Save();
            return true;
        }

        public ObservableCollection<PlayerMatch> GetFiveFilledAndFiveEmptyPlayerMatches()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #region Private Methods
        private ObservableCollection<PlayerMatch> AddBlankPlayerMatches(ObservableCollection<PlayerMatch> playerMatchList, int noOfBlankPlayersToAdd)
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
