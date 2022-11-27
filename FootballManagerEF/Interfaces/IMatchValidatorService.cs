using FootballManagerEF.Models;
using System.Collections.ObjectModel;

namespace FootballManagerEF.Interfaces
{
    public interface IMatchValidatorService : IValidatorService
    {
        ObservableCollection<PlayerMatch> PlayerMatches { get; set; }
        bool DataGridIsComplete();
    }
}
