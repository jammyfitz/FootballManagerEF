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
        ObservableCollection<Match> GetMatches();
        ObservableCollection<PlayerMatch> GetPlayerMatches(int matchId);
        ObservableCollection<Player> GetAllPlayers();
        ObservableCollection<Player> GetActivePlayers();
        ObservableCollection<Team> GetTeams();
        Config GetConfig();
        List<PlayerStat> GetPlayerStats();
        List<PlayerCalculation> GetPlayerCalculations();
        Match GetMatchByID(int matchId);
        Player GetPlayerByID(int playerId);
        Team GetTeamByID(int teamId);
        bool InsertPlayerMatches(ObservableCollection<PlayerMatch> playerMatchesToInsert, int matchId);
        bool InsertPlayers(ObservableCollection<Player> playersToInsert);
        Match InsertMatch(Match match);
        bool DeleteMatch(Match match);
        Match GetNewMatch();
        List<string> GetEmailAddresses(List<int?> playerIds);
        void Refresh();
        void Save();
    }
}
