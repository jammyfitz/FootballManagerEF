using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IMatch
    {
        int MatchID { get; set; }
        Nullable<System.DateTime> MatchDate { get; set; }
        Nullable<int> MatchWinner { get; set; }
    }
}
