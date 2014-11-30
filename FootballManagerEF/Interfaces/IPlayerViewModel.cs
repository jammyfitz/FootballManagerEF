using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerViewModel
    {
        List<Player> GetAllPlayers();
        List<Player> Players { get; set; }
    }
}
