using FootballManagerEF.EFModel;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FootballManagerEF.ViewModels
{
    public class MatchViewModel : Controller
    {
        private IMatchRepository _matchRepository;

        public MatchViewModel()
        {
            _matchRepository = new MatchRepository(new FootballEntities());
        }

        public MatchViewModel(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public List<Match> GetMatches()
        {
            return _matchRepository.GetMatches();
        }
    }
}
