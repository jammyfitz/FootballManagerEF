using FootballManagerEF.Models;
using System;

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
