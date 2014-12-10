﻿using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface ITeamRepository : IDisposable
    {
        ObservableCollection<Team> GetTeams();
        Team GetTeamByID(int teamId);
        void Save();
    }
}
