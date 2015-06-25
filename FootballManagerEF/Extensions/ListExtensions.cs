using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Extensions
{
    public static class ListExtensions
    {
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

        public static void Swap(this IList<PlayerData> playerDataList, PlayerData firstSwapCandidate, PlayerData lastSwapCandidate)
        {
            var firstIndex = playerDataList.IndexOf(firstSwapCandidate);
            var secondIndex = playerDataList.IndexOf(lastSwapCandidate);

            var tmp = playerDataList[firstIndex];
            playerDataList[firstIndex] = playerDataList[secondIndex];
            playerDataList[secondIndex] = tmp;
        }

        public static PlayerData GetClosestToWinRatio(this IEnumerable<PlayerData> enumeration, decimal target)
        {
            PlayerData closest = enumeration.OrderBy(playerData => Math.Abs((decimal)(target - playerData.WinRatio))).First();
            return closest;
        }
    }
}
