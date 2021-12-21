using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FootballManagerEF.Extensions;

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

        public List<PlayerCalculation> GetPlayerCalculations()
        {
            return GetPlayerCalculationData();
        }

        public List<PlayerStat> GetPlayerStatsByMatchWinsDesc()
        {
            var result = from playerStats in context.PlayerStats
                         orderby playerStats.MatchWins descending
                         select playerStats;

            return result.ToList();
        }

        public List<PlayerCalculation> GetPlayerCalculationData()
        {
            var mostRecentWinsToCount = Convert.ToInt32(ConfigurationManager.AppSettings["MostRecentWinsToCount"]);

            var playerMatchesByPlayer = context.PlayerMatches
                .Include(x => x.Player)
                .Include(x => x.Match)
                .GroupBy(x => x.PlayerID)
                .ToList();

            var playerCalculations = playerMatchesByPlayer.Select(x => new PlayerCalculation
            {
                PlayerID = x.First().Player.PlayerID,
                PlayerName = x.First().Player.PlayerName,
                MatchesPlayed = x.Count(),
                TotalMatchWins = x.Where(y => y.WonMatch()).Count(),
                RecentMatchWins = x.OrderByDescending(y => y.Match.MatchDate)
                    .Take(mostRecentWinsToCount)
                    .Count(y => y.WonMatch()),
                WinRatio = x.ToList().GetWinRatio(),
                RecentMatchCount = x.OrderByDescending(y => y.Match.MatchDate)
                    .Where(y => y.Match.MatchWinner.HasValue)
                    .Take(mostRecentWinsToCount)
                    .Count(),
            })
            .OrderByDescending(x => x.TotalMatchWins)
            .ToList();

            return playerCalculations;
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
