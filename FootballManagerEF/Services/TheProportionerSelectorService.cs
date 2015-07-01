using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FootballManagerEF.Extensions;

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
                                             select new PlayerData { PlayerMatch = pm, MatchWins = (subtemp == null ? 0 : subtemp.MatchWins),
                                                          MatchesPlayed = subtemp.MatchesPlayed,
                                                          WinRatio = GetWinRatio(subtemp)
                                             } into results
                                             orderby results.WinRatio descending
                                             select results;

            IList<PlayerData> playerData = result.ToList();

            outputList.EvenlyDistributePlayersFromList(playerData);
            outputList.AssignTeamsBasedOnListOrder(_teams);

            return outputList;
        }

        private static decimal? GetWinRatio(PlayerStat playerStat)
        {
            decimal? matchWins = (decimal?)playerStat.MatchWins;
            decimal? matchesPlayed = (decimal?)playerStat.MatchesPlayed;
            return matchWins / matchesPlayed;
        }
    }
}
