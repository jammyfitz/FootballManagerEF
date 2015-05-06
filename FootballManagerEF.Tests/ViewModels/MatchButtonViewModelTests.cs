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
    public class MatchButtonViewModelTests
    {
        FootballRepository fakeFootballRepo;
        MatchViewModel matchViewModel;
        MatchButtonViewModel matchButtonViewModel;
        FakeMatchRepository fakeMatchRepo;

        [TestFixtureSetUp]
        public void Init()
        {
            fakeFootballRepo = new FootballRepository();
            fakeMatchRepo = new FakeMatchRepository();
            matchViewModel = new MatchViewModel(fakeFootballRepo);
            matchButtonViewModel = new MatchButtonViewModel(fakeFootballRepo, matchViewModel);
        }

        [Test]
        public void MatchButtonViewModel_WhenCreateMatchButtonIsClickedAMatchIsAddedToMatches()
        {
            //Arrange
            matchViewModel.Matches = fakeMatchRepo.GetTwoMatches();

            //Act
            matchButtonViewModel.CreateMatchCommand.Execute(null);

            //Assert
            Assert.That(matchViewModel.Matches.Count(), Is.EqualTo(3));
        }
    }
}
