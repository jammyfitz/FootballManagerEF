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
    public class ThePorterSelectorService : ISelectorService
    {
        private List<PlayerStat> _playerStats;
        private ObservableCollection<Team> _teams;
        private IFootballRepository _footballRepository;

        public ThePorterSelectorService(IFootballRepository footballRepository)
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
                                             select new PlayerData
                                             {
                                                 PlayerMatch = pm,
                                                 MatchWins = (subtemp == null ? 0 : subtemp.MatchWins),
                                                 MatchesPlayed = subtemp.MatchesPlayed,
                                                 WinRatio = GetWinRatio(subtemp)
                                             } into results
                                             select results;

            IList<PlayerData> playerData = result.ToList();

            // Randomly shuffle list
            playerData.Shuffle();

            // Divide list of player data into a list per team
            IEnumerable<PlayerData> firstTeam = playerData.TakeFirstHalf();
            IEnumerable<PlayerData> lastTeam = playerData.TakeLastHalf();

            // Get total win ratios per team
            decimal? firstTeamsTotalWinRatio = firstTeam.Sum(x => x.WinRatio);
            decimal? lastTeamsTotalWinRatio = lastTeam.Sum(x => x.WinRatio);

            // Get difference in win ratio between teams
            decimal winRatioOffset = GetOffset(firstTeamsTotalWinRatio, lastTeamsTotalWinRatio);

            // Prepare swap candidates based on win ratio differential
            PlayerData firstSwapCandidate = firstTeam.GetClosestToWinRatio(winRatioOffset / 2);
            PlayerData lastSwapCandidate = lastTeam.GetClosestToWinRatio(winRatioOffset / 2);

            // Get difference in win ratio between swap candidates
            decimal? candidateOffset = GetOffset(firstSwapCandidate.WinRatio, lastSwapCandidate.WinRatio);

            // Undertake the swap if there would be a reduction in the differential i.e. improvement in team matching
            if (candidateOffset < winRatioOffset)
                playerData.Swap(firstSwapCandidate, lastSwapCandidate);

            // List is correctly ordered, 
            outputList.DistributePlayersBasedOnListOrder(playerData);
            outputList.AssignTeamsBasedOnListOrder(_teams);

            return outputList;
        }

        private static decimal GetOffset(decimal? firstValue, decimal? secondValue)
        {
            return Math.Abs((decimal)(firstValue - secondValue));
        }

        private static decimal? GetWinRatio(PlayerStat playerStat)
        {
            decimal? matchWins = (decimal?)playerStat.MatchWins;
            decimal? matchesPlayed = (decimal?)playerStat.MatchesPlayed;
            return matchWins / matchesPlayed;
        }
    }
}
