using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FootballManagerEF.Repositories
{
    public class FakePlayerRepository : IPlayerRepository
    {
        public ObservableCollection<Player> GetAllPlayers()
        {
            return new ObservableCollection<Player> 
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

        public ObservableCollection<Player> GetActivePlayers()
        {
            return new ObservableCollection<Player> 
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
                  Active = true
              },
              new Player
              {
                  PlayerID = 9,
                  PlayerName = "Dec",
                  Active = true
              },
              new Player
              {
                  PlayerID = 10,
                  PlayerName = "Walter",
                  Active = true,
                  Height = 180
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

        public ObservableCollection<Player> GetTenValidPlayers()
        {
            return new ObservableCollection<Player> 
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

        public ObservableCollection<Player> GetPlayersWithPlayerNameAndNoActiveFlag()
        {
            return new ObservableCollection<Player>(
                new List<Player> 
                { 
                    new Player
                    { 
                        PlayerID = 1,
                        PlayerName = "Jamie",
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
                }
            );
        }

        public ObservableCollection<Player> GetPlayersWithActiveFlagAndNoPlayerName()
        {
           return new ObservableCollection<Player>(
                new List<Player> 
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
                        Active = true
                    },
                    new Player
                    { 
                        PlayerID = 3,
                        PlayerName = "Caff",
                        Active = true
                    },
                }
            );
        }

        public ObservableCollection<Player> GetPlayersWithDuplicatePlayer()
        {
            return new ObservableCollection<Player>(
                new List<Player> 
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
                        PlayerName = "Jamie",
                        Active = true
                    },
                    new Player
                    { 
                        PlayerID = 3,
                        PlayerName = "Caff",
                        Active = true
                    },
                }
            );
        }

        public ObservableCollection<Player> GetPlayersWithNonAlphaCharacters()
        {
            return new ObservableCollection<Player>(
                new List<Player> 
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
                        PlayerName = "Caff3",
                        Active = true
                    },
                }
            );
        }

        public ObservableCollection<Player> GetPlayersWithInvalidEmailAddress()
        {
            return new ObservableCollection<Player>(
                new List<Player> 
                { 
                    new Player
                    { 
                        PlayerID = 1,
                        PlayerName = "Jamie",
                        Active = true,
                        EmailAddress = "test1@test.com"
                    },
                    new Player
                    { 
                        PlayerID = 2,
                        PlayerName = "Mike",
                        Active = true,
                        EmailAddress = "test1@test.com"
                    },
                    new Player
                    { 
                        PlayerID = 3,
                        PlayerName = "Caff",
                        Active = true,
                        EmailAddress = "invalidemailaddress"
                    },
                }
            );
        }

        public List<string> GetEmailAddresses(List<int?> playerIds)
        {
            return new List<string>() {
                "test1@test.com",
                "test2@test.com",
                "test3@test.co.uk"
            };
        }

        public bool InsertPlayers(ObservableCollection<Player> players)
        {
            return true;
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
