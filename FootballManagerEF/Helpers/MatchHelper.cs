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

        public static Match GetNewSubsequentMatch()
        {
            Match newMatch = new Match();
            newMatch.MatchDate = GetNextWeekday(DateTime.Now, DayOfWeek.Thursday);
            return newMatch;
        }

        private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
    }
}
