using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerMatchViewModel
    {
        ObservableCollection<PlayerMatch> GetPlayerMatches(int matchId);
        ObservableCollection<Player> GetActivePlayers();
        ObservableCollection<Team> GetTeams();
        ObservableCollection<PlayerMatch> PlayerMatches { get; set; }
        ObservableCollection<Player> Players { get; set; }
    }
}
