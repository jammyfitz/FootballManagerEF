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
                AddPlayerMatch(1, 1, 1, null),
                AddPlayerMatch(2, 2, 1, null)
            };
        }

        public ObservableCollection<PlayerMatch> GetPlayerMatchesWithTeamAndNoPlayer()
        {
            return new ObservableCollection<PlayerMatch> 
            { 
                AddPlayerMatch(1, null, 1, 1),
                AddPlayerMatch(2, null, 2, 1)
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
              AddPlayerMatch(1, 1, 1, 1),
              AddPlayerMatch(2, 2, 1, 1),
              AddPlayerMatch(3, 3, 1, 1),
              AddPlayerMatch(4, 4, 1, 1),
              AddPlayerMatch(5, 5, 1, 1),
              AddPlayerMatch(6, 6, 1, 1),
           };
        }

        public ObservableCollection<PlayerMatch> GetPlayerMatchesBeforeAlgorithm()
        {
            return new ObservableCollection<PlayerMatch> 
           { 
              AddPlayerMatch(0, 1, 1, 1),
              AddPlayerMatch(0, 2, 1, 2),
              AddPlayerMatch(0, 3, 1, 1),
              AddPlayerMatch(0, 4, 1, 2),
              AddPlayerMatch(0, 5, 1, 1),
              AddPlayerMatch(0, 6, 1, 2),
              AddPlayerMatch(0, 7, 1, 1),
              AddPlayerMatch(0, 8, 1, 2),
              AddPlayerMatch(0, 9, 1, 1),
              AddPlayerMatch(0, 10, 1, 2)
           };
        }

        public ObservableCollection<PlayerMatch> GetPlayerMatchesAfterAlgorithm()
        {
            return new ObservableCollection<PlayerMatch> 
           { 
              AddPlayerMatch(0, 10, 1, 1),
              AddPlayerMatch(0, 1, 1, 1),
              AddPlayerMatch(0, 9, 1, 1),
              AddPlayerMatch(0, 2, 1, 1),
              AddPlayerMatch(0, 8, 1, 1),
              AddPlayerMatch(0, 3, 1, 2),
              AddPlayerMatch(0, 7, 1, 2),
              AddPlayerMatch(0, 4, 1, 2),
              AddPlayerMatch(0, 6, 1, 2),
              AddPlayerMatch(0, 5, 1, 2)
           };
        }

        #region Private

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
            return AddPlayerMatch(1, 1, 1, 1);
        }

        private static PlayerMatch AddPlayerMatch(int playerMatchId, Nullable<int> playerId, Nullable<int> matchId, Nullable<int> teamId)
        {
            return new PlayerMatch
            {
                PlayerMatchID = playerMatchId,
                PlayerID = playerId,
                MatchID = matchId,
                TeamID = teamId
            };
        }

        #endregion

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
