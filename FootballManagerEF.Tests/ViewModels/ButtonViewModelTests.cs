
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
    public class ButtonViewModelTests
    {
        [Test]
        public void ButtonViewModel_WhenInsertPlayerMatchesIsCalledOnlyFilledRecordsAreInserted()
        {
            //Arrange 
            var fakeFootballRepo = new FootballRepository();
            var playerMatchViewModel = new PlayerMatchViewModel(fakeFootballRepo);
            playerMatchViewModel.PlayerMatches = fakeFootballRepo.GetFiveFilledAndFiveEmptyPlayerMatches();
            var buttonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel);

            //Act
            var result = buttonViewModel.GetPlayerMatchesToInsert();

            //Assert
            Assert.That((int)result.Select(x => (x.MatchID & x.TeamID)!= null).Count(), Is.EqualTo(5));
        }
    }
}
