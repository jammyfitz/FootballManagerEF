using FootballManagerEF.Models;
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
        public List<Player> GetAllPlayers()
        {
            return new List<Player> 
           { 
              new Player
              { 
                  PlayerID = 1,
                  PlayerName = "Jamie",
                  Active = true
              },
              new Player
              { 
                  PlayerID = 2,
                  PlayerName = "Mike",
                  Active = true
              },
              new Player
              { 
                  PlayerID = 3,
                  PlayerName = "Caff",
                  Active = true
              }
           };
        }

        public List<Player> GetActivePlayers()
        {
            return new List<Player> 
           { 
              new Player
              { 
                  PlayerID = 1,
                  PlayerName = "Jamie",
                  Active = true
              },
              new Player
              { 
                  PlayerID = 2,
                  PlayerName = "Mike",
                  Active = true
              },
              new Player
              { 
                  PlayerID = 3,
                  PlayerName = "Caff",
                  Active = true
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

        public List<Player> GetTenValidPlayers()
        {
            return new List<Player> 
           { 
              new Player
              { 
                  PlayerID = 1,
                  PlayerName = "Jamie",
                  Active = true
              },
              new Player
              { 
                  PlayerID = 2,
                  PlayerName = "Mike",
                  Active = true
              },
              new Player
              { 
                  PlayerID = 3,
                  PlayerName = "Caff",
                  Active = true
              },
              new Player
              { 
                  PlayerID = 4,
                  PlayerName = "Steve",
                  Active = true
              },
              new Player
              { 
                  PlayerID = 5,
                  PlayerName = "Richard",
                  Active = true
              },
              new Player
              { 
                  PlayerID = 6,
                  PlayerName = "Bruce",
                  Active = true
              },
              new Player
              { 
                  PlayerID = 7,
                  PlayerName = "Simon",
                  Active = true
              },
              new Player
              { 
                  PlayerID = 8,
                  PlayerName = "Ant",
                  Active = false
              },
              new Player
              { 
                  PlayerID = 9,
                  PlayerName = "Dec",
                  Active = false
              },
              new Player
              { 
                  PlayerID = 10,
                  PlayerName = "Walter",
                  Active = true
              }
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
