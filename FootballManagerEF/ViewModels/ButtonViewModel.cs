using FootballManagerEF.Handlers;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using FootballManagerEF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootballManagerEF.ViewModels
{
    public class ButtonViewModel
    {
        private IFootballRepository _footballRepository;

        public ButtonViewModel(IFootballRepository footballRepository)
        {
            _footballRepository = footballRepository;
            _canExecute = true;
        }

        public void UpdateButtonClicked()
        {
            // TODO: Insert new PlayerMatches

            _footballRepository.Save();
        }

        #region ICommand Members
        private bool _canExecute;

        public ICommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new CommandHandler(() => UpdateButtonClicked(), _canExecute));
            }
        }
        private ICommand _updateCommand;
        #endregion
    }
}
