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
    public class FootballRepository : IFootballRepository, IDisposable
    {
        private IMatchRepository matchRepository;
        private IPlayerMatchRepository playerMatchRepository;
        private IPlayerRepository playerRepository;
        private ITeamRepository teamRepository;
        private FootballEntities context;

        public FootballRepository(FootballEntities context)
        {
            this.context = context;
            matchRepository = new MatchRepository(context);
            playerMatchRepository = new PlayerMatchRepository(context);
            playerRepository = new PlayerRepository(context);
            teamRepository = new TeamRepository(context);
        }

        public FootballRepository()
        {
            matchRepository = new FakeMatchRepository();
            playerMatchRepository = new FakePlayerMatchRepository();
            playerRepository = new FakePlayerRepository();
            teamRepository = new FakeTeamRepository();
        }

        #region Match Repository
        public List<Match> GetMatches()
        {
            return matchRepository.GetMatches();
        }

        public Match GetMatchByID(int id)
        {
            return matchRepository.GetMatchByID(id);
        }
        #endregion

        #region PlayerMatch Repository
        public List<PlayerMatch> GetPlayerMatches(int matchId)
        {
            return playerMatchRepository.GetPlayerMatches(matchId);
        }

        public List<PlayerMatch> GetTenPlayerMatches(int matchId)
        {
            return playerMatchRepository.GetTenPlayerMatches(matchId);
        }
        #endregion

        #region Player Repository
        public List<Player> GetPlayers()
        {
            return playerRepository.GetPlayers();
        }

        public Player GetPlayerByID(int playerId)
        {
            return playerRepository.GetPlayerByID(playerId);
        }
        #endregion

        #region Team Repository
        public List<Team> GetTeams()
        {
            return teamRepository.GetTeams();
        }

        public Team GetTeamByID(int teamId)
        {
            return teamRepository.GetTeamByID(teamId);
        }
        #endregion


        public void Save()
        {
            context.SaveChanges();
        }

        #region IDisposable Members
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
        #endregion
    }
}
