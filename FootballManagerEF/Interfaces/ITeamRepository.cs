using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface ITeamRepository : IDisposable
    {
        List<Team> GetTeams();
        Team GetTeamByID(int teamId);
        void Save();
    }
}
