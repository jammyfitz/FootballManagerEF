using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Helpers
{
    public static class MatchHelper
    {
        public static Match GetNewSubsequentMatch(this Match match)
        {
            Match newMatch = new Match();
            newMatch.MatchDate = match.MatchDate.Value.AddDays(7);
            return newMatch;
        }
    }
}
