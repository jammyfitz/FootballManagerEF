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
    public class GiantKillerSelectorService : ISelectorService
    {
        private List<PlayerStat> _playerStats;
        private ObservableCollection<PlayerMatch> _playerMatches;
        private ObservableCollection<Team> _teams;
        private IFootballRepository _footballRepository;

        public GiantKillerSelectorService(IFootballRepository footballRepository, ObservableCollection<PlayerMatch> playerMatches)
        {
            _footballRepository = footballRepository;
            _playerStats = _footballRepository.GetPlayerStats();
            _teams = _footballRepository.GetTeams();
            _playerMatches = playerMatches;
        }

        public ObservableCollection<PlayerMatch> ApplyAlgorithm()
        {
            ObservableCollection<PlayerMatch> outputList = new ObservableCollection<PlayerMatch>();

            //Join the win stats onto list of user selected players
            var result = from pm in _playerMatches
                         join ps in _playerStats.DefaultIfEmpty() on pm.PlayerID equals ps.PlayerID into temp
                         from subtemp in temp.DefaultIfEmpty()
                         select new { PlayerMatch = pm, MatchWins = (subtemp == null ? 0 : subtemp.MatchWins) } into results
                         orderby results.MatchWins descending
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
