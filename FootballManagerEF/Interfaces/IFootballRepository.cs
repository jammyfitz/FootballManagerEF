using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IFootballRepository : IMatchRepository, IPlayerMatchRepository, IPlayerRepository, ITeamRepository, IConfigRepository, IPlayerStatsRepository, IDisposable
    {
        List<Match> GetMatches();
        ObservableCollection<PlayerMatch> GetPlayerMatches(int matchId);
        ObservableCollection<Player> GetAllPlayers();
        ObservableCollection<Player> GetActivePlayers();
        ObservableCollection<Team> GetTeams();
        Config GetConfig();
        List<PlayerStat> GetPlayerStats();
        Match GetMatchByID(int matchId);
        Player GetPlayerByID(int playerId);
        Team GetTeamByID(int teamId);
        bool InsertPlayerMatches(ObservableCollection<PlayerMatch> playerMatchesToInsert, int matchId);
        bool InsertPlayers(ObservableCollection<Player> playersToInsert);
        void Refresh();
        void Save();
    }
}
