﻿using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IFootballRepository : IMatchRepository, IPlayerMatchRepository, IPlayerRepository, ITeamRepository, IConfigRepository, IPlayerStatsRepository, IDisposable
    {
        List<Match> GetMatches();
        List<PlayerMatch> GetPlayerMatches(int matchId);
        List<Player> GetAllPlayers();
        List<Player> GetActivePlayers();
        List<Team> GetTeams();
        Config GetConfig();
        List<PlayerStat> GetPlayerStats();
        Match GetMatchByID(int matchId);
        Player GetPlayerByID(int playerId);
        Team GetTeamByID(int teamId);
        bool InsertPlayerMatches(List<PlayerMatch> playerMatchesToInsert, int matchId);
        bool InsertPlayers(List<Player> playersToInsert);
        void Save();
    }
}
