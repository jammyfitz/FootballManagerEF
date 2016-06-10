using FootballManagerEF.Extensions;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

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
                return 0.5M;

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

        private static Player GetShortestPlayer(IEnumerable<Player> playersInList)
        {
            return playersInList.OrderBy(x => x.Height).FirstOrDefault();
        }
    }
}
