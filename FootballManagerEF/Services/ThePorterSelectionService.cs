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
            playerData.Shuffle();

            IEnumerable<PlayerData> firstTeam = playerData.Take(playerData.Count / 2);
            IEnumerable<PlayerData> lastTeam = playerData.Skip(playerData.Count / 2);

            decimal winRatioOffset = Math.Abs((decimal)(firstTeam.Sum(x => x.WinRatio) - lastTeam.Sum(x => x.WinRatio)));
            PlayerData firstSwapCandidate = firstTeam.GetClosestToWinRatio(winRatioOffset / 2);
            PlayerData lastSwapCandidate = lastTeam.GetClosestToWinRatio(winRatioOffset / 2);

            Console.WriteLine("**********************\nBefore");
            Console.WriteLine("firstTeam =" + firstTeam.Sum(x => x.WinRatio));
            Console.WriteLine("lastTeam = " + lastTeam.Sum(x => x.WinRatio));
            Console.WriteLine("offset = " + winRatioOffset);
            Console.WriteLine();

            decimal? candidateOffset =  Math.Abs((decimal)(firstSwapCandidate.WinRatio - lastSwapCandidate.WinRatio));

            if (candidateOffset < winRatioOffset)
                playerData.Swap(firstSwapCandidate, lastSwapCandidate);

            decimal? firstTeamWinRatio = playerData.Take(playerData.Count / 2).Sum(x => x.WinRatio);
            decimal? lastTeamWinRatio = playerData.Skip(playerData.Count / 2).Sum(x => x.WinRatio);

            Console.WriteLine("After");
            Console.WriteLine("firstTeam =" + firstTeamWinRatio);
            Console.WriteLine("lastTeam = " + lastTeamWinRatio);
            Console.WriteLine("offset = " + Math.Abs((decimal)(firstTeamWinRatio - lastTeamWinRatio)));

            // Assign the players
            for (int i = 0; i < playerData.Count() / 2; i++)
            {
                outputList.Add(playerData.ElementAt(playerData.Count() - (i + 1)).PlayerMatch);
                outputList.Add(playerData.ElementAt(i).PlayerMatch);
            }

            // Assign the teams
            for (int i = 0; i < outputList.Count(); i++)
            {
                outputList.ElementAt(i).TeamID = (i < outputList.Count() / 2) ? _teams.First().TeamID : _teams.Last().TeamID;
            }

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
