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
    public static class StringExtensions
    {
        public static string GetOnly(this IList<string> list, string entryToInclude)
        {
            var result = list.FirstOrDefault(x => x == entryToInclude);

            return result;
        }

        public static IList<string> GetWithout(this IList<string> list, string entryToExclude)
        {
            var result = list.Where(x => x != entryToExclude);

            return result.ToList();
        }
    }
}
