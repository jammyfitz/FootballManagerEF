
using FootballManagerEF.Repositories;
using FootballManagerEF.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Tests.ViewModels
{
    [TestFixture]
    public class MatchViewModelTests
    {
        [Test]
        public void MatchViewModel_WhenGetMatchesIsCalledReturnsListOfMatches()
        {
            //Arrange 
            var fakeFootballRepo = new FootballRepository();
            var matchViewModel = new MatchViewModel(fakeFootballRepo);

            //Act
            var result = matchViewModel.GetMatches();

            //Assert
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void MatchViewModel_WhenGetTeamsIsCalledReturnsListOfTeams()
        {
            //Arrange 
            var fakeFootballRepo = new FootballRepository();
            var matchViewModel = new MatchViewModel(fakeFootballRepo);

            //Act
            var result = matchViewModel.GetTeams();

            //Assert
            Assert.That(result.Count, Is.GreaterThan(0));
        }
    }
}
