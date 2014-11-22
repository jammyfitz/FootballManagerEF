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
            return GetPlayerStatsByMatchWinsDesc();
        }

        public List<PlayerStat> GetPlayerStatsByMatchWinsDesc()
        {
            var result = from playerStats in context.PlayerStats
                         orderby playerStats.MatchWins descending
                         select playerStats;

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
