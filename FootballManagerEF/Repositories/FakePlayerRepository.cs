using FootballManagerEF.EFModel;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Repositories
{
    public class FakePlayerRepository : IPlayerRepository
    {
        public List<Player> GetPlayers()
        {
            return new List<Player> 
           { 
              new Player
              { 
                  PlayerID = 1,
                  PlayerName = "Jamie"
              },
              new Player
              { 
                  PlayerID = 2,
                  PlayerName = "Mike"
              }
           };
        }

        public Player GetPlayerByID(int playerId)
        {
            return new Player
            {
                PlayerID = 1,
                PlayerName = "Jamie"
            };
        }

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
    }
}
