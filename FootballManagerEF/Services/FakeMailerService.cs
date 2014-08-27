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
        public bool SendEmail()
        {
            return true;
        }
    }
}
