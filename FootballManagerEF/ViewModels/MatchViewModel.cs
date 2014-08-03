using FootballManagerEF.EFModel;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Windows.Controls;

namespace FootballManagerEF.ViewModels
{
    public class MatchViewModel : INotifyPropertyChanged
    {
        private IMatchRepository _matchRepository;
        private List<Match> _matches;
        private ObservableCollection<Match> _selectedMatch;

        public List<Match> Matches
        {
            get { return _matches; }
            set
            {
                this.Matches = value;
                RaisePropertyChanged("Matches");
            }
        }

        public ObservableCollection<Match> SelectedMatch
        {
            get { return _selectedMatch; }

            set {
                this.SelectedMatch = value;
                RaisePropertyChanged("SelectedMatch");
            }
        }

        public MatchViewModel()
        {
            _matchRepository = new MatchRepository(new FootballEntities());
            _matches = GetMatches();
        }

        public MatchViewModel(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public List<Match> GetMatches()
        {
            return _matchRepository.GetMatches();
        }

        public void lb_Matches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
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
