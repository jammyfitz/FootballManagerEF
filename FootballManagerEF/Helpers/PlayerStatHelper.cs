using FootballManagerEF.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FootballManagerEF.Helpers
{
    public static class PlayerStatHelper
    {
        public static List<PlayerStat> GetPlayerStatsForAllPlayers(List<PlayerStat> completeStats, ObservableCollection<Player> players)
        {
            if (!completeStats.Any())
                return new List<PlayerStat>();

            var missingPlayers = from player in players
                                 join playerStat in completeStats.DefaultIfEmpty() on player.PlayerID equals playerStat.PlayerID into completeStat
                                 from stat in completeStat.DefaultIfEmpty()
                                 select new PlayerStat
                                 {
                                     MatchesPlayed = (stat == null ? 0 : stat.MatchesPlayed),
                                     MatchWins = (stat == null ? 0 : stat.MatchWins),
                                     PlayerID = player.PlayerID,
                                     PlayerName = player.PlayerName,
                                 } into results
                                 select results;

            return missingPlayers.ToList();
        }
    }
}
