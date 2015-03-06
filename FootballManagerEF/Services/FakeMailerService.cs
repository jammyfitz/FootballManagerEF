using FootballManagerEF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FootballManagerEF.Services
{
    public class FakeMailerService : IMailerService
    {
        public bool SendStats()
        {
            return true;
        }

        public bool SendTeams()
        {
            return true;
        }

        public bool SendOKMessageToUser()
        {
            return true;
        }
    }
}
