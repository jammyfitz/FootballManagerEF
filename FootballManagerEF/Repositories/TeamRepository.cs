using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Repositories
{
    public class TeamRepository : ITeamRepository, IDisposable
    {
        private FootballEntities context;

        public TeamRepository(FootballEntities context)
        {
            this.context = context;
        }

        public List<Team> GetTeams()
        {
            return GetTeamsByDateAsc();
        }

        public List<Team> GetTeamsByDateAsc()
        {
            return context.Teams.OrderBy(x => x.TeamName).ToList();
        }

        public Team GetTeamByID(int teamId)
        {
            return context.Teams.Find(teamId);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
