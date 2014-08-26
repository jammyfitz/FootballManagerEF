using FootballManagerEF.Models;
using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Repositories
{
    public class FakeConfigRepository : IConfigRepository, IDisposable
    {
        public Config GetConfig()
        {
           return new Config 
           { 
              SmtpAgentDutyCode = "TestSmtpAgentDutyCode",
              SmtpAgentSine = "TestSmtpAgentDutyCode",
              SmtpPort = "TestSmtpPort",
              SmtpServer = "TestSmtpServer"
           };
        }

        #region IDisposable Members
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
        #endregion
    }
}
