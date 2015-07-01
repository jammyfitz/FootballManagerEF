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

        public static void Swap(this IList<PlayerData> playerDataList, PlayerData firstSwapCandidate, PlayerData lastSwapCandidate)
        {
            var firstIndex = playerDataList.GetIndexByPlayerId(firstSwapCandidate.PlayerMatch.PlayerID);
            var secondIndex = playerDataList.GetIndexByPlayerId(lastSwapCandidate.PlayerMatch.PlayerID);

            var tmp = playerDataList[firstIndex];
            playerDataList[firstIndex] = playerDataList[secondIndex];
            playerDataList[secondIndex] = tmp;
        }

        public static PlayerData GetClosestToWinRatio(this IEnumerable<PlayerData> enumeration, decimal target)
        {
            PlayerData closest = enumeration.OrderBy(playerData => Math.Abs((decimal)(target - playerData.WinRatio))).First();
            return closest;
        }

        public static int GetIndexByPlayerId(this IList<PlayerData> playerDataList, int? playerId)
        {
            return playerDataList.IndexOf(playerDataList.Single(x => x.PlayerMatch.PlayerID == playerId));
        }

        public static void EvenlyDistributePlayersFromList(this ObservableCollection<PlayerMatch> playerMatchList, IList<PlayerData> playerDataList)
        {
            for (int i = 0; i < playerDataList.Count() / 2; i++)
            {
                playerMatchList.Add(playerDataList.ElementAt(playerDataList.Count() - (i + 1)).PlayerMatch);
                playerMatchList.Add(playerDataList.ElementAt(i).PlayerMatch);
            }
        }

        public static void DistributePlayersBasedOnListOrder(this ObservableCollection<PlayerMatch> playerMatchList, IList<PlayerData> playerDataList)
        {
            foreach (var playerData in playerDataList)
            {
                playerMatchList.Add(playerData.PlayerMatch);
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

    }
}
