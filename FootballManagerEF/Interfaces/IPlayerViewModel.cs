using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerViewModel
    {
        ObservableCollection<Player> GetAllPlayers();
        ObservableCollection<Player> Players { get; set; }
    }
}
