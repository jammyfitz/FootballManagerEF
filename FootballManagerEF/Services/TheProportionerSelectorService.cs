using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FootballManagerEF.Extensions;
using FootballManagerEF.Helpers;

namespace FootballManagerEF.Services
{
    public class TheProportionerSelectorService : ISelectorService
    {
        private List<PlayerStat> _playerStats;
        private ObservableCollection<Team> _teams;
        private IFootballRepository _footballRepository;

        public TheProportionerSelectorService(IFootballRepository footballRepository)
        {
            _footballRepository = footballRepository;
            _playerStats = PlayerStatHelper.GetPlayerStatsForAllPlayers(_footballRepository.GetPlayerStats(), _footballRepository.GetAllPlayers());
            _teams = _footballRepository.GetTeams();
        }

        public ObservableCollection<PlayerMatch> ApplyAlgorithm(ObservableCollection<PlayerMatch> playerMatches)
        {
            ObservableCollection<PlayerMatch> outputList = new ObservableCollection<PlayerMatch>();

            //Join the win stats onto list of user selected players
            IEnumerable<PlayerData> result = from pm in playerMatches
                                             join ps in _playerStats.DefaultIfEmpty() on pm.PlayerID equals ps.PlayerID into temp
                                             from subtemp in temp.DefaultIfEmpty()
                                             select new PlayerData { PlayerMatch = pm, MatchWins = (subtemp == null ? 0 : subtemp.MatchWins),
                                                          MatchesPlayed = subtemp.MatchesPlayed,
                                                          WinRatio = SelectorServiceHelper.GetWinRatio(subtemp)
                                             } into results
                                             orderby results.WinRatio descending
                                             select results;

            IList<PlayerData> playerData = result.ToList();

            outputList.EvenlyDistributePlayersFromList(playerData);

            SelectorServiceHelper.AssignShortestTeamToBibs(outputList, _footballRepository);

            return outputList;
        }
    }
}
