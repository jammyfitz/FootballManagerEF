﻿using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerStatsRepository : IDisposable
    {
        List<PlayerStat> GetPlayerStats();

        List<PlayerCalculation> GetPlayerCalculations();

        List<PlayerStatisticsData> GetPlayerStatisticsData();
    }
}
