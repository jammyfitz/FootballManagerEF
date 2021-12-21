using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FootballManagerEF.Extensions;
using FootballManagerEF.Helpers;

namespace FootballManagerEF.Services
{
    public class ThePorterSelectorService : ISelectorService
    {
        private List<PlayerCalculation> _playerCalculations;
        private ObservableCollection<Team> _teams;
        private IFootballRepository _footballRepository;

        public ThePorterSelectorService(IFootballRepository footballRepository)
        {
            _footballRepository = footballRepository;
            _playerCalculations = _footballRepository.GetPlayerCalculations();
            _teams = _footballRepository.GetTeams();
        }

        public ObservableCollection<PlayerMatch> ApplyAlgorithm(ObservableCollection<PlayerMatch> playerMatches)
        {
            ObservableCollection<PlayerMatch> outputList = new ObservableCollection<PlayerMatch>();

            var playerCalculationsWithScore = playerMatches.Select(x => new PlayerCalculationWithScore
            {
                PlayerMatch = x,
                PlayerCalculation = _playerCalculations.Single(y => y.PlayerID == x.PlayerID),
                Score = SelectorServiceHelper.GetPlayerScore(_playerCalculations.Single(y => y.PlayerID == x.PlayerID))
            })
            .ToList();

            // Randomly shuffle list
            playerCalculationsWithScore.Shuffle();

            // Divide list of players into a list per team
            IEnumerable<PlayerCalculationWithScore> firstTeam = playerCalculationsWithScore.TakeFirstHalf();
            IEnumerable<PlayerCalculationWithScore> lastTeam = playerCalculationsWithScore.TakeLastHalf();

            // Get total player scores per team
            decimal? firstTeamsTotalScore = firstTeam.Sum(x => x.Score);
            decimal? lastTeamsTotalScore = lastTeam.Sum(x => x.Score);

            // Get difference in total player score between teams
            decimal totalScoreDifferential = SelectorServiceHelper.GetOffset(firstTeamsTotalScore, lastTeamsTotalScore);

            // Prepare swap candidates based on score differential
            var firstSwapCandidate = firstTeam.GetClosestToWinRatio(totalScoreDifferential / 2);
            var lastSwapCandidate = lastTeam.GetClosestToWinRatio(totalScoreDifferential / 2);

            // Get difference in player score between swap candidates
            decimal? candidateOffset = SelectorServiceHelper.GetOffset(firstSwapCandidate.Score, lastSwapCandidate.Score);

            // Undertake the swap if there would be a reduction in the differential i.e. improvement in team matching
            if (candidateOffset < totalScoreDifferential)
                playerCalculationsWithScore.Swap(firstSwapCandidate, lastSwapCandidate);

            // List is correctly ordered by player
            outputList.DistributePlayersBasedOnListOrder(playerCalculationsWithScore);
            outputList.AssignTeamsBasedOnListOrder(_teams);

            return outputList;
        }
    }
}
