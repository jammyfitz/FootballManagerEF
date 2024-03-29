﻿using FootballManagerEF.Extensions;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;

namespace FootballManagerEF.Helpers
{
    public static class SelectorServiceHelper
    {
        public static bool ShortestPlayerNotInFirstTeam(ObservableCollection<PlayerMatch> playerMatchList, Player shortestPlayer)
        {
            if (shortestPlayer == null)
                return false;

            return !playerMatchList.TakeFirstHalf().Where(x => x.PlayerID == shortestPlayer.PlayerID).Any();
        }

        public static decimal? GetWinRatio(PlayerStat playerStat)
        {
            if (playerStat.MatchesPlayed == 0)
                return 0.5m;

            decimal? matchWins = playerStat.MatchWins;
            decimal? matchesPlayed = playerStat.MatchesPlayed;
            return matchWins / matchesPlayed;
        }

        public static decimal GetOffset(decimal? firstValue, decimal? secondValue)
        {
            return Math.Abs((decimal)(firstValue - secondValue));
        }

        public static void AssignShortestTeamToBibs(ObservableCollection<PlayerMatch> playerMatchList, IFootballRepository footballRepository)
        {
            var teams = footballRepository.GetTeams();
            var players = footballRepository.GetActivePlayers();

            playerMatchList.AssignTeamsBasedOnListOrder(teams);

            var playersInList = from player in players
                                  join playermatch in playerMatchList on player.PlayerID equals playermatch.PlayerID
                                  where player.Height != null
                                  select player;

            var shortestPlayer = GetShortestPlayer(playersInList); 

            if (ShortestPlayerNotInFirstTeam(playerMatchList, shortestPlayer))
                playerMatchList.SwapTeams(teams);
        }

        public static decimal GetPlayerScore(PlayerCalculation playerCalculation)
        {
            var mostRecentWinsToCount = Convert.ToInt32(ConfigurationManager.AppSettings["MostRecentWinsToCount"] ?? "5");
            var recentMatchWinsForCalculation = playerCalculation.RecentMatchCount < mostRecentWinsToCount ? ((decimal)mostRecentWinsToCount / 2) : (decimal)playerCalculation.RecentMatchWins;
            var recentWinScore = ((decimal)recentMatchWinsForCalculation / (decimal)mostRecentWinsToCount);
            var winRatioScore = GetWinRatioScore(playerCalculation.WinRatio, playerCalculation.MatchesPlayed);
            var finalPlayerScore = recentWinScore + winRatioScore;

            return finalPlayerScore;
        }

        private static decimal GetWinRatioScore(decimal? winRatio, int? matchesPlayed)
        {
            if (!winRatio.HasValue || !matchesPlayed.HasValue || matchesPlayed == 0)
            {
                return 0.5m;
            }

            if (matchesPlayed > 0 && winRatio == 0)
            {
                return 0m;
            }

            return winRatio.Value / 100;
        }

        private static Player GetShortestPlayer(IEnumerable<Player> playersInList)
        {
            return playersInList.OrderBy(x => x.Height).FirstOrDefault();
        }
    }
}
