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
    public class PlayerRepository : IPlayerRepository, IDisposable
    {
        private FootballEntities context;

        public PlayerRepository(FootballEntities context)
        {
            this.context = context;
        }

        public List<Player> GetPlayers()
        {
            return GetPlayersByNameAsc();
        }

        public List<Player> GetPlayersByNameAsc()
        {
            return context.Players.OrderBy(x => x.PlayerName).ToList();
        }

        public Player GetPlayerByID(int playerId)
        {
            return context.Players.Find(playerId);
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
