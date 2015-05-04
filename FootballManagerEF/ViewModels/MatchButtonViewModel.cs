using FootballManagerEF.Handlers;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            _footballRepository.InsertMatch(newMatch);
            _matchViewModel.Matches = _footballRepository.GetMatches();
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
        #endregion
    }
}
