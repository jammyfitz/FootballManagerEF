using FootballManagerEF.Models;

namespace FootballManagerEF.Extensions
{
    public static class PlayerMatchExtensions
    {
        public static bool WonMatch(this PlayerMatch playerMatch)
        {
            return playerMatch.Match.MatchWinner.HasValue && playerMatch.Match.MatchWinner.Value == playerMatch.TeamID;
        }
    }
}
