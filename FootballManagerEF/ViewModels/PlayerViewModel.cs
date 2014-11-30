using FootballManagerEF.Interfaces;
using FootballManagerEF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FootballManagerEF.ViewModels
{
    public class PlayerViewModel : IPlayerViewModel, INotifyPropertyChanged
    {
        private IFootballRepository _footballRepository;
        private List<Player> _players;

        public List<Player> Players
        {
            get { return _players; }

            set
            {
                _players = value;
                RaisePropertyChanged("Players");
            }
        }

        public PlayerViewModel(IFootballRepository footballRepository)
        {
            _footballRepository = footballRepository;
            _players = GetAllPlayers();
        }

        public List<Player> GetAllPlayers()
        {
            return _footballRepository.GetAllPlayers();
        }

        private void dg_Players_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

            // Only act on Commit
            if (e.EditAction == DataGridEditAction.Commit)
            {

                Player editedPlayer = e.Row.DataContext as Player;

                //_footballRepository.Save();

            }

        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
