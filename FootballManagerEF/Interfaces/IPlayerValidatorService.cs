using FootballManagerEF.Models;
using System.Collections.Generic;
using System.Text;

namespace FootballManagerEF.Interfaces
{
    public interface IPlayerValidatorService : IValidatorService
    {
        List<Player> Players { get; set; }
    }
}
