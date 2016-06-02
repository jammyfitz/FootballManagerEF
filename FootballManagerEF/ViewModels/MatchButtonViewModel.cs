using FootballManagerEF.Handlers;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Helpers;
using System.Linq;
using System.Windows.Input;
using System.Windows;

namespace FootballManagerEF.ViewModels
{
    public class MatchButtonViewModel
    {
        private IFootballRepository _footballRepository;
        private MatchViewModel _matchViewModel;
        private IDialogSelectionService _dialogSelectionService;

        public MatchButtonViewModel(IFootballRepository footballRepository, MatchViewModel matchViewModel, IDialogSelectionService dialogSelectionService)
        {
            _footballRepository = footballRepository;
            _matchViewModel = matchViewModel;
            _dialogSelectionService = dialogSelectionService;
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
            MessageBoxResult messageBoxResult = _dialogSelectionService.ShowDialog("Are you sure?", "Delete Confirmation");
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Match matchToDelete = _matchViewModel.SelectedMatch;

                _footballRepository.DeleteMatch(matchToDelete);
                _matchViewModel.SelectedMatch = _matchViewModel.Matches.First();
                _matchViewModel.Matches.Remove(matchToDelete);
            }
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
