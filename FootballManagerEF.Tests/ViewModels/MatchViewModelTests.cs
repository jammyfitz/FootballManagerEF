
using FootballManagerEF.Repositories;
using FootballManagerEF.Services;
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
        FootballRepository fakeFootballRepo;
        PlayerMatchViewModel playerMatchViewModel;
        FakePlayerMatchRepository fakePlayerMatchRepo;
        MatchValidatorService matchValidatorService;
        ButtonViewModel buttonViewModel;
        MailerService mailerService;

        [TestFixtureSetUp]
        public void Init()
        {
            fakeFootballRepo = new FootballRepository();
            fakePlayerMatchRepo = new FakePlayerMatchRepository();
            playerMatchViewModel = new PlayerMatchViewModel(fakeFootballRepo);
            matchValidatorService = new MatchValidatorService(playerMatchViewModel, new FakeDialogService());
            mailerService = new MailerService(playerMatchViewModel,fakeFootballRepo);
            buttonViewModel = new ButtonViewModel(fakeFootballRepo, playerMatchViewModel, matchValidatorService, mailerService);
        }

        [Test]
        public void MatchViewModel_WhenAMatchIsSelectedPlayerMatchesIsPopulatedOnPlayerMatchViewModel()
        {
            //Arrange
            var matchViewModel = new MatchViewModel(fakeFootballRepo);
            matchViewModel.PlayerMatchViewModel = playerMatchViewModel;
            matchViewModel.ButtonViewModel = buttonViewModel;

            //Act
            matchViewModel.SelectedMatch = fakeFootballRepo.GetMatches().First();

            //Assert
            Assert.That(matchViewModel.PlayerMatchViewModel.PlayerMatches.Count, Is.GreaterThan(0));
        }

        [Test]
        public void MatchViewModel_WhenAMatchIsSelectedSelectedMatchIsPopulatedOnButtonViewModel()
        {
            //Arrange
            var matchViewModel = new MatchViewModel(fakeFootballRepo);
            var selectedMatch = fakeFootballRepo.GetMatches().First();
            matchViewModel.PlayerMatchViewModel = playerMatchViewModel;
            matchViewModel.ButtonViewModel = buttonViewModel;

            //Act
            matchViewModel.SelectedMatch = selectedMatch;

            //Assert
            Assert.That(matchViewModel.ButtonViewModel.SelectedMatch, Is.EqualTo(selectedMatch));
        }
    }
}
