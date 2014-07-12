using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayer
    {
        int PlayerID { get; set; }
        string PlayerName { get; set; }
        string EmailAddress { get; set; }
        string Mobile { get; set; }
    }
}
