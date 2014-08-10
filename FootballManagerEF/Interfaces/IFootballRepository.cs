using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IFootballRepository : IMatchRepository, IPlayerMatchRepository, IPlayerRepository, ITeamRepository, IDisposable
    {
        List<Match> GetMatches();
        Match GetMatchByID(int matchId);
        List<PlayerMatch> GetPlayerMatches(int matchId);
        List<Player> GetPlayers();
        Player GetPlayerByID(int playerId);
        List<Team> GetTeams();
        Team GetTeamByID(int teamId);
        void Save();
    }
}
