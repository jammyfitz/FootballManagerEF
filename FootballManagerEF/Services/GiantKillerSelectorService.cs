using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FootballManagerEF.Helpers;

namespace FootballManagerEF.Services
{
    public class GiantKillerSelectorService : ISelectorService
    {
        private List<PlayerStat> _playerStats;
        private ObservableCollection<Team> _teams;
        private IFootballRepository _footballRepository;

        public GiantKillerSelectorService(IFootballRepository footballRepository)
        {
            _footballRepository = footballRepository;
            _playerStats = _footballRepository.GetPlayerStats();
            _teams = _footballRepository.GetTeams();
        }

        public ObservableCollection<PlayerMatch> ApplyAlgorithm(ObservableCollection<PlayerMatch> playerMatches)
        {
            ObservableCollection<PlayerMatch> outputList = new ObservableCollection<PlayerMatch>();

            //Join the win stats onto list of user selected players
            IEnumerable<PlayerData> result = from pm in playerMatches
                                             join ps in _playerStats.DefaultIfEmpty() on pm.PlayerID equals ps.PlayerID into temp
                                             from subtemp in temp.DefaultIfEmpty()
                                             select new PlayerData { PlayerMatch = pm, MatchWins = (subtemp == null ? 0 : subtemp.MatchWins) } into results
                                             orderby results.MatchWins descending
                                             select results;

            IList<PlayerData> playerData = result.ToList();

            outputList.EvenlyDistributePlayersFromList(playerData);
            SelectorServiceHelper.AssignShortestTeamToBibs(outputList, _footballRepository);

            return outputList;
        }
    }
}
