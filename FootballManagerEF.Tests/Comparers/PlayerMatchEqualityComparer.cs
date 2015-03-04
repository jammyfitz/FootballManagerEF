using FootballManagerEF.Models;
using System.Collections.Generic;

public class PlayerMatchEqualityComparer : IEqualityComparer<PlayerMatch>
{
    public bool Equals(PlayerMatch x, PlayerMatch y)
    {
        if (object.ReferenceEquals(x, y)) return true;

        if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

        return (x.MatchID == y.MatchID && x.PlayerID == y.PlayerID && x.PlayerMatchID == y.PlayerMatchID && x.TeamID == y.TeamID);
    }

    public int GetHashCode(PlayerMatch obj)
    {
        if (object.ReferenceEquals(obj, null)) return 0;

        int hash = 13;
        hash = (hash * 7) + obj.MatchID.GetHashCode();
        hash = (hash * 7) + obj.PlayerID.GetHashCode();
        hash = (hash * 7) + obj.PlayerMatchID.GetHashCode();
        hash = (hash * 7) + obj.TeamID.GetHashCode();
        return hash;
    }
}