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
            var result = from pm in playerMatches
                         join ps in _playerStats.DefaultIfEmpty() on pm.PlayerID equals ps.PlayerID into temp
                         from subtemp in temp.DefaultIfEmpty()
                         select new { PlayerMatch = pm, MatchWins = (subtemp == null ? 0 : subtemp.MatchWins),
                             MatchesPlayed = subtemp.MatchesPlayed,
                             WinRatio = subtemp.MatchWins / subtemp.MatchesPlayed
                         } into results
                         orderby results.WinRatio descending
                         select results;

            // Assign the players
            for (int i = 0; i < result.Count() / 2; i++)
            {
                outputList.Add(result.ElementAt(result.Count() - (i + 1)).PlayerMatch);
                outputList.Add(result.ElementAt(i).PlayerMatch);
            }

            // Assign the teams
            for (int i = 0; i < outputList.Count(); i++)
            {
                outputList.ElementAt(i).TeamID = (i < outputList.Count() / 2) ? _teams.First().TeamID : _teams.Last().TeamID;
            }

            return outputList;
        }
    }
}
