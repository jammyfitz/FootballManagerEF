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
    public class PlayerStatsRepository : IPlayerStatsRepository, IDisposable
    {
        private FootballEntities context;

        public PlayerStatsRepository(FootballEntities context)
        {
            this.context = context;
        }

        public List<PlayerStat> GetPlayerStats()
        {
            var result = from playerstats in context.PlayerStats
                         select playerstats;

            return result.ToList();
        }

        public List<Match> GetMatchesByDateAsc()
        {
            return context.Matches.OrderBy(x => x.MatchDate).ToList();
        }

        public List<Match> GetMatchesByDateAscNotOlderThanTwoWeeks()
        {
            DateTime twoWeeksAgo = Utils.TwoWeeksAgo();

            var result = from matches in context.Matches
                         where (matches.MatchDate > twoWeeksAgo)
                         orderby matches.MatchDate ascending
                         select matches;

            return result.ToList();
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
