using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Extensions
{
    public static class ListExtensions
    {
        #region Generic Extension Methods

        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IEnumerable<T> TakeFirstHalf<T>(this IList<T> list)
        {
            return list.Take(list.Count / 2);
        }

        public static IEnumerable<T> TakeLastHalf<T>(this IList<T> list)
        {
            return list.Skip(list.Count / 2);
        }

        #endregion

        public static void Swap(this IList<PlayerCalculationWithScore> playerList, PlayerCalculationWithScore firstSwapCandidate, PlayerCalculationWithScore lastSwapCandidate)
        {
            var firstIndex = playerList.GetIndexByPlayerId(firstSwapCandidate.PlayerMatch.PlayerID);
            var secondIndex = playerList.GetIndexByPlayerId(lastSwapCandidate.PlayerMatch.PlayerID);

            var tmp = playerList[firstIndex];
            playerList[firstIndex] = playerList[secondIndex];
            playerList[secondIndex] = tmp;
        }

        public static PlayerCalculationWithScore GetClosestToWinRatio(this IEnumerable<PlayerCalculationWithScore> enumeration, decimal target)
        {
            PlayerCalculationWithScore closestMatch = enumeration.OrderBy(player => Math.Abs((decimal)(target - player.Score))).First();
            return closestMatch;
        }

        public static int GetIndexByPlayerId(this IList<PlayerCalculationWithScore> playerList, int? playerId)
        {
            return playerList.IndexOf(playerList.Single(x => x.PlayerMatch.PlayerID == playerId));
        }

        public static void EvenlyDistributePlayersFromList(this ObservableCollection<PlayerMatch> playerMatchList, IList<PlayerData> playerDataList)
        {
            for (int i = 0; i < playerDataList.Count() / 2; i++)
            {
                playerMatchList.Add(playerDataList.ElementAt(playerDataList.Count() - (i + 1)).PlayerMatch);
                playerMatchList.Add(playerDataList.ElementAt(i).PlayerMatch);
            }
        }

        public static void DistributePlayersBasedOnListOrder(this ObservableCollection<PlayerMatch> playerMatchList, IList<PlayerCalculationWithScore> playerList)
        {
            foreach (var player in playerList)
            {
                playerMatchList.Add(player.PlayerMatch);
            }
        }

        public static void AssignTeamsBasedOnListOrder(this ObservableCollection<PlayerMatch> playerMatchList, ObservableCollection<Team> teamList)
        {
            int firstTeamId = teamList.First().TeamID;
            int lastTeamId = teamList.Last().TeamID;
            int teamSize = playerMatchList.Count() / 2;

            for (int i = 0; i < playerMatchList.Count(); i++)
            {
                playerMatchList.ElementAt(i).TeamID = (i < teamSize) ? firstTeamId : lastTeamId;
            }
        }

        public static void SwapTeams(this ObservableCollection<PlayerMatch> playerMatchList, ObservableCollection<Team> teamList)
        {
            int firstTeamId = teamList.First().TeamID;
            int lastTeamId = teamList.Last().TeamID;

            foreach (PlayerMatch playerMatch in playerMatchList)
            {
                if(playerMatch.TeamID == firstTeamId)
                {
                    playerMatch.TeamID = lastTeamId;
                }
                else
                {
                    playerMatch.TeamID = firstTeamId;
                }
            }
        }

        public static decimal GetWinRatio(this List<PlayerMatch> playerMatches)
        {
            var totalMatchWins = playerMatches.Where(y => y.WonMatch()).Count();
            var matchesPlayed = playerMatches.Count();
            var winRatio = ((decimal)totalMatchWins / (decimal)matchesPlayed) * 100;

            return winRatio;
        }

        public static int GetGoalsFor(this List<PlayerMatch> playerMatches)
        {
            var totalGoalsScoredAsBibs = playerMatches.Where(x => x.Match.BibsGoals.HasValue).Sum(x => x.TeamID == 1 ? x.Match.BibsGoals.Value : 0);
            var totalGoalsScoredAsNonBibs = playerMatches.Where(x => x.Match.NonBibsGoals.HasValue).Sum(x => x.TeamID == 2 ? x.Match.NonBibsGoals.Value : 0);
            var totalGoalsScored = totalGoalsScoredAsBibs + totalGoalsScoredAsNonBibs;

            return totalGoalsScored;
        }

        public static int GetGoalsAgainst(this List<PlayerMatch> playerMatches)
        {
            var totalGoalsConceededAsBibs = playerMatches.Where(x => x.Match.NonBibsGoals.HasValue).Sum(x => x.TeamID == 1 ? x.Match.NonBibsGoals.Value : 0);
            var totalGoalsConceededAsNonBibs = playerMatches.Where(x => x.Match.BibsGoals.HasValue).Sum(x => x.TeamID == 2 ? x.Match.BibsGoals.Value : 0);
            var totalGoalsConceeded = totalGoalsConceededAsBibs + totalGoalsConceededAsNonBibs;

            return totalGoalsConceeded;
        }

        public static decimal GetAverageGoalsFor(this List<PlayerMatch> playerMatches)
        {
            var goalsFor = playerMatches.GetGoalsFor();
            var averageGoalsFor = playerMatches.GetAverageOverMatchesPlayed(goalsFor);

            return averageGoalsFor;
        }

        public static decimal GetAverageGoalsAgainst(this List<PlayerMatch> playerMatches)
        {
            var goalsAgainst = playerMatches.GetGoalsAgainst();
            var averageGoalsFor = playerMatches.GetAverageOverMatchesPlayed(goalsAgainst);

            return averageGoalsFor;
        }

        private static decimal GetAverageOverMatchesPlayed(this List<PlayerMatch> playerMatches, decimal stat)
        {
            var matchesPlayed = playerMatches.Count();
            var averageGoalsFor = (decimal)stat / (decimal)matchesPlayed;

            return averageGoalsFor;
        }

        private static int? GetShortestPlayersTeamId(ObservableCollection<PlayerMatch> playerMatchList)
        {
            return playerMatchList.OrderByDescending(x => x.Player.Height).First().TeamID;
        }
    }
}
