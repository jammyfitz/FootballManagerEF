using FootballManagerEF.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerValidatorService : IValidatorService
    {
        ObservableCollection<Player> Players { get; set; }
    }
}
