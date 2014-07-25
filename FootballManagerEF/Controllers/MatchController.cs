using FootballManagerEF.EFModel;
using FootballManagerEF.Interfaces;
using FootballManagerEF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FootballManagerEF.Controllers
{
    public class MatchController : Controller
    {
        private IMatchRepository _matchRepository;

        public MatchController()
        {
            _matchRepository = new MatchRepository(new FootballEntities());
        }

        public MatchController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public List<Match> GetMatches()
        {
            return _matchRepository.GetMatches();
        }
    }
}
