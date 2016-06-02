using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FootballManagerEF.Repositories
{
    public class FootballRepository : IFootballRepository, IDisposable
    {
        private IMatchRepository matchRepository;
        private IPlayerMatchRepository playerMatchRepository;
        private IPlayerRepository playerRepository;
        private ITeamRepository teamRepository;
        private IConfigRepository configRepository;
        private IPlayerStatsRepository playerStatsRepository;
        private FootballEntities context;

        public FootballRepository(FootballEntities context)
        {
            this.context = context;
            InitialiseRepositories();
        }

        public FootballRepository()
        {
            InitialiseTestRepositories();
        }

        public void Refresh()
        {
            if (context == null)
                return;

            context.Dispose();
            context = new FootballEntities();
            InitialiseRepositories();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #region Match Repository
        public ObservableCollection<Match> GetMatches()
        {
            return matchRepository.GetMatches();
        }

        public Match GetMatchByID(int id)
        {
            return matchRepository.GetMatchByID(id);
        }

        public Match InsertMatch(Match match)
        {
            return matchRepository.InsertMatch(match);
        }

        public bool DeleteMatch(Match match)
        {
            return matchRepository.DeleteMatch(match);
        }
        #endregion

        #region PlayerMatch Repository
        public ObservableCollection<PlayerMatch> GetPlayerMatches(int matchId)
        {
            return playerMatchRepository.GetPlayerMatches(matchId);
        }

        public ObservableCollection<PlayerMatch> GetTenPlayerMatches(int matchId)
        {
            return playerMatchRepository.GetTenPlayerMatches(matchId);
        }

        public bool InsertPlayerMatches(ObservableCollection<PlayerMatch> playerMatches, int matchId)
        {
            return playerMatchRepository.InsertPlayerMatches(playerMatches, matchId);
        }

        public ObservableCollection<PlayerMatch> GetFiveFilledAndFiveEmptyPlayerMatches()
        {
            return playerMatchRepository.GetFiveFilledAndFiveEmptyPlayerMatches();
        }
        #endregion

        #region Player Repository
        public ObservableCollection<Player> GetAllPlayers()
        {
            return playerRepository.GetAllPlayers();
        }

        public ObservableCollection<Player> GetActivePlayers()
        {
            return playerRepository.GetActivePlayers();
        }

        public Player GetPlayerByID(int playerId)
        {
            return playerRepository.GetPlayerByID(playerId);
        }

        public bool InsertPlayers(ObservableCollection<Player> players)
        {
            return playerRepository.InsertPlayers(players);
        }

        public List<string> GetEmailAddresses(List<int?> playerIDs)
        {
            return playerRepository.GetEmailAddresses(playerIDs);
        }
        #endregion

        #region Team Repository
        public ObservableCollection<Team> GetTeams()
        {
            return teamRepository.GetTeams();
        }

        public Team GetTeamByID(int teamId)
        {
            return teamRepository.GetTeamByID(teamId);
        }
        #endregion

        #region Config Repository
        public Config GetConfig()
        {
            return configRepository.GetConfig();
        }
        #endregion

        #region PlayerStats Repository
        public List<PlayerStat> GetPlayerStats()
        {
            return playerStatsRepository.GetPlayerStats();
        }

        #endregion

        #region Private
        private void InitialiseRepositories()
        {
            matchRepository = new MatchRepository(context);
            playerMatchRepository = new PlayerMatchRepository(context);
            playerRepository = new PlayerRepository(context);
            teamRepository = new TeamRepository(context);
            configRepository = new ConfigRepository(context);
            playerStatsRepository = new PlayerStatsRepository(context);
        }

        private void InitialiseTestRepositories()
        {
            matchRepository = new FakeMatchRepository();
            playerMatchRepository = new FakePlayerMatchRepository();
            playerRepository = new FakePlayerRepository();
            teamRepository = new FakeTeamRepository();
            configRepository = new FakeConfigRepository();
            playerStatsRepository = new FakePlayerStatsRepository();
        }

        #endregion

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
