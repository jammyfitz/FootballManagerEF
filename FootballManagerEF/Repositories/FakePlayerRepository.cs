using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FootballManagerEF.Repositories
{
    public class FakePlayerRepository : IPlayerRepository
    {
        public ObservableCollection<Player> GetAllPlayers()
        {
            return new ObservableCollection<Player>
            {
                AddPlayer(1, "Jamie"),
                AddPlayer(2, "Mike"),
                AddPlayer(3, "Caff"),
                AddPlayer(4, "Steve"),
                AddPlayer(5, "Richard"),
                AddPlayer(6, "Bruce"),
                AddPlayer(7, "Simon"),
                AddPlayer(8, "Ant"),
                AddPlayer(9, "Dec"),
                AddPlayer(10, "Walter")
            };
        }

        public ObservableCollection<Player> GetActivePlayers()
        {
            return new ObservableCollection<Player>
           {
                AddPlayer(1, "Jamie"),
                AddPlayer(2, "Mike"),
                AddPlayer(3, "Caff"),
                AddPlayer(4, "Steve"),
                AddPlayer(5, "Richard"),
                AddPlayer(6, "Bruce"),
                AddPlayer(7, "Simon"),
                AddPlayer(8, "Ant"),
                AddPlayer(9, "Dec"),
                AddPlayerWithEmailAndHeight(10, "Walter", string.Empty, 180)
           };
        }

        public Player GetPlayerByID(int playerId)
        {
            return AddPlayer(1, "Jamie");
        }

        public ObservableCollection<Player> GetTenValidPlayers()
        {
            return new ObservableCollection<Player> 
            {
                AddPlayer(1, "Jamie"),
                AddPlayer(2, "Mike"),
                AddPlayer(3, "Caff"),
                AddPlayer(4, "Steve"),
                AddPlayer(5, "Richard"),
                AddPlayer(6, "Bruce"),
                AddPlayer(7, "Simon"),
                AddInactivePlayer(8, "Ant"),
                AddInactivePlayer(9, "Dec"),
                AddPlayer(7, "Walter")
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
                    AddPlayer(2, "Mike"),
                    AddPlayer(3, "Caff")
                }
            );
        }

        public ObservableCollection<Player> GetPlayersWithActiveFlagAndNoPlayerName()
        {
           return new ObservableCollection<Player>(
                new List<Player> 
                {
                    AddPlayer(1, "Jamie"),
                    new Player
                    { 
                        PlayerID = 2,
                        Active = true
                    },
                    AddPlayer(3, "Caff"),
                }
            );
        }

        public ObservableCollection<Player> GetPlayersWithDuplicatePlayerName()
        {
            return new ObservableCollection<Player>(
                new List<Player> 
                {
                    AddPlayer(1, "Jamie"),
                    AddPlayer(2, "Jamie"),
                    AddPlayer(3, "Caff"),
                }
            );
        }

        public ObservableCollection<Player> GetPlayersWithNonAlphaCharacters()
        {
            return new ObservableCollection<Player>(
                new List<Player>
                {
                    AddPlayer(1, "Jamie"),
                    AddPlayer(2, "Mike"),
                    AddPlayer(3, "Caff3")
                }
            );
        }

        public ObservableCollection<Player> GetPlayersWithInvalidEmailAddress()
        {
            return new ObservableCollection<Player>(
                new List<Player> 
                {
                    AddPlayerWithEmail(1, "Jamie", "test1@test.com"),
                    AddPlayerWithEmail(2, "Mike", "test2@test.com"),
                    AddPlayerWithEmail(3, "Caff", "invalidemailaddress")
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

        private static Player AddPlayer(int playerId, string playerName)
        {
            return AddPlayer(playerId, playerName, string.Empty, string.Empty, true, null);
        }

        private static Player AddInactivePlayer(int playerId, string playerName)
        {
            return AddPlayer(playerId, playerName, string.Empty, string.Empty, false, null);
        }

        private static Player AddPlayerWithEmail(int playerId, string playerName, string emailAddress)
        {
            return AddPlayer(playerId, playerName, emailAddress, string.Empty, true, null);
        }

        private static Player AddPlayerWithEmailAndHeight(int playerId, string playerName, string emailAddress, Nullable<decimal> height)
        {
            return AddPlayer(playerId, playerName, emailAddress, string.Empty, true, height);
        }

        private static Player AddPlayer(int playerId, string playerName, string emailAddress, string mobile, Nullable<bool> active, Nullable<decimal> height)
        {
            return new Player
            {
                PlayerID = playerId,
                PlayerName = playerName,
                EmailAddress = emailAddress,
                Mobile = mobile,
                Active = active,
                Height = height
            };
        }
    }
}
