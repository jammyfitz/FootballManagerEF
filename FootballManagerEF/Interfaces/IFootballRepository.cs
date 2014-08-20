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
        List<PlayerMatch> GetPlayerMatches(int matchId);
        List<Player> GetPlayers();
        List<Team> GetTeams();
        Match GetMatchByID(int matchId);
        Player GetPlayerByID(int playerId);
        Team GetTeamByID(int teamId);
        bool InsertPlayerMatches(List<PlayerMatch> playerMatchesToInsert, int matchId);
        void Save();
    }
}
