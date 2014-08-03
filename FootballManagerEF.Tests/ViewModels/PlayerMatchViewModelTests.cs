
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
    public class PlayerMatchViewModelTests
    {
        [Test]
        public void PlayerMatchViewModel_WhenGetPlayerMatchesIsCalledReturnsListOfPlayerMatchesForThatMatch()
        {
            //Arrange 
            var fakePlayerMatchRepo = new FakePlayerMatchRepository();
            var playerMatchViewModel = new PlayerMatchViewModel(fakePlayerMatchRepo);

            //Act
            var result = playerMatchViewModel.GetPlayerMatches(1);

            //Assert
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void PlayerMatchViewModel_WhenGetPlayerMatchesIsCalledDoesntReturnPlayerMatchForAnotherMatch()
        {
            //Arrange 
            var fakePlayerMatchRepo = new FakePlayerMatchRepository();
            var playerMatchViewModel = new PlayerMatchViewModel(fakePlayerMatchRepo);

            //Act
            var result = playerMatchViewModel.GetPlayerMatches(1);

            //Assert
            Assert.That(result.Where(x => x.MatchID != 1), Is.Empty);
        }
    }
}
