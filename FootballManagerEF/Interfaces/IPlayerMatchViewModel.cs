using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerMatchViewModel
    {
        List<PlayerMatch> GetPlayerMatches(int matchId);
        List<Player> GetPlayers();
        List<Team> GetTeams();
        List<PlayerMatch> PlayerMatches{ get; set;}
    }
}
