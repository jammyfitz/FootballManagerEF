﻿using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Repositories
{
    public class FakePlayerStatsRepository : IPlayerStatsRepository, IDisposable
    {
        public List<PlayerStat> GetPlayerStats()
        {
           return new List<PlayerStat> 
           { 
              CreatePlayer(1, "Jamie", 10, 10),
              CreatePlayer(2, "Mike", 8, 10),
              CreatePlayer(3, "Caff", 7, 10),
              CreatePlayer(4, "Ant", 7, 10),
              CreatePlayer(5, "Croucho", 5, 15),
              CreatePlayer(6, "Greg", 4, 4),
              CreatePlayer(7, "Hugh", 4, 4),
              CreatePlayer(8, "Skip", 4, 12),
              CreatePlayer(9, "Imran", 2, 8),
              CreatePlayer(10, "John", 1, 8),
           };
        }

        #region Private
        private PlayerStat CreatePlayer(int playerId, string playerName, int? matchWins, int? matchesPlayed)
        {
            return new PlayerStat
            {
                PlayerID = playerId,
                PlayerName = playerName,
                MatchWins = matchWins,
                MatchesPlayed = matchesPlayed
            };
        }
        #endregion

        #region IDisposable Members
        public void Save()
        {
            throw new NotImplementedException();
        }

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
