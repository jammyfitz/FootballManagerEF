using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Repositories
{
    public class FakeFootballRepository : IFootballRepository
    {
        private FakeMatchRepository matchRepository;
        private FakePlayerMatchRepository playerMatchRepository;
        private FakePlayerRepository playerRepository;
        private FakeTeamRepository teamRepository;

        public FakeFootballRepository()
        {
            matchRepository = new FakeMatchRepository();
            playerMatchRepository = new FakePlayerMatchRepository();
            playerRepository = new FakePlayerRepository();
            teamRepository = new FakeTeamRepository();
        }

        public List<Match> GetMatches()
        {
            return matchRepository.GetMatches();
        }

        public Match GetMatchByID(int id)
        {
            return matchRepository.GetMatchByID(id);
        }

        public List<PlayerMatch> GetPlayerMatches(int matchId)
        {
            return playerMatchRepository.GetPlayerMatches(matchId);
        }

        public List<Player> GetPlayers()
        {
            return playerRepository.GetPlayers();
        }

        public Player GetPlayerByID(int playerId)
        {
            return playerRepository.GetPlayerByID(playerId);
        }

        public List<Team> GetTeams()
        {
            return teamRepository.GetTeams();
        }

        public Team GetTeamByID(int teamId)
        {
            return teamRepository.GetTeamByID(teamId);
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
