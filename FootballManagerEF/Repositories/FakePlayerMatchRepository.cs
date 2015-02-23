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
    public class FakePlayerMatchRepository : IPlayerMatchRepository
    {
        public void Save() {}

        public ObservableCollection<PlayerMatch> GetPlayerMatches(int matchId)
        {
            return AddFivePlayerMatches();
        }

        public ObservableCollection<PlayerMatch> GetTenPlayerMatches(int matchId)
        {
            return GetFiveFilledAndFiveEmptyPlayerMatches();
        }

        public bool InsertPlayerMatches(ObservableCollection<PlayerMatch> playerMatches, int matchId)
        {
            return true;
        }

        public ObservableCollection<PlayerMatch> GetFiveFilledAndFiveEmptyPlayerMatches()
        {
            ObservableCollection<PlayerMatch> playerMatches = AddFivePlayerMatches();
            AddFiveEmptyPlayerMatches(playerMatches);
            return playerMatches;
        }

        public ObservableCollection<PlayerMatch> GetPlayerMatchesWithPlayerAndNoTeam()
        {
            return new ObservableCollection<PlayerMatch> 
            { 
                new PlayerMatch
                { 
                    PlayerMatchID = 1,
                    PlayerID = 1,
                    MatchID = 1
                },
                new PlayerMatch
                { 
                    PlayerMatchID = 2,
                    PlayerID = 2,
                    MatchID = 1
                }
            };
        }

        public ObservableCollection<PlayerMatch> GetPlayerMatchesWithTeamAndNoPlayer()
        {
            return new ObservableCollection<PlayerMatch> 
            { 
                new PlayerMatch
                { 
                    PlayerMatchID = 1,
                    TeamID = 1,
                    MatchID = 1
                },
                new PlayerMatch
                { 
                    PlayerMatchID = 2,
                    TeamID = 2,
                    MatchID = 1
                }
            };
        }

        public ObservableCollection<PlayerMatch> GetPlayerMatchesWithDuplicatePlayer()
        {
            return new ObservableCollection<PlayerMatch> 
            { 
                AddPlayerMatch(),
                AddPlayerMatch()
            };
        }

        public ObservableCollection<PlayerMatch> GetPlayerMatchesWithTooManyPlayersInATeam()
        {
            return new ObservableCollection<PlayerMatch> 
           { 
              new PlayerMatch
              { 
                  PlayerMatchID = 1,
                  PlayerID = 1,
                  MatchID = 1,
                  TeamID = 1
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 2,
                  PlayerID = 2,
                  MatchID = 1,
                  TeamID = 1
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 3,
                  PlayerID = 3,
                  MatchID = 1,
                  TeamID = 1
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 4,
                  PlayerID = 4,
                  MatchID = 1,
                  TeamID = 1
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 5,
                  PlayerID = 5,
                  MatchID = 1,
                  TeamID = 1
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 6,
                  PlayerID = 6,
                  MatchID = 1,
                  TeamID = 1
              }
           };
        }

        private static ObservableCollection<PlayerMatch> AddFivePlayerMatches()
        {
            return new ObservableCollection<PlayerMatch> 
            { 
                AddPlayerMatch(),
                AddPlayerMatch(),
                AddPlayerMatch(),
                AddPlayerMatch(),
                AddPlayerMatch()
            };
        }

        private static ObservableCollection<PlayerMatch> AddFiveDistinctPlayerMatches()
        {
            return new ObservableCollection<PlayerMatch> 
           { 
              new PlayerMatch
              { 
                  PlayerMatchID = 1,
                  PlayerID = 1,
                  MatchID = 1,
                  TeamID = 1
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 2,
                  PlayerID = 2,
                  MatchID = 1,
                  TeamID = 2
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 3,
                  PlayerID = 3,
                  MatchID = 1,
                  TeamID = 1
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 4,
                  PlayerID = 4,
                  MatchID = 1,
                  TeamID = 2
              },
              new PlayerMatch
              { 
                  PlayerMatchID = 5,
                  PlayerID = 5,
                  MatchID = 1,
                  TeamID = 2
              }
           };
        }

        private static ObservableCollection<PlayerMatch> AddFiveEmptyPlayerMatches(ObservableCollection<PlayerMatch> playerMatches)
        {
            playerMatches.Add(new PlayerMatch());
            playerMatches.Add(new PlayerMatch());
            playerMatches.Add(new PlayerMatch());
            playerMatches.Add(new PlayerMatch());
            playerMatches.Add(new PlayerMatch());

            return new ObservableCollection<PlayerMatch>(playerMatches);
        }

        private static PlayerMatch AddPlayerMatch()
        {
            return new PlayerMatch
            {
                PlayerMatchID = 1,
                PlayerID = 1,
                MatchID = 1,
                TeamID = 1
            };
        }

        #region IDisposable Members
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
