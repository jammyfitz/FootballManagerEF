using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FootballManagerEF.Repositories
{
    public class FakeTeamRepository : ITeamRepository
    {
        public ObservableCollection<Team> GetTeams()
        {
            return new ObservableCollection<Team> 
           { 
              new Team
              { 
                  TeamID = 1,
                  TeamName = "Bibs"
              },
              new Team
              { 
                  TeamID = 2,
                  TeamName = "Non Bibs"
              }
           };
        }

        public Team GetTeamByID(int teamId)
        {
            return new Team
            {
                TeamID = 1,
                TeamName = "Bibs"
            };
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
