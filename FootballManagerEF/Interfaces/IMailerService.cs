using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IMailerService
    {
        bool SendEmail();
        bool SendOKMessageToUser();
    }
}
