using FootballManagerEF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Comparers
{
    public class PlayerDataComparer : IComparable
    {
        private decimal? _totalWinRatio;

        public PlayerDataComparer(decimal? totalWinRatio)
        {
            _totalWinRatio = totalWinRatio;
        }

        public int CompareTo(object x)
        {
            //return ((PlayerData)x).WinRatio.CompareTo(null);
            return -1;
        }
    }
}
