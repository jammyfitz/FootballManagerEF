using FootballManagerEF.Handlers;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Helpers;
using System.Linq;
using System.Windows.Input;

namespace FootballManagerEF.ViewModels
{
    public class MatchButtonViewModel
    {
        private IFootballRepository _footballRepository;
        private MatchViewModel _matchViewModel;

        public MatchButtonViewModel(IFootballRepository footballRepository, MatchViewModel matchViewModel)
        {
            _footballRepository = footballRepository;
            _matchViewModel = matchViewModel;
            _canExecute = true;
        }

        public void CreateMatchButtonClicked()
        {
            Match newMatch = _footballRepository.GetMatches().Last().GetNewSubsequentMatch();

            newMatch = _footballRepository.InsertMatch(newMatch);
            _matchViewModel.Matches.Add(newMatch);
        }

        public void DeleteMatchButtonClicked()
        {
            Match matchToDelete = _matchViewModel.SelectedMatch;

            _footballRepository.DeleteMatch(matchToDelete);
            _matchViewModel.SelectedMatch = _matchViewModel.Matches.First();
            _matchViewModel.Matches.Remove(matchToDelete);
        }

        #region ICommand Members
        private bool _canExecute;

        public ICommand CreateMatchCommand
        {
            get
            {
                return _createMatchCommand ?? (_createMatchCommand = new CommandHandler(() => CreateMatchButtonClicked(), _canExecute));
            }
        }
        private ICommand _createMatchCommand;

        public ICommand DeleteMatchCommand
        {
            get
            {
                return _deleteMatchCommand ?? (_deleteMatchCommand = new CommandHandler(() => DeleteMatchButtonClicked(), _canExecute));
            }
        }
        private ICommand _deleteMatchCommand;
        #endregion
    }
}
