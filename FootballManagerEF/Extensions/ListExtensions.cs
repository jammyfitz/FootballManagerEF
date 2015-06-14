using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void EquallyHalveBy<T>(this IList<T> list, Func<PlayerData,bool> predicate)
        {
            //var query = from trade in list
            //            where predicate(trade)
            //            orderby trade.TradeTime descending
            //            select trade;
            //return (query.First().Value - query.Last().Value) / query.First().Value * 100;


        }
    }
}
